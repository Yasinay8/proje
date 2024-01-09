using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroHealth : MonoBehaviour
{
    public int health = 1000;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject, 2f);
        anim.SetBool("isDead", true);
        anim.SetBool("isRegularAttack", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", false);
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

}
