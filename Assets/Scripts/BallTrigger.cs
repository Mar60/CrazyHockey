using UnityEngine;
using System.Collections;

public class BallTrigger : MonoBehaviour {
    private const string LOG_TAG = "BallTrigger - ";


    private GameObject lastPlayerTouched;
    public BonusGenerator manager;

    private const string PLAYER_TAG = "Player";
    private const string BONUS_SHIELD_TAG = "BonusShield";


    // Use this for initialization
    void Start () {
        lastPlayerTouched = null;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collideEvent)
    {
        if (other.gameObject.CompareTag(BONUS_SHIELD_TAG))
        {
            Debug.Log(LOG_TAG + "Destruction bonus shield");
            Destroy(other.gameObject);
            manager.BonusDestroyed();
            if (lastPlayerTouched != null) {
                Debug.Log(LOG_TAG + "Power shield send to player");
                lastPlayerTouched.GetComponent<BonusPlayerManager>().activateShieldPower();
            }
        }
        else if (other.gameObject.CompareTag(PLAYER_TAG)) {
            Debug.Log(LOG_TAG + "Save last player who touched the ball");
            lastPlayerTouched = other.gameObject;

        }

        
    }

}
