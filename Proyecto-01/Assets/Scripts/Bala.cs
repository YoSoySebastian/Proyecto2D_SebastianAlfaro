using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float vidaUtil = 2f; // tiempo antes de destruirse (seguridad)

    private void Start()
    {
        Destroy(gameObject, vidaUtil); // 👈 ahora se destruye solo después de 2 segundos
    }

    private void Update()
    {
        // movimiento constante hacia adelante
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemigo = other.GetComponent<EnemyController>();
            if (enemigo != null)
            {
                enemigo.RecibirDamage(damage);
            }

            Destroy(gameObject); // destruye la bala al impactar
        }
    }
}
