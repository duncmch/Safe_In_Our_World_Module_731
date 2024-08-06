using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatformItem : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine(DestroyPlatform());
    }

    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
}
