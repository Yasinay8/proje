using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public int speed = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GoToPlayer(collision.gameObject);
        }
    }

    public void GoToPlayer(GameObject target)
    {
        rb.MovePosition(target.transform.position);
        
    }
}
