using UnityEngine; 
using System.Collections; 

public class Control : MonoBehaviour {  
	public int speed = 2;  
	private Rigidbody rb; 
	
	void Start()  
	{  
		rb = GetComponent<Rigidbody>();  
	} 
	void Update()  
	{  
		KeyboardMovements();  
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
} 
