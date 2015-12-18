using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {
	public Rigidbody projectileBlurring;
	public int speed = 70;
	public GameObject startPoint;
    private bool throwed = false;
    private bool  resetTimer;
    private float startTime, currentTimer;
    // Use this for initialization
    void Start () {
		//startPoint = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (throwed)
        {
            if (resetTimer)
            {
                startTime = Time.time;
                resetTimer = false;
            }
            currentTimer = 5.0f - (Time.time - startTime);
            if (startTime + 5.0f < Time.time)
            {
                resetTimer = true;
                throwed = false;
            }


        }
        /*if (Input.GetKeyDown ("f")) {
			Rigidbody clone = (Rigidbody)Instantiate(projectileBlurring, startPoint.GetComponent<Transform>().position, Quaternion.identity);
			clone.velocity = transform.TransformDirection(new Vector3 (0, 0, speed));
			Destroy(clone.gameObject, 3);
		}*/
    }		

	public void sendProjectile(){
        if (!throwed)
        {
            throwed = true;
            Rigidbody clone = (Rigidbody)Instantiate(projectileBlurring, startPoint.GetComponent<Transform>().position, Quaternion.identity);
            clone.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            Destroy(clone.gameObject, 3);
        }
	}

    public bool getThrowed()
    {
        return throwed;
    }
}
