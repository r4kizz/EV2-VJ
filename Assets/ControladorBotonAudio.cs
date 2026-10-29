using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ControladorBotonAudio : MonoBehaviour
{
    private Button boton;
    
    [Header("Audio de Boton")]
    public AudioClip sonidoClick;
    
    [Range(0f, 1f)]
    public float volumen = 0.5f;

    void Start()
    {
        boton = GetComponent<Button>();
        if (boton != null)
        {
            boton.onClick.AddListener(ReproducirSonido);
        }
    }

    void ReproducirSonido()
    {
        if (sonidoClick == null) return;

        // Crea un objeto temporal para que el sonido no se corte al cambiar de escena
        GameObject objetoAudio = new GameObject("SonidoBotonTemporal");
        AudioSource audioSource = objetoAudio.AddComponent<AudioSource>();
        
        audioSource.clip = sonidoClick;
        audioSource.volume = volumen;
        audioSource.playOnAwake = false;
        audioSource.Play();

        // Evita que Unity destruya el audio al cargar/reiniciar la escena
        DontDestroyOnLoad(objetoAudio);

        // Destruye el objeto temporal automaticamente cuando termina de sonar el clip
        Destroy(objetoAudio, sonidoClick.length);
    }
}