using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbInstructions : MonoBehaviour

{
    [SerializeField] GameObject particles;
    [SerializeField] GameObject canvasUI;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.Instance.unlockVarJump)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            StartCoroutine(ShowUI());
        }
    }

    IEnumerator ShowUI()
    {
        GameObject _particles = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(_particles, 0.5f);
        yield return new WaitForSeconds(0.5f);

        canvasUI.SetActive(true);

        yield return new WaitForSeconds(4f);
        canvasUI.SetActive(false);
        Destroy(gameObject);
    }
}
