using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPowerUps : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private float vidaInicial = 80f;
    [SerializeField] private float vidaMax = 100f;
    private float vidaActual;

    [Header("Velocidad")]
    [SerializeField] private float velocidadBase = 30f;
    private float velocidadActual;

    [Header("UI")]
    [SerializeField] private TMP_Text textoVida;
    [SerializeField] private TMP_Text textoVelocidad;
    [SerializeField] private TMP_Text textoEscudo;

    [Header("Efectos Visuales")]
    [SerializeField] private GameObject fxEscudo;
    [SerializeField] private GameObject fxEscudoParticulas;
    [SerializeField] private GameObject fxVelocidad;
    [SerializeField] private GameObject fxCuracion;

    private bool tieneEscudo;
    private bool tieneVelocidad;

    private float tiempoEscudo = 0f;
    private float tiempoVelocidad = 0f;

    void Start()
    {
        vidaActual = vidaInicial;
        velocidadActual = velocidadBase;

        textoEscudo.text = "";
        textoVelocidad.text = "";

        ActualizarVidaUI();

        fxEscudo.SetActive(false);
        fxEscudoParticulas.SetActive(false);
        fxVelocidad.SetActive(false);
        fxCuracion.SetActive(false);
    }

    void Update()
    {
        ActualizarEscudo();
        ActualizarVelocidad();
    }

    private void ActualizarVidaUI()
    {
        if (textoVida != null)
            textoVida.text = "Vida: " + Mathf.Ceil(vidaActual) + " / " + vidaMax;
    }

    // CURACIÓN
    public void Curar(float cantidad)
    {
        vidaActual = Mathf.Clamp(vidaActual + cantidad, 0, vidaMax);

        ActualizarVidaUI();

        StartCoroutine(EfectoCuracion());
    }

    IEnumerator EfectoCuracion()
    {
        fxCuracion.SetActive(true);
        yield return new WaitForSeconds(1f);
        fxCuracion.SetActive(false);
    }

    // ESCUDO
    public void ActivarEscudo(float duracion)
    {
        tiempoEscudo += duracion;

        if (!tieneEscudo)
        {
            fxEscudo.SetActive(true);
            fxEscudoParticulas.SetActive(true);
        }

        tieneEscudo = true;
    }

    void ActualizarEscudo()
    {
        if (!tieneEscudo) return;

        tiempoEscudo -= Time.deltaTime;

        textoEscudo.text = "Escudo: " + Mathf.Ceil(tiempoEscudo) + "s";

        if (tiempoEscudo <= 5f)
        {
            textoEscudo.color = Color.red;
            textoEscudo.enabled = !textoEscudo.enabled;
        }
        else
        {
            textoEscudo.color = Color.white;
            textoEscudo.enabled = true;
        }

        // ESCUDO TERMINA CORRECTAMENTE
        if (tiempoEscudo <= 0)
        {
            tiempoEscudo = 0f;
            tieneEscudo = false;

            textoEscudo.text = "";
            textoEscudo.enabled = true;

            fxEscudo.SetActive(false);
            fxEscudoParticulas.SetActive(false);

            Debug.Log("Escudo terminado");
        }
    }

    // VELOCIDAD
    public void ActivarVelocidad(float duracion, float multiplicador)
{
    tiempoVelocidad += duracion;

    if (!tieneVelocidad)
    {
        velocidadActual = velocidadBase * multiplicador; // x3 REAL
        fxVelocidad.SetActive(true);
        tieneVelocidad = true;
    }
}

    void ActualizarVelocidad()
    {
        if (!tieneVelocidad) return;

        tiempoVelocidad -= Time.deltaTime;

        textoVelocidad.text = "Velocidad: " + Mathf.Ceil(tiempoVelocidad) + "s";

        if (tiempoVelocidad <= 5f)
        {
            textoVelocidad.color = Color.red;
            textoVelocidad.enabled = !textoVelocidad.enabled;
        }
        else
        {
            textoVelocidad.color = Color.white;
            textoVelocidad.enabled = true;
        }

        if (tiempoVelocidad <= 0)
        {
            tieneVelocidad = false;
            velocidadActual = velocidadBase;

            textoVelocidad.text = "";
            textoVelocidad.enabled = true;

            fxVelocidad.SetActive(false);
        }
    }

    public float GetVelocidad()
    {
        return velocidadActual;
    }

    public void RecibirDanio(float dano)
    {
        // BLOQUEO TOTAL DEL ESCUDO
        if (tieneEscudo && tiempoEscudo > 0f)
        {
            Debug.Log("Daño bloqueado por escudo");
            return;
        }

        vidaActual -= dano;

        ActualizarVidaUI();

        if (vidaActual <= 0)
        {
            vidaActual = 0;
            Muerte();
        }
    }

    // MUERTE
    private void Muerte()
    {
        Debug.Log("Jugador murió");
        Invoke(nameof(ReiniciarNivel), 1.5f);
    }

    private void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}