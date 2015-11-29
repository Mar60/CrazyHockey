using UnityEngine;
using System.Collections;

public class BonusPlayerManager : MonoBehaviour {

    private const string LOG_TAG = "BonusPlayerManager - ";

    private bool shieldPowerIsActive = true;
    private  bool ballTrapPowerIsActive = false;
    private  bool projectilePowerIsActive= false;
	private  bool terrainPowerIsActive= false;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {//TODO match item on the field with power enabled
        if (shieldPowerIsActive && Input.GetKeyDown("1")) //TODO change with razer control
        {
            GetComponent<ShieldController>().riseShield();
			//TODO add this line when FPS installed shieldPowerIsActive = false;
        }
        else if (ballTrapPowerIsActive && Input.GetKey("2"))//TODO change with razer control
        {

        }
        else if (projectilePowerIsActive && Input.GetKey("3"))//TODO change with razer control
        {

        }
		else if (terrainPowerIsActive && Input.GetKey("4"))//TODO change with razer control
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
	public void activateTerrainPower()
	{
		Debug.Log(LOG_TAG + "Get terrain/mountain power");
		terrainPowerIsActive = true;
	}
}

