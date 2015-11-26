using UnityEngine;
using System.Collections;

/**
 * This class is used to enable a player to change the field 
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

    private float distanceToObj;    // Distance entre le personnage et l'objet saisi
    private Rigidbody attachedObject;   // Objet saisi, null si aucun objet saisi

    public const int RAYCASTLENGTH = 100;   // Longueur du rayon issu de la caméra


    //public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = new Vector2(16, 16);   // Offset du centre du curseur
   // public Texture2D cursorOff, cursorDragged, cursorDraggable; // Textures à appliquer aux curseurs

    void Start()
    {
        distanceToObj = -1;
       // Cursor.SetCursor(cursorOff, hotSpot, cursorMode);
        //Cursor.visible = true;
    }

    void Update()
    {
        // Le raycast attache un objet cliqué
        RaycastHit hitInfo;
        Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * RAYCASTLENGTH, Color.blue);
        bool rayCasted = Physics.Raycast(ray, out hitInfo, RAYCASTLENGTH);

        if (rayCasted)
        {
            rayCasted = hitInfo.transform.CompareTag("Draggable");
        }
        // rayCasted est true si un objet possédant le tag draggable est détécté

        if (Input.GetMouseButtonDown(0))    // L'utilisateur vient de cliquer
        {
            if (rayCasted)
            {
                Debug.Log(LOG_TAG + "Object attached");
                attachedObject = hitInfo.rigidbody; //On recupere le rigidbody pour saisir l'objet
				//TODO Terrain.terrainData.GetHeights(xBase,yBase,xRes);
				//TODO SetHeights() https://www.youtube.com/watch?v=1j9McAMK4lE
				//attachedObject.isKinematic = true;
                //distanceToObj = hitInfo.distance;
               // Cursor.SetCursor(cursorDragged, hotSpot, cursorMode);
            }
        }

        else if (Input.GetMouseButtonUp(0) && attachedObject != null)   // L'utilisateur relache un objet saisi
        {
            attachedObject.isKinematic = false;
            attachedObject = null;
            Debug.Log(LOG_TAG + "Object detached");
            /*if (rayCasted)
            {
                Cursor.SetCursor(cursorDraggable, hotSpot, cursorMode);
            }
            else
            {
                Cursor.SetCursor(cursorOff, hotSpot, cursorMode);
            }*/
        }

        if (Input.GetMouseButton(0) && attachedObject != null) // L'utilisateur continue la saisie d'un objet
        {
            attachedObject.MovePosition(ray.origin + (ray.direction * distanceToObj)); //deplacer l'objet
        }

        else  // L'utilisateur bouge la sourie sans cliquer 
        {
           /* if (rayCasted)
            {
                Cursor.SetCursor(cursorDraggable, hotSpot, cursorMode);
            }
            else
            {
                Cursor.SetCursor(cursorOff, hotSpot, cursorMode);
            }*/
        }
    }
}