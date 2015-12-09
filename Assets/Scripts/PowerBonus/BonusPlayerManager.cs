using UnityEngine;
using System.Collections;

public class BonusPlayerManager : MonoBehaviour {

    private const string LOG_TAG = "BonusPlayerManager - ";

    private int shieldPowerIsActive = 10;
    private  int ballTrapPowerIsActive = 10;
    private  int projectilePowerIsActive= 10;
	private  int terrainPowerIsActive= 10;
    private SixenseHands Sixense;
    private SixenseInput.Controller m_controller;
    private bool firstTimeButton = true;
    private int cpt;
    private GameObject hand;
    private float initialPosition;
    private GameObject ball;
    private bool blurVision, resetTimer;
    private float startTime, currentTimer;
    // Use this for initialization
    void Start () {
        hand = GameObject.Find("Hand - Left");
        ball = GameObject.Find("Ball");
	}

    // Update is called once per frame
    void Update() {//TODO match item on the field with power enabled
        m_controller = SixenseInput.GetController(SixenseHands.LEFT);
        if (m_controller.GetButtonDown(SixenseButtons.TRIGGER))
        {
            if (ballTrapPowerIsActive > 0 && ball.GetComponent<Renderer>().material.color != Color.red)
            {
                ballTrapPowerIsActive--;
                ball.GetComponent<Renderer>().material.color = Color.red;
            }
        }

        if (blurVision)
        {
            if (resetTimer)
            {
                GetComponentInChildren<UnityStandardAssets.ImageEffects.Blur>().enabled = true;
                startTime = Time.time;
                resetTimer = false;
            }
            currentTimer = 5.0f - (Time.time - startTime);
            if (startTime + 5.0f < Time.time)
            {
                GetComponentInChildren<UnityStandardAssets.ImageEffects.Blur>().enabled = false;
                resetTimer = true;
                blurVision = false;
            }


        }
        if (shieldPowerIsActive > 0 && /*Input.GetKeyDown("1")*/m_controller.GetButtonUp(SixenseButtons.ONE))
        {
                if ((hand.GetComponent<Transform>().position.y - initialPosition) > 0.1)
                {
                    GetComponent<ShieldController>().riseShield();
                shieldPowerIsActive--;
                }

         }
        if(m_controller.GetButtonDown(SixenseButtons.ONE))
        {
            initialPosition = hand.GetComponent<Transform>().position.y;
        }




    }
        //Debug.Log("yo");
        //if (shieldPowerIsActive>0 && /*Input.GetKeyDown("1")*/m_controller.GetButtonDown(SixenseButtons.ONE)) //TODO change with razer control
        /*{
            //Debug.Log("yo");

            if (!firstTimeButton)
            {
                initialPosition = hand.GetComponent<Transform>().position.y;
                firstTimeButton = true;
            }
            cpt++;
            if (cpt == 60 && ((hand.GetComponent<Transform>().position.y - initialPosition) > 1 ))
            {
                GetComponent<ShieldController>().riseShield();
                shieldPowerIsActive --;
                firstTimeButton = false;
                cpt = 0;
            }
			//TODO add this line when FPS installed shieldPowerIsActive = false;
        }
        //if(Input.GetKey("1")/*m_controller.GetButtonDown(SixenseButtons.TWO)*///)
        //{ 
        /*Debug.Log("yo");
        if (ballTrapPowerIsActive>0)
            {
            
            ball.GetComponent<Material>().SetColor("_SpecColor", Color.red);
                ballTrapPowerIsActive--;
            }
        //}
        else if (projectilePowerIsActive>0 && Input.GetKey("3"))//TODO change with razer control
        {

        }
		else if (terrainPowerIsActive>0 && Input.GetKey("4"))//TODO change with razer control
		{
			
		}*/
    
   // }

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
    public void setBlur()
    {
        blurVision = true;
        resetTimer = true;
    }
}

