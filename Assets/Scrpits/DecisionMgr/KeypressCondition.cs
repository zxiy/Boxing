using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// returns true if selected key is pressed
public class KeypressCondition : Condition
{
    public KeyCode key_to_check;
    public bool keydown;
    public bool keyheld;
    public bool keyup;

    public override bool Eval()
    {
        return ((keydown && Input.GetKeyDown(key_to_check))
                || (keyheld && Input.GetKey(key_to_check))
                || (keyup && Input.GetKeyUp(key_to_check)));
    }
}
