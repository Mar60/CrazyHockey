using UnityEngine;
using System.Collections;

public class laserScript : MonoBehaviour {

    LineRenderer line;
	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.DrawLine(transform.position, transform.forward, Color.red);
       // Debug.Log(transform.forward);
        if (Input.GetKeyDown("4"))
        {
            Debug.Log("yo");
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
	}
    IEnumerator FireLaser()
    {
        line.enabled = true;
        while(Input.GetKey("4"))
        {
            Debug.Log(transform.position);
            Ray ray = new Ray(transform.position, transform.forward);
            line.SetPosition(0, ray.origin);
            line.SetPosition(1, ray.GetPoint(100));
            yield return null;
        }
        line.enabled = false;
    }
}
