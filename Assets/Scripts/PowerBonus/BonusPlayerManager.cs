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
    private float initialPosition,initialRotation;
    private GameObject ball;
    private bool blurVision, resetTimer;
    private float startTime, currentTimer;
	private Vector3 NULL_VECTOR3 = new Vector3(200000,200000,200000);
	private Vector3 localPointTerrainRaycasting;
    public AudioClip soundGoalClip;
    private AudioSource soundGoalSource;


    // Use this for initialization
    void Start () {
        hand = GameObject.Find("Hand - Left");
        ball = GameObject.Find("Ball");
		localPointTerrainRaycasting = NULL_VECTOR3;
        soundGoalSource = CreateSound(soundGoalClip);

    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown("2"))
        {
            GetComponent<ShieldController>().riseShield();

        }
        if (projectilePowerIsActive > 0 && Input.GetKeyDown("1") /*hand.GetComponent<Transform>().rotation.x < 0.5*/)
        {
            if (!GetComponentInChildren<ShootProjectile>().getThrowed())
            {
                GetComponentInChildren<ShootProjectile>().sendProjectile();
                projectilePowerIsActive--;
                soundGoalSource.Play();

            }
        }
        m_controller = SixenseInput.GetController(SixenseHands.LEFT);
        if (/*m_controller.GetButtonDown(SixenseButtons.TRIGGER)*/Input.GetKeyDown("3"))
        {
            if (ballTrapPowerIsActive > 0 && !ball.GetComponent<Control>().trapmat )
            {
                ballTrapPowerIsActive--;
                 ball.GetComponent<Control>().setTrap();
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
        if (shieldPowerIsActive > 0 && /*Input.GetKeyDown("2")*/m_controller.GetButtonUp(SixenseButtons.ONE))
        {
	        if ((hand.GetComponent<Transform>().position.y - initialPosition) > 0.1)
	        {
	            GetComponent<ShieldController>().riseShield();
	        	shieldPowerIsActive--;
	        }

         }
		if (terrainPowerIsActive > 0 && /*Input.GetKeyDown("2")*/m_controller.GetButtonUp(SixenseButtons.TWO))
		{
			if(localPointTerrainRaycasting.Equals(NULL_VECTOR3))
				localPointTerrainRaycasting = GetComponent<RayCasting>().sendRaycast();
			if ( !localPointTerrainRaycasting.Equals(NULL_VECTOR3) &&((hand.GetComponent<Transform>().position.y - initialPosition) > 0.1))
			{
				GetComponent<RaiseLowerTerrain>().riseController(localPointTerrainRaycasting);
				terrainPowerIsActive--;
				localPointTerrainRaycasting = NULL_VECTOR3;
                Debug.Log("raiseterrain");
			}
			
		}


        if(m_controller.GetButtonDown(SixenseButtons.ONE))
        {
            initialPosition = hand.GetComponent<Transform>().position.y;
        }
        if (m_controller.GetButtonDown(SixenseButtons.TWO))
        {
            initialPosition = hand.GetComponent<Transform>().position.y;
            Debug.Log(initialPosition);
        }
        if (m_controller.GetButtonDown(SixenseButtons.FOUR))
        {
            initialRotation = hand.GetComponent<Transform>().rotation.x;
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

    public void setLastPlayerTouched()
    {
        ball.GetComponent<BallTrigger>().lastPlayerTouched = this.gameObject;  
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
    public void setBlur()
    {
        blurVision = true;
        resetTimer = true;
    }
    private AudioSource CreateSound(AudioClip clip)
    {
        //Création de la souvelle source audio et configuration de ses propriétés
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.clip = clip;
        source.volume = 10;
        source.loop = false;
        return source;
    }

}

