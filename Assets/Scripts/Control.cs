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
	
	void Start()  
	{  
		rb = GetComponent<Rigidbody>();
        initialPosition = GetComponent<Transform>().position;
        force = new Vector3(1,0,1);
        Debug.Log(Vector3.left);
        direction = new Vector3(0, 0, 0);

    }
    void Update()  
	{  
		KeyboardMovements();

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
                //addforce();
            }
        }
    } 

    void FixedUpdate()
    {
        if (gameStarted)
        {
           

            //addforce();
            if (rb.velocity != direction && rb.velocity != Vector3.zero)
            {
                direction = rb.velocity;
            }
            if(rb.velocity.magnitude != speedWall && rb.velocity.magnitude >1)
            {
                if (rb.velocity.magnitude < 25)
                {
                    speedWall = rb.velocity.magnitude;
                }
                Debug.Log(rb.velocity.magnitude);
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
        if(other.gameObject.tag == "Player")
        {
            setForce(other.gameObject.GetComponent<Transform>().forward);
            addforce();
        }
        if(GetComponent<Renderer>().material.color == Color.red)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        if(other.gameObject.layer == 10)
        {
            force = Vector3.Reflect(direction, other.contacts[0].normal);
            addSpecifiedForce();
        }
    }
} 
