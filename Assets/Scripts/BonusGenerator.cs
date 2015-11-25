using UnityEngine;
using System.Collections;

public class BonusGenerator : MonoBehaviour {
    private const string LOG_TAG = "BonusGenerator - ";


    const int MAXITEMS = 5; // Maximum number of items in the scene
    private int bonusCounter; // Current number of items in the scene
    public GameObject field; // The field of the scene
    Bounds fieldBounds; // Bounds of the fields

    public GameObject bonusPrefab; // the bonus prefab
    private Vector3 bonusSize; // The size of the bonus

    // Use this for initialization
    void Start () {
        bonusCounter = 0;
        // bounds of the field
        fieldBounds = field.GetComponent<Renderer>().bounds;
        // take the size of one items
        bonusSize = bonusPrefab.GetComponent<Renderer>().bounds.size;
    }
	
	// Update is called once per frame
	void Update () {
        if (bonusCounter < MAXITEMS)
        {
            float x = Random.Range(fieldBounds.min.x, fieldBounds.max.x);
            float y = fieldBounds.max.y + bonusSize.y / 2;
            float z = (fieldBounds.min.z + fieldBounds.max.z )/ 2; //we put the bonus in the middle of the field
            Vector3 bonusPosition = new Vector3(x, y, z);
            Debug.Log(LOG_TAG + "Creation bonus  (" + x + ", " + y + " , " + z + ")"); 
            Instantiate(bonusPrefab, bonusPosition, Quaternion.identity);
            bonusCounter++;
        }
    }

    // Appelé par BallController lors de la destruction d'un cube
    public void BonusDestroyed()
    {
        bonusCounter--;
    }
}
