using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// returns true, so that response can be dispatched
// good for debug, kind of trivial
public class TrueCondition : Condition
{
    public override bool Eval()
    {
        return true;
    }

}
