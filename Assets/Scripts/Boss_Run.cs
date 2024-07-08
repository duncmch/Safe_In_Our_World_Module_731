using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponentInParent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TargetPlayerPosition(animator);

        if(GolemScript.Instance.attackCountDown <= 0)
        {
            GolemScript.Instance.AttackHandler();
            GolemScript.Instance.attackCountDown = Random.Range(GolemScript.Instance.attackTimer - 1, GolemScript.Instance.attackTimer + 1);
        }
    }
    void TargetPlayerPosition(Animator animator)
    {
        if(GolemScript.Instance.Grounded())
        {
            GolemScript.Instance.Flip();
            Vector2 _target = new Vector2(PlayerController.Instance.transform.position.x, rb.position.y);
            Vector2 _newPos = Vector2.MoveTowards(rb.position, _target, GolemScript.Instance.runSpeed * Time.deltaTime);

            rb.MovePosition(_newPos);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -25); // if boss is not grounded fall to ground
        }

        if(Vector2.Distance(PlayerController.Instance.transform.position, rb.position) <= GolemScript.Instance.attackRange)
        {
            animator.SetBool("Walk", false);
        }
        else
        {
            return;
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Walk", false);
    }
}
