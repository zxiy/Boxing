using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float conveyorSpeed;

    // moves all objects that are touching the belt to the right
    void OnTriggerStay2D(Collider2D other)
    {
            other.transform.position = new Vector3(other.transform.position.x + conveyorSpeed, other.transform.position.y, other.transform.position.z);
    }

    public void SpeedUp()
    {
        conveyorSpeed += 0.03f;
    }
}
