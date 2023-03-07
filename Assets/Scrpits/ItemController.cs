using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class ItemController : DecisionManager
{
    [SerializeField]
    private Vector3 originalPos;
    private Vector3Int lastPos;
    private bool canPutDown = false;
    public Tilemap avaliableTilemap;
    public Grid grid;

    
    public void OnGrab(DecisionManager dm)
    {
        MouseController mc = dm.gameObject.GetComponent<MouseController>();
        
        if (mc != null)
        {
            mc.OnMouseMoved.AddListener(OnMouseMoved);
        }
    }
    
    public void OnRelease(DecisionManager dm)
    {
        MouseController mc = dm.gameObject.GetComponent<MouseController>();
        
        if (mc != null)
        {
            mc.OnMouseMoved.RemoveListener(OnMouseMoved);
        }
        
        if (!canPutDown)
        {
            transform.position = originalPos;
        }
    }
    
    private void OnMouseMoved(Vector2 pos)
    {
        var newPos = grid.WorldToCell(pos);
        if (newPos != lastPos)
        {
            transform.position = new Vector2(newPos.x + 0.5f, newPos.y + 0.5f);
            lastPos = newPos;
            if (avaliableTilemap.HasTile(newPos))
            {
                canPutDown = true;
            }
            else
            {
                canPutDown = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        canPutDown = false;
    }   

    private void OnCollisionExit2D(Collision2D other)
    {
        canPutDown = true;
    }
}
