using UnityEngine;
using System.Collections;

public class slowPower : MonoBehaviour {

    public GameObject slow;
    private Vector3 initialPosition;
    private GameObject cam;
    private Object slowclone;
    // Use this for initialization
    void Start () {
        cam = GameObject.Find("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(1))
        {
            initialPosition = transform.position;
            Instantiate(slow, initialPosition, Quaternion.identity);
         }

	}


}
