using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Animator animator;

    private float fallDelay = 1f;
    private float destroyDelay = 2f;

    [SerializeField] private Rigidbody2D rb;

    public Vector2 initialPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("FallStart");
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.None;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        transform.position = initialPosition;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        StopCoroutine(Fall());
    }
}
