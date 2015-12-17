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
    private Vector3 force;
	
	void Start()  
	{  
		rb = GetComponent<Rigidbody>();
        initialPosition = GetComponent<Transform>().position;
        force = new Vector3(1,0,1);
        Debug.Log(Vector3.left);

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
        Debug.Log(other.gameObject);
        if(other.gameObject.tag == "Player")
        {
            setForce(other.gameObject.GetComponent<Transform>().forward);
            addforce();
        }
        if(GetComponent<Renderer>().material.color == Color.red)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
} 
