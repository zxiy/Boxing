using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)),
    RequireComponent(typeof(Rigidbody2D))]
public class Item_Movement_Circle_Body : MonoBehaviour
{
    Vector3 lastPosition0;
    Vector3 lastPosition1;
    Vector3 lastPosition2;
    public float initialX; //for testing purposes only
    public float initialY; //for testing purposes only
    public float speed;
    private IEnumerator coroutine;

    public bool isFlying;

    Rigidbody2D rigidbody2DComponent;
    CircleCollider2D circleCollider2DComponent;

    void Awake()
    {
        // get references to components
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
        circleCollider2DComponent = GetComponent<CircleCollider2D>();

    }
    private void FixedUpdate()
    {
        lastPosition2 = lastPosition1;
        lastPosition1 = lastPosition0;
        lastPosition0 = transform.position;

    }

    private void OnMouseDown()
    {
        // if flying, return early -- do nothing
        if (isFlying) return;
        // turn off colision
        circleCollider2DComponent.enabled = false;
    }

    void OnMouseDrag()
    {
        // if flying, return early -- do nothing
        if (isFlying) return;

        Vector2 mousePositionInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // if the key is clicked, have key follow mouse
        transform.position = new Vector3(Mathf.Clamp(mousePositionInWorldSpace.x, -7f, 7f), Mathf.Clamp(mousePositionInWorldSpace.y, -4.5f, 2.4f), -0.1f);
    }

    void OnMouseUp()
    {
        // if flying, return early -- do nothing
        if (isFlying) return;

        // enable physics on the object
        rigidbody2DComponent.bodyType = RigidbodyType2D.Dynamic;

        // get the direction of object relative to its latest different position
        Vector3 directionTowardLastPosition;
        if (transform.position != lastPosition0)
        {
            directionTowardLastPosition = transform.position - lastPosition0;
        }
        else if (transform.position != lastPosition1)
        {
            directionTowardLastPosition = transform.position - lastPosition1;
        }
        else
        {
            directionTowardLastPosition = transform.position - lastPosition2;
        }

        // apply an impulse force to the object
        rigidbody2DComponent.AddForce(directionTowardLastPosition * speed, ForceMode2D.Impulse);

        // apply a throw effect if it was thrown
        if (rigidbody2DComponent.velocity.magnitude >= 3f)
        {
            // grow the object a little 
            transform.localScale = new Vector3(transform.localScale.x + 1f, transform.localScale.y + 1f, transform.localScale.z);

            // grow the object again later
            Invoke("Grow", 0.1f);

            // shrink the object after that
            Invoke("Shrink", 0.2f);

            // turn collision back on in 0.3 second and return it to normal size
            coroutine = Land(true, 0.3f);
            StartCoroutine(coroutine);
        }
        // land if it wasn't thrown
        else
        {
            // turn collision back on in 0.1 second
            coroutine = Land(false, 0f);
            StartCoroutine(coroutine);
        }

        // prevent the object from being clickable until it lands
        isFlying = true;

    }

    private void Grow()
    {
        transform.localScale = new Vector3(transform.localScale.x + 1f, transform.localScale.y + 1f, transform.localScale.z);
    }

    private void Shrink()
    {
        transform.localScale = new Vector3(transform.localScale.x - 1f, transform.localScale.y - 1f, transform.localScale.z);
    }

    private IEnumerator Land(bool thrown, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (thrown == true)
        {
            transform.localScale = new Vector3(transform.localScale.x - 1f, transform.localScale.y - 1f, transform.localScale.z);
        }
        isFlying = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        circleCollider2DComponent.enabled = true;
    }
}