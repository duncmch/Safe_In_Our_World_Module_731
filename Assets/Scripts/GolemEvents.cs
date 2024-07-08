using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEvents : MonoBehaviour
{
    void SlamDamagePlayer()
    {
        if (PlayerController.Instance.transform.position.x > transform.position.x ||
            PlayerController.Instance.transform.position.x < transform.position.x)
        {
            Hit(GolemScript.Instance.SideAttackTransform, GolemScript.Instance.SideAttackArea);
        }
    }
    void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider2D[] _objectsToHit = Physics2D.OverlapBoxAll(_attackTransform.position, _attackArea, 0);
        for(int i = 0;  i < _objectsToHit.Length; i++)
        {
            if (_objectsToHit[i].GetComponent<PlayerController>() != null)
            {
                _objectsToHit[i].GetComponent<PlayerController>().TakeDamage(GolemScript.Instance.damage);
            }
        }
    }

    void DestroyAfterDeath()
    {
        GolemScript.Instance.DestroyAfterDeath();
    }
}
