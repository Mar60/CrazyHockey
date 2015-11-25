using UnityEngine;
using System.Collections;

public class BonusPlayerManager : MonoBehaviour {

    private const string LOG_TAG = "BonusPlayerManager - ";

    private bool shieldPowerIsActive = false;
    private  bool ballTrapPowerIsActive = false;
    private  bool projectilePowerIsActive= false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (shieldPowerIsActive && Input.GetKey("1")) //TODO change with razer control
        {
            GetComponent<ShieldController>().riseShield();
        }
        else if (ballTrapPowerIsActive && Input.GetKey("2"))//TODO change with razer control
        {

        }
        else if (projectilePowerIsActive && Input.GetKey("3"))//TODO change with razer control
        {

        }
    
    }

    public void activateShieldPower() {
        Debug.Log(LOG_TAG+"Get shield power");
        shieldPowerIsActive = true;
    }
    public void activateBallTrapPower()
    {
        Debug.Log(LOG_TAG + "Get ball trap power");
        ballTrapPowerIsActive = true;
    }
    public void activateProjectilePower()
    {
        Debug.Log(LOG_TAG + "Get projectile power");
        projectilePowerIsActive = true;
    }
}

