using UnityEngine;
using System.Collections;

/**
 * This class is used to enable a player to change the field 
 * https://www.youtube.com/watch?v=1j9McAMK4lE
 * see http://answers.unity3d.com/questions/11093/modifying-terrain-height-under-a-gameobject-at-run.html
 * Cette classe permet de créer un rayon partant de la caméra en direction de la position du curseur dans l'environnement 3D.
 * L'objet portant se script peut saisir des objets, les manipuler et les déplacer grace au clique gauche de la souris.
 * Trois curseurs sont implémentés : 
 * Un curseur cursorOff lorsqu'aucun objet manipulable (tag "Draggable") n'est détecté par le rayon.
 * Un curseur cursorDraggable lorsqu'un objet manipulable est détécté mais non saisi
 * Un curseur cursorDragged lorsqu'un obhet est saisi
**/
public class RayCasting : MonoBehaviour
{
    private const string LOG_TAG = "RayCasting - ";

    public const int RAYCASTLENGTH = 1000;   // Longueur du rayon issu de la caméra
	public GameObject pointerFinger;
	public GameObject pointStart;

    //public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = new Vector2(16, 16);   // Offset du centre du curseur
   // public Texture2D cursorOff, cursorDragged, cursorDraggable; // Textures à appliquer aux curseurs

    void Start()
    {
       // Cursor.SetCursor(cursorOff, hotSpot, cursorMode);
        //Cursor.visible = true;
    }

    void Update()
    {
		Ray ray = new Ray(pointStart.transform.position, pointerFinger.transform.position+ new Vector3(0f,0.2f,0f) - pointStart.transform.position);
		Debug.DrawRay(ray.origin, ray.direction * RAYCASTLENGTH, Color.blue);

		if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit hit;

			//Ray ray = (Ray)pointerFinger.transform;
			//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast (ray, out hit)) 
			{
				GetComponent<RaiseLowerTerrain>().riseController(hit.point);
			}
		}


    }
}