using UnityEngine;
using System.Collections;

public class BonusPlayerManager : MonoBehaviour {

    private const string LOG_TAG = "BonusPlayerManager - ";

    private int shieldPowerIsActive = 0;
    private  int ballTrapPowerIsActive = 0;
    private  int projectilePowerIsActive= 0;
	private  int terrainPowerIsActive= 0;
    private SixenseHands Sixense;
    private SixenseInput.Controller m_controller;
    private bool firstTimeButton = false;
    private int cpt;
    private GameObject hand;
    private float initialPosition;
    // Use this for initialization
    void Start () {
        hand = GameObject.Find("Hand - Left");
        Debug.Log(hand);
	}
	
	// Update is called once per frame
	void Update () {//TODO match item on the field with power enabled
        m_controller = SixenseInput.GetController(SixenseHands.LEFT);
        if (shieldPowerIsActive>0 && /*Input.GetKeyDown("1")*/m_controller.GetButtonDown(SixenseButtons.ONE)) //TODO change with razer control
        {
            if(!firstTimeButton)
            {
                initialPosition = hand.GetComponent<Transform>().position.y;
                firstTimeButton = true;
            }
            cpt++;
            if (cpt == 60 && ((hand.GetComponent<Transform>().position.y - initialPosition) > 1 ))
            {
                GetComponent<ShieldController>().riseShield();
                shieldPowerIsActive --;
            }
			//TODO add this line when FPS installed shieldPowerIsActive = false;
        }
        else if (ballTrapPowerIsActive>0 && /*Input.GetKey("2")*/m_controller.GetButtonDown(SixenseButtons.TWO))//TODO change with razer control
        {

        }
        else if (projectilePowerIsActive>0 && Input.GetKey("3"))//TODO change with razer control
        {

        }
		else if (terrainPowerIsActive>0 && Input.GetKey("4"))//TODO change with razer control
		{
			
		}
    
    }

    public void activateShieldPower() {
        Debug.Log(LOG_TAG+"Get shield power");
        shieldPowerIsActive ++;
    }
    public void activateBallTrapPower()
    {
        Debug.Log(LOG_TAG + "Get ball trap power");
        ballTrapPowerIsActive++;
    }
    public void activateProjectilePower()
    {
        Debug.Log(LOG_TAG + "Get projectile power");
        projectilePowerIsActive++;
    }
	public void activateTerrainPower()
	{
		Debug.Log(LOG_TAG + "Get terrain/mountain power");
		terrainPowerIsActive ++;
	}
    public int getShield()
    {
        return shieldPowerIsActive; 
    }
    public int getTrap()
    {
        return ballTrapPowerIsActive;
    }
    public int getProj()
    {
        return projectilePowerIsActive;
    }
    public int getTerrain()
    {
        return terrainPowerIsActive;
    }
}

