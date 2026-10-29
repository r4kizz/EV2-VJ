using UnityEngine;
using TMPro;

public class ControladorMonedas : MonoBehaviour
{
    private int monedasActuales = 0;
    public int monedasTotalesRequeridas = 7;
    
    [Header("UI y Audio")]
    public TextMeshProUGUI textoContadorMonedas;
    public AudioClip sonidoVictoria; 
    
    [Range(0f, 1f)]
    public float volumenVictoria = 0.5f; 

    void Start()
    {
        ActualizarTexto();
    }

    public void SumarMoneda()
    {
        monedasActuales++;
        ActualizarTexto();
        Debug.Log("Monedas actuales: " + monedasActuales);

        if (monedasActuales >= monedasTotalesRequeridas)
        {
            Debug.Log("¡Se alcanzaron las 7 monedas! Mostrando victoria...");

            if (sonidoVictoria != null)
            {
                ReproducerSonidoVictoriaConVolumen();
            }
            
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.MostrarVictoria();
            }
            else
            {
                Debug.LogWarning("No se encontro el GameManager en la escena.");
            }
        }
    }

    void ReproducerSonidoVictoriaConVolumen()
    {
        // Creamos un objeto temporal para reproducir el clip aplicando el volumen personalizado
        GameObject objetoAudio = new GameObject("SonidoVictoriaTemporal");
        AudioSource audioSource = objetoAudio.AddComponent<AudioSource>();
        
        audioSource.clip = sonidoVictoria;
        audioSource.volume = volumenVictoria;
        audioSource.playOnAwake = false;
        audioSource.Play();

        // Lo destruimos automaticamente cuando termina de sonar
        Destroy(objetoAudio, sonidoVictoria.length);
    }

    void ActualizarTexto()
    {
        if (textoContadorMonedas != null)
        {
            textoContadorMonedas.text = monedasActuales + " / " + monedasTotalesRequeridas;
        }
    }
}