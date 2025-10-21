using UnityEngine;

public class ShotController : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo; // Punto donde sale la bala
    [SerializeField] private GameObject balaPrefab;        // Prefab de la bala

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Click izquierdo del mouse
        {
            Disparar();
        }
    }

    void Disparar()
    {
        GameObject nuevaBala = Instantiate(balaPrefab, controladorDisparo.position, controladorDisparo.rotation);

        // Si el jugador está mirando a la izquierda, invertir dirección
        if (transform.localScale.x < 0)
        {
            nuevaBala.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
