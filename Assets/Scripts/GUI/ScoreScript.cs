﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    Text text;
	// Use this for initialization
	void Start () {
        player1 = GameObject.Find("Player1_score");
        player2 = GameObject.Find("Player2_score");
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        text.text = "Joueur 1 : "+ player1.GetComponent<GoalScript>().score() + "\nJoueur 2 : " + player2.GetComponent<GoalScript>().score();
    }
}
