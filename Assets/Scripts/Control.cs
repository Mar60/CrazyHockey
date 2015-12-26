using UnityEngine; 
using System.Collections; 

public class Control : MonoBehaviour {  
	private int speed = 50;  
	private Rigidbody rb;
    float startTime = 0.0f;
    bool count = true;
    bool resetTimer = true;
    float currentTimer;
    private GameObject timerU;
    private Vector3 initialPosition;
    private bool gameStarted;
    private Vector3 force,direction;
    private float speedWall;
    private float limit;
    private GameObject difficulty;
    public AudioClip soundGoalClip;
    private AudioSource soundGoalSource;
    public Material defaultMaterial;
    public Material trapMaterial;
    public bool trapmat;

    void Start()  
	{
        GetComponent<MeshRenderer>().material = defaultMaterial;
        trapmat = false;
        soundGoalSource = CreateSound(soundGoalClip);
        difficulty = GameObject.Find("Difficulty");
		rb = GetComponent<Rigidbody>();
        initialPosition = GetComponent<Transform>().position;
        force = new Vector3(1,0,1);
       // Debug.Log(Vector3.left);
        direction = new Vector3(0, 0, 0);
        if (difficulty == null)
        {
            limit = 25;
        }
        else
        {
            switch ((int)difficulty.GetComponent<Difficulty>().val)
            {
                case 1:
                    limit = 25;
                    break;
                case 2:
                    limit = 30;
                    break;
                case 3:
                    limit = 40;
                    break;
                case 4:
                    limit = 50;
                    break;
                default:
                    limit = 25;
                    break;
            }
        }
        //Debug.Log(limit);

    }
    void Update()  
	{  
		KeyboardMovements();
        //Debug.Log();
        if (count)
        {
            if (resetTimer)
            {
                startTime = Time.time;
                resetTimer = false;
            }
            currentTimer = 5.0f - (Time.time - startTime);
            if (startTime + 5.0f < Time.time)
            {
                gameStarted = true;
                count = false;
                soundGoalSource.Play();
                //addforce();
            }
        }
    }

    public void setTrap()
    {
        GetComponent<MeshRenderer>().material = trapMaterial;
        trapmat = true;
    }

    public void setDefault()
    {
        GetComponent<MeshRenderer>().material = defaultMaterial;
        trapmat = false;
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

    void FixedUpdate()
    {
        if (gameStarted)
        {

            //Debug.Log(limit);
            //addforce();
            if (rb.velocity != direction && rb.velocity != Vector3.zero)
            {
                direction = rb.velocity;
            }
            if(rb.velocity.magnitude != speedWall && rb.velocity.magnitude >1)
            {
                if (rb.velocity.magnitude < limit)
                {
                    speedWall = rb.velocity.magnitude;
                }
                //Debug.Log(rb.velocity.magnitude);
            }

        }
    }
	
	void KeyboardMovements()  
	{  
    
		if(Input.GetKey("right"))  
		{  
			rb.AddForce(Vector3.right * speed);  
		}  
		else if(Input.GetKey("left"))  
		{  
			rb.AddForce(Vector3.left * speed);  
		} 
		else if(Input.GetKey("up"))  
		{  
			rb.AddForce(Vector3.forward * speed);  
		}  
		else if(Input.GetKey("down"))  
		{  
			rb.AddForce(Vector3.back * speed);  
		}  
	}  

    public void setForce(Vector3 forceToSet)
    {
        force = forceToSet;
    }
    
    public void addforce()
    {
        rb.AddForce(force * 1000);
       // Debug.Log(force);
    }
    public void addSpecifiedForce()
    {
        rb.AddForce(force * speedWall);
    }

    public float getTimer()
    {
        return currentTimer;
    }
    public bool getCount()
    {
        return count;
    }
    public void resetTimerBall()
    {
        gameStarted = false;
        count = true;
        resetTimer = true;
        GetComponent<Transform>().position = initialPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.name == "Shield")
        {
            setForce(other.gameObject.GetComponent<Transform>().forward);
            addforce();
        }
        if(trapmat)
        {
            setDefault();
        }
        if(other.gameObject.layer == 10 || other.gameObject.tag == "Terrain")
        {
            force = Vector3.Reflect(direction, other.contacts[0].normal);
            addSpecifiedForce();
        }
    }
} 
