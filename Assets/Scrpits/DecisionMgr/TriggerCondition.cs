using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCondition : Condition
{
    public string Tag;
    public bool triggerEnter;
    public bool triggerStay;
    public bool triggerExit;


    private bool isTriggerEnter;
    private bool isTriggerStay;
    private bool isTriggerExit;

    private void OnTriggerEnter(Collider trigger)
    {
        if (Tag == "" || trigger.tag == Tag)
            isTriggerEnter = true;
    }

    private void OnTriggerStay(Collider trigger)
    {
        if (Tag == "" || trigger.tag == Tag)
            isTriggerStay = true;
    }

    private void OnTriggerExit(Collider trigger)
    {
        if (Tag == "" || trigger.tag == Tag)
        {
            isTriggerStay = false;
            isTriggerExit = true;
        }
    }

    private bool GetOnEnter()
    {
        if (isTriggerEnter)
        {
            isTriggerEnter = false;
            return true;
        }
        return false;
    }

    private bool GetOnExit()
    {
        if (isTriggerExit)
        {
            isTriggerExit = false;
            return true;
        }
        return false;
    }

    public override bool Eval()
    {
        return (triggerEnter && GetOnEnter()) || 
            (triggerStay && isTriggerStay) || 
            (triggerExit && GetOnExit());
    }
}

