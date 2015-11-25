using UnityEngine; 
using System.Collections; 

public class Control : MonoBehaviour {  
	public int speed = 2;  
	private Rigidbody rb;
    float startTime = 0.0f;
    bool count = true;
    bool resetTimer = true;
    float currentTimer;
    private GameObject timerU;
    private Vector3 initialPosition;
	
	void Start()  
	{  
		rb = GetComponent<Rigidbody>();
        initialPosition = GetComponent<Transform>().position;
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
                startforce();
                count = false;
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
    
    void startforce()
    {
        rb.AddForce(Vector3.forward * speed);
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
        count = true;
        resetTimer = true;
        GetComponent<Transform>().position = initialPosition;
    }
} 
