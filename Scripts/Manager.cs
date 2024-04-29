using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    //public GameObject ball;

    public GameObject powerSwing;
    public GameObject normalSwing;
    public GameObject contactSwing;
    //public GameObject footBallPrefab;


    Pursuer pursuer;
    Rigidbody rb;
    SphereCollider sphereCollider;

    void Start()
    {
       /* ball.GetComponent<Pursuer>().enabled = false;
        ball.GetComponent<Rigidbody>().isKinematic = true;
        ball.GetComponent<Rigidbody>().useGravity = false;*/

        powerSwing.SetActive(false);
        normalSwing.SetActive(false);
        contactSwing.SetActive(false);

        //ResetBallTransform();
    }

    public void PowerSwing()
    {
        normalSwing.SetActive(false);
        powerSwing.SetActive(true);
        contactSwing.SetActive(false);
        //ball.GetComponent<Rigidbody>().isKinematic = false;
        //ball.GetComponent<Pursuer>().enabled = true;
        //ball.GetComponent<Rigidbody>().useGravity = true;
    }

    public void NormalSwing()
    {
        powerSwing.SetActive(false);
        normalSwing.SetActive(true);
        contactSwing.SetActive(false);
    }

    public void ContactSwing()
    {
        powerSwing.SetActive(false);
        normalSwing.SetActive(false);
        contactSwing.SetActive(true);

        //GameObject newBall = Instantiate(footBallPrefab, this.transform);
        //newBall.transform.parent = null;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            ResetBallTransform();
            powerSwing.SetActive(true);
        }
    }*/

    /*public void ResetBallTransform()
    {
        ball.GetComponent<Rigidbody>().isKinematic = true;
        ball.GetComponent<Pursuer>().enabled = false;
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.transform.position = new Vector3(0f, 4f, 18f);
    }*/

}
