using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    private IEnumerator coroutine;
    
    public GameObject laptopOpen;
    
    Rigidbody2D rigidbody2DComponent;

    private void Awake()
    {
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
    }

    void OnMouseUp()
    {
        // check if the laptop was thrown
        if (rigidbody2DComponent.velocity.magnitude >= 3f)
        {
            // have it open when it lands
            Invoke ("OpenOnLand", 0.3f);
        }
    }

    private void OpenOnLand()
    {
        GameObject openedLaptop = Instantiate(laptopOpen, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
