using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// moves parent object this much
public class TranslateResponse : Response
{
    public Vector3 vTrans;

    public override void Dispatch()
    {
        this.transform.root.Translate(vTrans * Time.deltaTime);
    }
}
