using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goalie : MonoBehaviour
{
    public Text startText;
    public Text discontentmentText;
    public Text actionText;

    BallManager ballManager;

    Goal[] myGoals;
    Action[] myActions;
    Action changeOverTime;
    const float TIME_BTWN_TICKS = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        ballManager = GetComponent<BallManager>();

        myGoals = new Goal[3];
        myGoals[0] = new Goal("Power Swing", 5f);
        myGoals[1] = new Goal("Normal Swing", 3f);
        myGoals[2] = new Goal("Contact Swing", 1f);

        myActions = new Action[3];
        myActions[0] = new Action("swing very hard");
        myActions[0].targetGoals.Add(new Goal("Power Swing", -6f));
        myActions[0].targetGoals.Add(new Goal("Normal Swing", +3f));
        myActions[0].targetGoals.Add(new Goal("Contact Swing", +1f));
        
        myActions[1] = new Action("swing normally");
        myActions[1].targetGoals.Add(new Goal("Power Swing", +2f));
        myActions[1].targetGoals.Add(new Goal("Normal Swing", -2f));
        myActions[1].targetGoals.Add(new Goal("Contact Swing", +1f));
        
        //myActions[2] = new Action("do the trebuchet throw thing");
        //myActions[2].targetGoals.Add(new Goal("Block Shot", -3f));
        //myActions[2].targetGoals.Add(new Goal("Hydrate", -2f));
        //myActions[2].targetGoals.Add(new Goal("Score", +4f));
        
        myActions[2] = new Action("swing very soft");
        myActions[2].targetGoals.Add(new Goal("Power Swing", -1f));
        myActions[2].targetGoals.Add(new Goal("Normal Swing", +2f));
        myActions[2].targetGoals.Add(new Goal("Contact Swing", -10f));

        changeOverTime = new Action("tick");
        changeOverTime.targetGoals.Add(new Goal("Power Swing", +3f));
        changeOverTime.targetGoals.Add(new Goal("Normal Swing", +2f));
        changeOverTime.targetGoals.Add(new Goal("Contact Swing", +1f));

        startText.text = "Starting clock. One hour will pass every " + TIME_BTWN_TICKS + " seconds.";
        InvokeRepeating("Tick", 0f, TIME_BTWN_TICKS);
    }

    void Tick()
    {
        foreach (Goal goal in myGoals)
        {
            goal.goalValue += changeOverTime.GetGoalChange(goal);
            goal.goalValue = Mathf.Max(goal.goalValue, 0);
        }

        PrintGoals();
    }

    void PrintGoals()
    {
        string goalString = "";
        foreach (Goal goal in myGoals)
        {
            goalString += goal.goalName + ": " + goal.goalValue + "\n";
        }
        goalString += "Discontentment: " + CurrentDiscontentment();
        discontentmentText.text = goalString;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Action bestAction = ChooseAction(myActions, myGoals);
            actionText.text = "I choose to " + bestAction.actionName;

            if(bestAction.actionName == "swing very hard")
            {
                ballManager.PowerSwing();
            }
            else if(bestAction.actionName == "swing normally")
            {
                ballManager.NormalSwing();
            }
            else if(bestAction.actionName == "swing very soft")
            {
                ballManager.ContactSwing();
            }

            foreach(Goal goal in myGoals)
            {
                goal.goalValue += bestAction.GetGoalChange(goal);
                goal.goalValue = Mathf.Max(goal.goalValue, 0f);
            }

            PrintGoals();
        }
    }

    Action ChooseAction(Action[] actions, Goal[] goals)
    {
        Action bestActionToTake = null;
        float bestVal = float.PositiveInfinity;

        foreach(Action action in actions)
        {
            float currVal = Discontentment(action, goals);
            if(currVal < bestVal)
            {
                bestVal = currVal;
                bestActionToTake = action;
            }
        }

        return bestActionToTake;
    }

    float Discontentment(Action action, Goal[] goals)
    {
        float discontentment = 0f;

        foreach(Goal goal in goals)
        {
            float newVal = goal.goalValue + action.GetGoalChange(goal);
            newVal = Mathf.Max(newVal, 0f);

            discontentment += goal.GetDiscontentment(newVal);
        }

        return discontentment;
    }

    float CurrentDiscontentment()
    {
        float totalDiscontentment = 0f;

        foreach(Goal goal in myGoals)
        {
            totalDiscontentment += goal.GetDiscontentment(goal.goalValue);
        }

        return totalDiscontentment;
    }
}
