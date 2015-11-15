using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        Application.LoadLevel(1);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnMouseUp()
    {
        Debug.Log("lol");
    }
}
