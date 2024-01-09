using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer renderer;
    private HeroRangedAttack throwingProjectile;
    public GameObject projectile;
    int direction = 1;

    public int speed;
    public int damage;

    bool isOnAttack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnAttack == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                GoRight();
                direction = 1;
            }
            if (Input.GetKeyUp(KeyCode.D))
                GoIdle();
            if (Input.GetKeyDown(KeyCode.A))
            {
                GoLeft();
                direction = -1;
            }
            if (Input.GetKeyUp(KeyCode.A))
                GoIdle();

            if (Input.GetKeyDown(KeyCode.E))
            {
                RangedAttack();
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
            StartCoroutine(GoRegularAttack());
    }

    public void GoRight()
    {
        rb.velocity = new Vector2(speed, 0);
        anim.SetBool("isWalking", true);
        anim.SetBool("isIdle", false);
        renderer.flipX = false;
    }
    public void GoLeft()
    {
        rb.velocity = new Vector2(-speed, 0);
        anim.SetBool("isWalking", true);
        anim.SetBool("isIdle", false);
        renderer.flipX = true;
    }

    public void GoIdle()
    {
        rb.velocity = new Vector2(0, 0);
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRegularAttack", false);
    }

    public void Attack()
    {
        anim.SetBool("isRegularAttack", true);
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        rb.velocity = new Vector2(0, 0);
    }

    IEnumerator GoRegularAttack()
    {
        isOnAttack = true;
        anim.SetBool("isRegularAttack", true);
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSecondsRealtime(0.4f);
        isOnAttack = false;
        anim.SetBool("isRegularAttack", false);
    }

    public void RangedAttack()
    {
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRegularAttack", false);
        GameObject knife = Instantiate(projectile, this.transform.position, Quaternion.identity);
        throwingProjectile = knife.GetComponent<HeroRangedAttack>();
        throwingProjectile.dir = direction;
    }

}
