using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  base behavior for a condition that can be evalutated, and responded to
//
public abstract class Condition : MonoBehaviour {

    abstract public bool Eval();

}
