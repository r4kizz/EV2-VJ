using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento y Animacion")]
    public float velocidad = 5f;
    public Animator animator;
    public float fuerzaSalto = 10f;
    public float distanciaLineaSuelo = 0.1f;
    public LayerMask capaSuelo;
    public bool tocandoElSuelo = true;
    
    [Header("Audio")]
    public AudioSource audioSource; 
    public AudioClip clipSalto;   
    public AudioClip clipDano; 
    public AudioClip clipPerderTodas;

    private Rigidbody2D rb;
    private Vector3 escalaInicial;
    private Vector3 posicionInicial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        escalaInicial = transform.localScale;
        posicionInicial = transform.position;
    }

    void Update()
    {
        float velocidadH = Input.GetAxis("Horizontal") * velocidad * Time.deltaTime;
        
        animator.SetBool("IsWalking", Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f);

        if (velocidadH < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(escalaInicial.x), escalaInicial.y, escalaInicial.z);
        }
        else if (velocidadH > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(escalaInicial.x), escalaInicial.y, escalaInicial.z);
        }

        Vector3 movimiento = transform.position;
        transform.position = new Vector3(movimiento.x + velocidadH, movimiento.y, movimiento.z);

        // 2. Deteccion de Suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanciaLineaSuelo, capaSuelo);
        tocandoElSuelo = hit.collider != null;

        // 3. Salto
        if (Input.GetKeyDown(KeyCode.Space) && tocandoElSuelo)
        {
            rb.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);

            if (audioSource != null && clipSalto != null)
            {
                audioSource.PlayOneShot(clipSalto);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Muerte"))
        {
            ControladorVidas controladorVidas = FindFirstObjectByType<ControladorVidas>();

            if (controladorVidas != null)
            {
                controladorVidas.PerderVida();

                if (controladorVidas.ObtenerVidas() > 0)
                {
                    // aun tiene vidas (Reproduce sonido de dano - Hit 7)
                    if (clipDano != null)
                    {
                        AudioSource.PlayClipAtPoint(clipDano, transform.position);
                    }

                    transform.position = posicionInicial;
                    if (rb != null)
                    {
                        rb.linearVelocity = Vector2.zero; 
                    }
                }
                else
                {
                    // no tiene vidas (Reproduce sonido de derrota total - Lose 1)
                    if (clipPerderTodas != null)
                    {
                        AudioSource.PlayClipAtPoint(clipPerderTodas, transform.position);
                    }

                    enabled = false;

                    if (rb != null)
                    {
                        rb.simulated = false;
                    }
                }
            }
        }
    }

    void ReiniciarJuego()
    {
        Debug.Log("Game Over - Reiniciando nivel...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanciaLineaSuelo);
    }
}