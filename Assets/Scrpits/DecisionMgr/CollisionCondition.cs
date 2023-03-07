using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCondition : Condition
{
    public string Tag;
    public bool collisionEnter;
    public bool collisionStay;
    public bool collisionExit;


    private bool isCollisionEnter;
    private bool isCollisionStay;
    private bool isCollisionExit;

    private void OnCollisionEnter(Collision collision)
    {
        if (Tag == "" || collision.collider.tag == Tag)
        {
            isCollisionEnter = true;
            Debug.Log(Tag);

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Tag == "" || collision.collider.tag == Tag)
            isCollisionStay = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (Tag == "" || collision.collider.tag == Tag)
        {
            isCollisionStay = false;
            isCollisionExit = true;
        }
    }

    private bool GetOnEnter()
    {
        if (isCollisionEnter)
        {
            isCollisionEnter = false;
            return true;
        }
        return false;
    }

    private bool GetOnExit()
    {
        if (isCollisionExit)
        {
            isCollisionExit = false;
            return true;
        }
        return false;
    }

    public override bool Eval()
    {
        return (collisionEnter && GetOnEnter()) || 
            (collisionStay && isCollisionStay) || 
            (collisionExit && GetOnExit());
    }
}

