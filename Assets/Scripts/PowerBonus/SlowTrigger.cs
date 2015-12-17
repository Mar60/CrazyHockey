using UnityEngine;
using System.Collections;

public class SlowTrigger : MonoBehaviour {
	private const string PROJECTILE_SLOW_TAG = "ProjectileSlow";
	private const string LOG_TAG = "SlowTrigger - ";


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag (PROJECTILE_SLOW_TAG)) {
			Debug.Log (LOG_TAG +" recieved "+ PROJECTILE_SLOW_TAG);
		}
	}
}
