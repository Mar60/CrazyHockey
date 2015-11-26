using UnityEngine;
using System.Collections;

public class BallTrigger : MonoBehaviour {
    private GameObject lastPlayerTouched;
    public BonusGenerator manager;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collideEvent)
    {
        if (collideEvent.gameObject.CompareTag("BonusShield"))
        {
            Debug.Log("Destruction bonus shield");
            Destroy(collideEvent.gameObject);
            manager.BonusDestroyed();
        }
    }
}
