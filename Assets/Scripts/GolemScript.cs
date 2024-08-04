using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : Enemy
{
    public static GolemScript Instance;

    [SerializeField] GameObject slamEffect;
    public Transform SideAttackTransform;
    public Vector2 SideAttackArea;

    public Transform UpAttackTransform;
    public Vector2 UpAttackArea;
    
    public Transform DownAttackTransform;
    public Vector2 DownAttackArea;

    public float attackRange;
    public float attackTimer;

    [HideInInspector] public bool facingRight;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;

    int hitCounter;
    bool alive;

    [HideInInspector] public float runSpeed;

    protected override void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        ChangeState(EnemyStates.Golem_Stage1);
        alive = true;
    }

    public bool Grounded()
    {
        if (Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(SideAttackTransform.position, SideAttackArea);
        Gizmos.DrawWireCube(UpAttackTransform.position, UpAttackArea);
        Gizmos.DrawWireCube(DownAttackTransform.position, DownAttackArea);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(health <=0 && alive)
        {
            Death(0);
        }

        if(!attacking)
        {
            attackCountDown -= Time.deltaTime;
        }
    }

    public void Flip()
    {
        if(PlayerController.Instance.transform.position.x < transform.position.x && transform.localScale.x > 0)
        {
            transform.eulerAngles = new Vector2(transform.eulerAngles.x, 180);
            facingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector2(transform.eulerAngles.x, 0);
            facingRight = true;
        }
    }

    protected override void UpdateEnemyStates()
    {
        if(PlayerController.Instance != null)
        {
            switch(GetCurrentEnemyState)
            {
                case EnemyStates.Golem_Stage1:
                    attackTimer = 4;
                    runSpeed = speed;
                    break;
            }
        }
    }

    protected override void OnCollisionStay2D(Collision2D _other)
    {

    }
    #region attacking
    #region variables
    [HideInInspector] public bool attacking;
    [HideInInspector] public float attackCountDown;

    #endregion

    #region Control

    public void AttackHandler()
    {
        if(currentEnemyState == EnemyStates.Golem_Stage1)
        {
            if(Vector2.Distance(PlayerController.Instance.transform.position, rb.position) <= attackRange)
            {
                StartCoroutine(SlamAttack());
            }
            else
            {
                return;
            }
        }
    }
    public void ResetAllAttacks()
    {
        attacking = false;

        StopCoroutine(SlamAttack());
    }
    #endregion

    #region Stage 1

    IEnumerator SlamAttack()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Slam");
        SlamAngle();
        yield return new WaitForSeconds(1f);
        anim.ResetTrigger("Slam");

        ResetAllAttacks();
    }

    void SlamAngle()
    {
        if(PlayerController.Instance.transform.position.x > transform.position.x ||
                PlayerController.Instance.transform.position.x < transform.position.x)
        {
            Instantiate(slamEffect, SideAttackTransform);
        }
    }

    #endregion

    #endregion

    protected override void Death(float _destroyTime)
    {
        ResetAllAttacks();
        alive = false;
        rb.velocity = new Vector2(rb.velocity.x, -25);
        anim.SetTrigger("Death");
    }

    public void DestroyAfterDeath()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {

            Debug.Log("Enemy started colliding with rock");


            this.health -= 10;

        }
    }
}
