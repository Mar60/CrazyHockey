using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {
	public Rigidbody projectileBlurring;
	public int speed = 70;
	public GameObject startPoint;
	// Use this for initialization
	void Start () {
		startPoint = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("f")) {
			Rigidbody clone = (Rigidbody)Instantiate(projectileBlurring, startPoint.GetComponent<Transform>().position, Quaternion.identity);
			clone.velocity = transform.TransformDirection(new Vector3 (0, 0, speed));
			Destroy(clone.gameObject, 3);
		}
	}			
}
