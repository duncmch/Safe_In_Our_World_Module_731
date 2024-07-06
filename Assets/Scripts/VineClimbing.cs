using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineClimbing : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isVine;
    private bool isCimbing;

    [SerializeField] private Rigidbody2D rb;


    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isVine && Mathf.Abs(vertical) > 0f)
        {
            isCimbing = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vine"))
        {
            isVine = true;
        }
    }

    private void FixedUpdate()
    {
        if (isCimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 9.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Vine"))
        {
            isVine = false;
            isCimbing = false;
        }
    }
}
