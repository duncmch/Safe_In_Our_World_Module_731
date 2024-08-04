using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDamageItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            StartCoroutine(DestroyRockAfterTouch());
        }
        else
        {
            StartCoroutine(DestroyRockAfterTouch());
        }
    }

    IEnumerator DestroyRockAfterTouch()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
