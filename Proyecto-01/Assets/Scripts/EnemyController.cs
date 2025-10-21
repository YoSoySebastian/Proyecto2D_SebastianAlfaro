using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 30.0f;
    public float speed = 5.0f;
    public float vida = 3f;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            float directionX = player.position.x > transform.position.x ? 1f : -1f;
            rb.velocity = new Vector2(directionX * speed, rb.velocity.y);

            // Voltear sprite
            if (player.position.x > transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        animator.SetFloat("Movement", Mathf.Abs(rb.velocity.x));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            Vector2 direccionDamage = new Vector2(transform.position.x, 0);
            collision.gameObject.GetComponent<PlayerController>().onDamage(direccionDamage, 1);
        }
    }

        public void RecibirDamage(float damage)
    {
        vida -= damage;

        if (vida <= 0)
        {
            Muerte();
        }
    }

    void Muerte()
    {
        animator.SetTrigger("Death");

        Destroy(gameObject, 0.5f);
    }
}
