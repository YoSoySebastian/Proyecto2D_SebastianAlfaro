using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            // Reinicia la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
