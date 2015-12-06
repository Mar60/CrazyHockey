using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBonusManager : MonoBehaviour {


    private GameObject pro,trap,shield,ground,player2;
	// Use this for initialization
	void Start () {
        player2 = GameObject.Find("Player2");
        pro = GameObject.Find("Text_pro");
        trap = GameObject.Find("Text_trap");
        shield = GameObject.Find("Text_shield");
        ground = GameObject.Find("Text_ground");
        ground.GetComponent<Text>().text = ""+ player2.GetComponent<BonusPlayerManager>().getTerrain();
        pro.GetComponent<Text>().text = "" + player2.GetComponent<BonusPlayerManager>().getProj();
        trap.GetComponent<Text>().text = "" + player2.GetComponent<BonusPlayerManager>().getTrap();
        shield.GetComponent<Text>().text = "" + player2.GetComponent<BonusPlayerManager>().getTrap();


    }

    // Update is called once per frame
    void Update () {
        ground.GetComponent<Text>().text = "" + player2.GetComponent<BonusPlayerManager>().getTerrain();
        pro.GetComponent<Text>().text = "" + player2.GetComponent<BonusPlayerManager>().getProj();
        trap.GetComponent<Text>().text = "" + player2.GetComponent<BonusPlayerManager>().getTrap();
        shield.GetComponent<Text>().text = "" + player2.GetComponent<BonusPlayerManager>().getShield();
    }
}
