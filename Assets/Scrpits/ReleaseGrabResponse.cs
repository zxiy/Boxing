using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseGrabResponse : Response
{
    public override void Dispatch()
    {
        MouseController mc = GetComponent<MouseController>();
        if (mc.grabbedObject != null)
        {
            mc.grabbedObject.GetComponent<ItemController>().OnRelease(mc);
            mc.grabbedObject = null;
        }
    }
}
