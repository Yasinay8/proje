using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
    public float speed;
    public float detectAreaRadius;
    public float attackAreaRadius;

    public bool isShouldRotate;
    public LayerMask playerLayer;
    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 dir;
    public GameObject attackPoint;
    public float attackRange = 0.5f;
    public bool isAttacking;
    public bool isResting;
    public int damage = 1;
    public GameObject attackPositionRight;
    public GameObject attackPositionLeft;
    public GameObject attackPositionUp;
    public GameObject attackPositionDown;

    private bool isInChaseRange;
    private bool isInAttackRange;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        anim.SetBool("isRunning", isInChaseRange);
        anim.SetBool("isAttacking", isInAttackRange && isAttacking == false);

        isInChaseRange = Physics2D.OverlapCircle(transform.position, detectAreaRadius, playerLayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackAreaRadius, playerLayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if (isShouldRotate)
        {
            anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);
        }
    }

    private void FixedUpdate()
    {
        if (!isResting)
        {
            if (isInChaseRange && !isInAttackRange)
            {
                MoveCharacter(movement);
            }
            if (isInAttackRange)
            {
                Attack();
            }
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + dir * speed * Time.deltaTime);
    }

    public void Attack()
    {
        rb.velocity = Vector2.zero;
        StartCoroutine(AttackTime());
        /*
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, playerLayer);
        foreach (Collider2D target in hitPlayer)
        {
            if (target.tag == "Player")
            {
                target.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
        */
    }


    IEnumerator AttackTime()
    {
        isResting = true;
        yield return new WaitForSecondsRealtime(2f);
        isResting = false;
    }
}
