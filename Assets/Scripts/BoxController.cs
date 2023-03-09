using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private GUIController guiController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        int valueChange = 0;
        if (other.CompareTag("Items"))
        {
            if (other.TryGetComponent(typeof(Item_Movement_Circle_Body), out var icb))
            {
                valueChange = other.GetComponent<Item_Movement_Circle_Body>().pointValue;
            }
            else if (other.TryGetComponent(typeof(Item_Movement_Box_Body), out var ibb))
            {
                valueChange = other.GetComponent<Item_Movement_Box_Body>().pointValue;
            }
            guiController.onScoreChanged.Invoke(other.gameObject, valueChange);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        int valueChange = 0;
        if (other.CompareTag("Items"))
        {
            if (other.TryGetComponent(typeof(Item_Movement_Circle_Body), out var icb))
            {
                valueChange = -other.GetComponent<Item_Movement_Circle_Body>().pointValue;
            }
            else if (other.TryGetComponent(typeof(Item_Movement_Box_Body), out var ibb))
            {
                valueChange = -other.GetComponent<Item_Movement_Box_Body>().pointValue;
            }
            guiController.onScoreChanged.Invoke(other.gameObject, valueChange);
        }
    }
}
