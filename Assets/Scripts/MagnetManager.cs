using UnityEngine;
using System.Collections;

public class MagnetManager : MonoBehaviour {


    private bool count,resetTimer;
    private float startTime, currentTimer;
    private GameObject[] players;
    private GameObject ball;
    Vector3 heading, direction;
    float distance;
	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        resetTimer = true;
        ball = GameObject.Find("Ball");
	}
	
	// Update is called once per frame
	void Update () {
	    if(GetComponent<Renderer>().enabled == true)
        {
                if (resetTimer)
                {
                    startTime = Time.time;
                    resetTimer = false;
                }
                currentTimer = 5.0f - (Time.time - startTime);
                if (startTime + 5.0f < Time.time)
                {
                GetComponent<Renderer>().enabled = false;
                ball.GetComponent<BallTrigger>().setMagnetEnable();
                resetTimer = true;
            }

            foreach (GameObject player in players)
            {
                heading = GetComponent<Transform>().position - player.GetComponent<Transform>().position;
                distance = heading.magnitude;
                direction = heading / distance;
                direction.y = 0;
                direction.z = 0;
                player.GetComponent<Rigidbody>().AddForce(direction,ForceMode.VelocityChange);
            }
        }
	}

    public void setMagnet()
    {
        GetComponent<Renderer>().enabled = true;
    }
}
