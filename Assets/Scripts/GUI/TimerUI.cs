using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TimerUI : MonoBehaviour {

    private GameObject Ball;
    Text text;
	// Use this for initialization
	void Start () {
        Ball = GameObject.Find("Ball");
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = Ball.GetComponent<Control>().getTimer().ToString();
        if(Ball.GetComponent<Control>().getCount())
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
	}
}
