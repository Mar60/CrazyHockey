using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {
    private const string LOG_TAG = "ShieldController - ";

    public GameObject shield;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void riseShield() {
        Debug.Log(LOG_TAG + "The shield is rised");
        shield.GetComponent<Animator>().SetBool("RiseShield",true);
        Debug.Log(LOG_TAG + "ShieldController The shield is lowered");
        shield.GetComponent<Animator>().SetBool("RiseShield", false);


    }
}
