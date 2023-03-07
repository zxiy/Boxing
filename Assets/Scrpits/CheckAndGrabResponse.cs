using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckAndGrabResponse : Response
{
    private Grid grid;
    private MouseController mc;
    private void Start()
    {
        mc = GetComponent<MouseController>(); 
    }

    public override void Dispatch()
    {
        if (mc.grabbedObject != null)
        {
            return;
        }
        
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction, 100f, 1 << LayerMask.NameToLayer("Item"));
        if (hit.collider != null)
        {
            var c = hit.collider.gameObject;
            mc.grabbedObject = c;
            c.GetComponent<ItemController>().OnGrab(mc);
        } 
    }
}
