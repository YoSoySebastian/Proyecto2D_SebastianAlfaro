using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Movimiento
    public float velocidad = 15f;

    // Salto
    public float fuerzaSalto = 10f;
    public float fuerzaRebote = 10f;
    public LayerMask capaGround;
    public float duracionShot = 0.2f;

    private bool enSuelo;
    private bool damage;
    private Rigidbody2D rb; 
    private Collider2D col;

    public int vidas = 5;

    // Animacion
    public Animator animator;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento
        if (!damage)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * velocidad, rb.velocity.y);

            // Cambiar direccion
            if (moveInput != 0)
                transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);

            animator.SetFloat("Movement", Mathf.Abs(moveInput));
        }

        // Detectar suelo
        enSuelo = col.IsTouchingLayers(capaGround);

        // Salto
        if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !damage)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        }

        // Animaciones
        animator.SetBool("Suelo", enSuelo);
        animator.SetFloat("VelocidadY", rb.velocity.y);
        animator.SetBool("Damage", damage);

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Disparo());
        }
    }

    IEnumerator Disparo()
    {
        animator.SetBool("Shot", true);
        yield return new WaitForSeconds(duracionShot);
        animator.SetBool("Shot", false);
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

            vidas--;
            if (vidas <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void offDamage()
    {
        damage = false;
        animator.SetBool("Damage", false);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
}