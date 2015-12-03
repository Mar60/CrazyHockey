using UnityEngine;
using System.Collections;

public class BonusGenerator : MonoBehaviour {
    private const string LOG_TAG = "BonusGenerator - ";



    public const int MAXITEMS = 3; // Maximum number of items in the scene
    private int bonusCounter; // Current number of items in the scene
    public GameObject field; // The field of the scene
    Bounds fieldBounds; // Bounds of the fields

	private float INCERTITUDE_SIZE_OBJECT =2f;

	private float SIZE_WALL =5f;

    public GameObject bonusShieldPrefab; // the bonus shield prefab
	public GameObject bonusMountainPrefab; // the bonus mountain prefab
	public GameObject bonusProjectilePrefab; // the bonus projectile prefab
	public GameObject bonusBallTrapPrefab; // the bonus projectile prefab
	public GameObject bonusMagnetPrefab; // the bonus magnet prefab



	private Vector3 bonusSize; // The size of the bonus

    // Use this for initialization
    void Start () {
        bonusCounter = 0;
        // bounds of the field
        fieldBounds = field.GetComponent<Renderer>().bounds;
<<<<<<< HEAD
        // take the size of one items
        //bonusSize = bonusPrefab.GetComponent<Renderer>().bounds.size;
        Debug.Log(fieldBounds);
=======

>>>>>>> origin/master
    }
	
	// Update is called once per frame
	void Update () {
        if (bonusCounter < MAXITEMS)
        {
            float x = Random.Range(fieldBounds.min.x, fieldBounds.max.x);
            float y = fieldBounds.min.y + bonusSize.y;
            float z = (fieldBounds.min.z + fieldBounds.max.z )/ 2; //we put the bonus in the middle of the field
            Vector3 bonusPosition = new Vector3(x, y, z);
           // Debug.Log(LOG_TAG + "Creation bonus  (" + x + ", " + y + " , " + z + ")"); 
            //Instantiate(bonusPrefab, bonusPosition, Quaternion.identity);
            bonusCounter++;
			int choiceBonus = Random.Range(1,5);
			switch(choiceBonus){
				case 1 ://Shield bonus
					Debug.Log(LOG_TAG + "Creation shield bonus "); 
					createBonusItem(bonusShieldPrefab, bonusShieldPrefab.GetComponent<Renderer>().bounds.size);
					break;
				case 2 ://mountain bonus
					Debug.Log(LOG_TAG + "Creation mountain bonus"); 
					createBonusItem(bonusMountainPrefab, bonusMountainPrefab.GetComponent<Renderer>().bounds.size);
					break;
				case 3 ://projectile bonus
					Debug.Log(LOG_TAG + "Creation projectile bonus "); 
					createBonusItem(bonusProjectilePrefab, bonusProjectilePrefab.GetComponent<Renderer>().bounds.size);
					break;
				case 4 ://ball trap bonus
					Debug.Log(LOG_TAG + "Creation ball trap bonus "); 
					createBonusItem(bonusBallTrapPrefab, bonusBallTrapPrefab.GetComponent<Renderer>().bounds.size);
					break;
				case 5 ://magnet bonus
					Debug.Log(LOG_TAG + "Creation magnet bonus "); 
					createBonusItem(bonusMagnetPrefab, bonusBallTrapPrefab.GetComponent<Renderer>().bounds.size);
					break;
				default :
					Debug.Log(LOG_TAG + "Creation none of them bonus "); 
					break;
			}
        }
    }


	private void createBonusItem(GameObject bonusPrefabUsed ,Vector3 bonusSizeUsed){
		// take the size of one items
		float x = Random.Range(fieldBounds.min.x+SIZE_WALL, fieldBounds.max.x-SIZE_WALL);
		float y = fieldBounds.min.y + bonusSizeUsed.y+ INCERTITUDE_SIZE_OBJECT;
		float z = (fieldBounds.min.z + fieldBounds.max.z )/ 2; //we put the bonus in the middle of the field
		Vector3 bonusPosition = new Vector3(x, y, z);
		Debug.Log(LOG_TAG + "Creation bonus  (" + x + ", " + y + " , " + z + ")"); 

		Instantiate(bonusPrefabUsed, bonusPosition, Quaternion.identity);
		bonusCounter++;
	}

    // Appelé par BallController lors de la destruction d'un cube
    public void BonusDestroyed()
    {
        bonusCounter--;
    }


}
