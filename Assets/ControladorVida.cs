using UnityEngine;
using UnityEngine.UI;

public class ControladorVidas : MonoBehaviour
{
    [Header("Configuración de Vidas")]
    public int vidasMaximas = 3;
    private int vidasActuales;

    [Header("Referencias de UI (Corazón Único)")]
    public Image imagenCorazon;
    public Sprite[] spritesCorazones;

    void Start()
    {
        // Al empezar el juego, las vidas se configuran al máximo y se actualiza el sprite inicial
        vidasActuales = vidasMaximas;
        ActualizarSpriteCorazon();
    }

    public void PerderVida()
    {
        vidasActuales--;
        Debug.Log("Vidas restantes: " + vidasActuales);

        // Actualiza el sprite del corazón en pantalla
        ActualizarSpriteCorazon();

        // Si las vidas llegan a cero, buscamos el GameManager para mostrar el Game Over
        if (vidasActuales <= 0)
        {
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.MostrarGameOver();
            }
            else
            {
                Debug.LogWarning("No se encontró ningún GameManager en la escena.");
            }
        }
    }

    void ActualizarSpriteCorazon()
    {
        if (imagenCorazon != null && spritesCorazones != null && spritesCorazones.Length > 0)
        {
            // Limita el índice para que coincida exactamente con las vidas actuales (0 hasta 3)
            int index = Mathf.Clamp(vidasActuales, 0, spritesCorazones.Length - 1);
            imagenCorazon.sprite = spritesCorazones[index];
        }
    }

    public int ObtenerVidas()
    {
        return vidasActuales;
    }
}