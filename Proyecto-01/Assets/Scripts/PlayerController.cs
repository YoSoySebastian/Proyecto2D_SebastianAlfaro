using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Movimiento lateral
    public float velocidad = 15f;

    // Salto
    public float fuerzaSalto = 10f;
    public float fuerzaRebote = 10f;
    public LayerMask capaGround;

    private bool enSuelo;
    private bool damage;
    private Rigidbody2D rb; 
    private Collider2D col;

    // Animacion
    public Animator animator;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Velocidad de movimiento
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        // Animacion de movimiento
        animator.SetFloat("Movement", velocidadX * velocidad);

        // Cambiar de lado
        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Movimiento
        if (!damage)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * velocidad, rb.velocity.y);

            // Animaciones
            animator.SetFloat("Movement", Mathf.Abs(moveInput));
        }
        else
        {
            // Mientras está en damage, solo se mueve por física (AddForce) y no modificamos rb.velocity.x
            // Esto permite que el rebote se aplique correctamente y el personaje deje de deslizarse cuando offDamage sea llamado
        }

        // Detectar suelo
        enSuelo = col.IsTouchingLayers(capaGround);

        // Salto
        if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !damage)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            animator.SetBool("Suelo", false);
        }

        // Animaciones
        animator.SetBool("Suelo", enSuelo);
        animator.SetFloat("VelocidadY", rb.velocity.y);
        animator.SetFloat("Movement", Mathf.Abs(velocidadX));
        animator.SetBool("Damage", damage);
    }

    public void onDamage(Vector2 direccion, int cantidadDamage)
    {
        if (!damage)
        {
            damage = true;
            animator.SetBool("Damage", true);

            float horizontalDirection = transform.position.x < direccion.x ? -1 : 1;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(horizontalDirection, 1).normalized * fuerzaRebote, ForceMode2D.Impulse);
        }
    }

    public void offDamage()
    {
        damage = false;
        animator.SetBool("Damage", false);

        rb.velocity = new Vector2(0, rb.velocity.y);
    }
}
