using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] LayerMask mask;
    [SerializeField] private UpgradeScript upgradeScript;
    private Ray ray;


    private void Update( )
    {
        if (Input.GetMouseButtonDown(0))  //While button is down, run mouse click.
        {
            MouseClick();
        }
    }

    private void MouseClick( )                                                                      //**************************************
    {
        Vector3 temp = mainCamera.ScreenToWorldPoint(Input.mousePosition);      //Grabbing the point in world space that we clicked
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);                 //Setting the rays origin using this method  (In normal circumstances this would also set its direction, but our scene is backwards.
        ray.direction = new Vector3(0, 0, 1);                                   //LayerMask layerMask = LayerMask.GetMask("Tower");                                                                        
                                                                                //So we need to flip it. I do this here by artificially setting its Z coordinate, since you cannot vector math a Ray, and raycasting
                                                                                //cannot be changed in game time.      
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, new Vector3(0, 0, 1), Mathf.Infinity);

        if (hit)  //
        {
            Debug.Log("Clicked on " + hit.collider.gameObject.name); 

            //change to switch statement to check for which object is being clicked, and handle appropriately
            upgradeScript.OpenUI(hit.collider.gameObject);
        }
        ///                                                                     ****************    Note from Nate                                                    
        ///                                                                     ****************    UI handles clicks differently, we do not need to interact with it in the same way unless our game components                                                     
        ///                                                                                         themselves need to be interactable in the UI (This might be the case for the idle mechanics eventually)                             
        ///                                                                                         The UI should instead act as a click BLOCKER, as you dont wanna click objects behind the UI accidentally 
    }
    public void OnDrawGizmos() // This displays kinda whats happening in scene view when the game is running. Organising these gizmos will be crucial for proper debugging later on.
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(ray);
    }
}