using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseController : DecisionManager  
{
    [HideInInspector]
    public UnityEvent<Vector2> OnMouseMoved;
    [HideInInspector]
    public GameObject grabbedObject = null;
    
    private void Update()
    {
        DecisionManagerUpdate();
        OnMouseMoved.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
