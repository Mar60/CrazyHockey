using UnityEngine;
using System.Collections;

public class throwing : MonoBehaviour {

    private GameObject cam;

	// Use this for initialization
	void Start () {
        cam = GameObject.Find("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().AddForce(cam.GetComponent<Transform>().forward*5);
	}
}
