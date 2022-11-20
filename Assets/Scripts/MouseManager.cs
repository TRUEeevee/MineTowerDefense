using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object that can be clicked by MouseManager
/// </summary>
    interface IClickableObject
    {
    /// <summary>
    /// The object was clicked by MouseManager.
    /// </summary>
    /// <param name="mousePosition3D">The postiion, including depth, of the point
    /// under the mouse cursor</param>
    void OnClick( Vector3 mousePosition3D );
}
public class MouseManager : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    private void Update( )
    {
        // If the mouse button is down, call TestMouseClick.
        // We're testing instantly. If you want to only check for when the mouse cursor was pressed
        // down on an object, then lifted up on that same object, just keep track of the GameObject
        // the cursor was pressed on, and call OnClick on mouse up.
        if ( Input.GetMouseButtonDown( 0 ) )
            TestMouseClick();
    }

    private void TestMouseClick( )
    {
        // Get a ray representing the mouse cursor from the current camera.
        // m_camera should be a reference to the game's main camera you're seeing the objects from.
        Ray ray = m_camera.ScreenPointToRay( Input.mousePosition );

        // Create a layer mask containing just the objects you want to click.
        // This is so that if there's a non-clickable collider, it won't obstruct
        // a clickable collider. If you want a collider to block mouse click,
        // add it here.
        int layerMask = LayerMask.GetMask("UI", "Tower", "Map");

        // Perform the raycast.
        RaycastHit raycastHit;
        if ( Physics.Raycast( ray, out raycastHit, Mathf.Infinity, layerMask ) )
        { // If something was hit:

            // Get every MonoBehaviour sibling of the topmost collider under the mouse cursor which
            // is an IClickableObject
            var hitClickableObjects = raycastHit.collider.GetComponents<IClickableObject>();

            // Call OnClick in each of them. This is so that if multiple MonoBehaviours on the single
            // clicked GameObject exist, OnClick will be called for all of them.
            foreach ( var clickableObject in hitClickableObjects )
                // We're passing RaycastHit.point to the method. You can obviously define
                // the interface to better suit your needs.
                clickableObject.OnClick( raycastHit.point ); 
        }
    }
}






 /* 
  * This code was found from this forum question
  * https://answers.unity.com/questions/1546623/how-to-click-in-only-one-object-at-a-time.html 
  */