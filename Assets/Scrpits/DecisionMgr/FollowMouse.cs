using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : Response
{
    public override void Dispatch()
    {
        // Get the mouse position
        Vector3 mousePos = Input.mousePosition;
        // Convert the mouse position to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        // Set the position of the game object to the world position
        transform.position = worldPos;
    }
}
