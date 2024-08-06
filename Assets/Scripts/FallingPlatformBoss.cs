using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformBoss : MonoBehaviour
{
    private Animator animator;

    private float fallDelay = 5f;
    private float destroyDelay = 2f;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        yield return new WaitForSeconds(4f);
        animator.SetTrigger("FallStart");
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}
