using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour {

    public float val;
    public Slider slid;
	// Use this for initialization
	void Start () {

    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }


	
	// Update is called once per frame
	void Update () {

	}

    private void setValue(float value)
    {
        val = value;
    }
    public void changeDifficulty()
    {
        Debug.Log(slid.value);
        setValue(slid.value);

    }
}
