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

    private Vector3 checkpointPosition;

    private bool tieneEscudo;
    private bool tieneVelocidad;

    // 🔥 CORRUTINAS ACTIVAS
    private Coroutine escudoCoroutine;
    private Coroutine velocidadCoroutine;

    void Start()
    {
        vidaActual = vidaInicial;
        velocidadActual = velocidadBase;

        checkpointPosition = transform.position;

        textoEscudo.text = "";
        textoVelocidad.text = "";

        ActualizarVidaUI();

        fxEscudo.SetActive(false);
        fxEscudoParticulas.SetActive(false);
        fxVelocidad.SetActive(false);
        fxCuracion.SetActive(false);
    }

    private void ActualizarVidaUI()
    {
        if (textoVida != null)
            textoVida.text = "Vida: " + Mathf.Ceil(vidaActual) + " / " + vidaMax;
    }

    public void SetCheckpoint(Vector3 nuevaPosicion)
    {
        checkpointPosition = nuevaPosicion;
    }

    // =========================
    // CURACIÓN
    // =========================
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

    // =========================
    // ESCUDO (CORRUTINA)
    // =========================
    public void ActivarEscudo(float duracion)
    {
        if (escudoCoroutine != null)
            StopCoroutine(escudoCoroutine);

        escudoCoroutine = StartCoroutine(EscudoCoroutine(duracion));
    }

    IEnumerator EscudoCoroutine(float duracion)
    {
        tieneEscudo = true;

        fxEscudo.SetActive(true);
        fxEscudoParticulas.SetActive(true);

        float tiempo = duracion;

        while (tiempo > 0f)
        {
            tiempo -= Time.deltaTime;

            textoEscudo.text = "Escudo: " + Mathf.Ceil(tiempo) + "s";

            if (tiempo <= 5f)
            {
                textoEscudo.color = Color.red;
                textoEscudo.enabled = !textoEscudo.enabled;
            }
            else
            {
                textoEscudo.color = Color.white;
                textoEscudo.enabled = true;
            }

            yield return null;
        }

        // FIN
        tieneEscudo = false;

        textoEscudo.text = "";
        textoEscudo.enabled = true;

        fxEscudo.SetActive(false);
        fxEscudoParticulas.SetActive(false);

        Debug.Log("Escudo terminado");
    }

    // =========================
    // VELOCIDAD (CORRUTINA)
    // =========================
    public void ActivarVelocidad(float duracion, float multiplicador)
    {
        if (velocidadCoroutine != null)
            StopCoroutine(velocidadCoroutine);

        velocidadCoroutine = StartCoroutine(VelocidadCoroutine(duracion, multiplicador));
    }

    IEnumerator VelocidadCoroutine(float duracion, float multiplicador)
    {
        tieneVelocidad = true;

        velocidadActual = velocidadBase * multiplicador;
        fxVelocidad.SetActive(true);

        float tiempo = duracion;

        while (tiempo > 0f)
        {
            tiempo -= Time.deltaTime;

            textoVelocidad.text = "Velocidad: " + Mathf.Ceil(tiempo) + "s";

            if (tiempo <= 5f)
            {
                textoVelocidad.color = Color.red;
                textoVelocidad.enabled = !textoVelocidad.enabled;
            }
            else
            {
                textoVelocidad.color = Color.white;
                textoVelocidad.enabled = true;
            }

            yield return null;
        }

        // FIN
        tieneVelocidad = false;

        velocidadActual = velocidadBase;

        textoVelocidad.text = "";
        textoVelocidad.enabled = true;

        fxVelocidad.SetActive(false);

        Debug.Log("Velocidad terminada");
    }

    // =========================
    public float GetVelocidad()
    {
        return velocidadActual;
    }

    public void RecibirDanio(float dano)
    {
        if (tieneEscudo)
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

    // =========================
    private void Muerte()
    {
        Invoke(nameof(Respawn), 1.5f);
    }

    private void Respawn()
    {
        transform.position = checkpointPosition;
        vidaActual = vidaInicial;
        ActualizarVidaUI();
    }
}