using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {
    private const string LOG_TAG = "ShieldController - ";

    public GameObject shield;
	private bool flagStartTimer = false;
	private const float TIME_SIELD_ACTIVE = 5.0f;
	private float startTime = 0.0f;
	//private float currentTimer;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(flagStartTimer == true){
			
			//currentTimer = TIME_SIELD_ACTIVE - (Time.time - startTime);
			if(startTime + TIME_SIELD_ACTIVE < Time.time){
				flagStartTimer = false;
				lowerShield();				
			}
		}


	}

    public void riseShield() {
        Debug.Log(LOG_TAG + "The shield is rised");
        shield.GetComponent<Animator>().SetBool("RiseShield",true);
		flagStartTimer = true;
		startTime = Time.time;//Time when the shield was rised
	}

	public void lowerShield(){
		Debug.Log(LOG_TAG + "ShieldController The shield is lowered");//TODO add a timer
		shield.GetComponent<Animator>().SetBool("RiseShield", false);
	}


}
