using UnityEngine;
using System.Collections;

public class BallTrigger : MonoBehaviour {
    private const string LOG_TAG = "BallTrigger - ";


    private GameObject lastPlayerTouched;
    public BonusGenerator manager;

    private const string PLAYER_TAG = "Player";
    private const string BONUS_SHIELD_TAG = "BonusShield";
	private const string BONUS_TERRAIN_TAG = "BonusTerrain";
	private const string BONUS_PROJECTILE_TAG = "BonusProjectile";
	private const string BONUS_BALL_TRAP_TAG = "BonusBallTrap";




    // Use this for initialization
    void Start () {
        lastPlayerTouched = null;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
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
		else if (other.gameObject.CompareTag(BONUS_TERRAIN_TAG))
		{
			Debug.Log(LOG_TAG + "Destruction bonus terrain/mountain");
			Destroy(other.gameObject);
			manager.BonusDestroyed();
			if (lastPlayerTouched != null) {
				Debug.Log(LOG_TAG + "Power terrain send to player");
				lastPlayerTouched.GetComponent<BonusPlayerManager>().activateTerrainPower();
			}
		}
		else if (other.gameObject.CompareTag(BONUS_PROJECTILE_TAG))
		{
			Debug.Log(LOG_TAG + "Destruction bonus projectile");
			Destroy(other.gameObject);
			manager.BonusDestroyed();
			if (lastPlayerTouched != null) {
				Debug.Log(LOG_TAG + "Power projectile send to player");
				lastPlayerTouched.GetComponent<BonusPlayerManager>().activateProjectilePower();
			}
		}
		else if (other.gameObject.CompareTag(BONUS_BALL_TRAP_TAG))
		{
			Debug.Log(LOG_TAG + "Destruction bonus ball trap");
			Destroy(other.gameObject);
			manager.BonusDestroyed();
			if (lastPlayerTouched != null) {
				Debug.Log(LOG_TAG + "Power ball trap send to player");
				lastPlayerTouched.GetComponent<BonusPlayerManager>().activateBallTrapPower();
			}
		}
        else if (other.gameObject.CompareTag(PLAYER_TAG)) {
            Debug.Log(LOG_TAG + "Save last player who touched the ball");
            lastPlayerTouched = other.gameObject;

        }

        
    }
	

}
