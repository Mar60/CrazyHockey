using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

    
    private int cpt;
    private GameObject Ball;
	// Use this for initialization
	void Start () {
        cpt = 0;
        Ball = GameObject.Find("Ball");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        cpt++;
        Ball.GetComponent<Control>().resetTimerBall();
    }

    public int score()
    {
        return cpt;
    }
}
