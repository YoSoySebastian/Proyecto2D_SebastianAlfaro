using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10.0f;
    public float speed = 5.0f;

    private Rigidbody2D rb; 
    private Vector2 Movement;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            float directionX = player.position.x > transform.position.x ? 1f : -1f;
            rb.velocity = new Vector2(directionX * speed, rb.velocity.y);

            // --- BONUS: mirar hacia el jugador ---
            if (player.position.x > transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            Vector2 direccionDamage = new Vector2(transform.position.x, 0);
            collision.gameObject.GetComponent<PlayerController>().onDamage(direccionDamage, 1);
        }
    }
}
