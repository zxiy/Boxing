using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy_Object : MonoBehaviour
{
    Rigidbody2D rigidbody2DComponent;
    BoxCollider2D boxCollider2DComponent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isFragile;
        if (collision.gameObject.GetComponent<Item_Movement_Box_Body>() != null)
        {
            isFragile = collision.gameObject.GetComponent<Item_Movement_Box_Body>().fragile;
        }
        else
        {
            isFragile = collision.gameObject.GetComponent<Item_Movement_Circle_Body>().fragile;
        }
        if (isFragile == true)
        {
            collision.gameObject.SendMessage("BreakCheck");
        }
    }
}
