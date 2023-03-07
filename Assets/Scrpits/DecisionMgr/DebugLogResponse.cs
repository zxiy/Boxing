using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogResponse : Response
{
    public string message;

    public override void Dispatch()
    {
        Debug.Log(message);
    }
}
