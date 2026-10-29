using UnityEngine;

public class Moneda : MonoBehaviour
{
    [Header("Audio de Moneda")]
    public AudioClip sonidoMoneda;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reproduce el sonido en la posición actual antes de destruir la moneda
            if (sonidoMoneda != null)
            {
                AudioSource.PlayClipAtPoint(sonidoMoneda, transform.position);
            }

            // Suma la moneda en el controlador
            ControladorMonedas controlador = FindFirstObjectByType<ControladorMonedas>();
            if (controlador != null)
            {
                controlador.SumarMoneda();
            }

            Destroy(gameObject);
        }
    }
}