using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerPowerUps : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private float vidaMax = 100f;
    private float vidaActual;

    [Header("Velocidad")]
    [SerializeField] private float velocidadBase = 5f;
    private float velocidadActual;

    [Header("UI")]
    [SerializeField] private TMP_Text textoVelocidad;
    [SerializeField] private TMP_Text textoEscudo;

    [Header("Efectos Visuales")]
    [SerializeField] private GameObject fxEscudo; // esfera
    [SerializeField] private GameObject fxEscudoParticulas;
    [SerializeField] private GameObject fxVelocidad;
    [SerializeField] private GameObject fxCuracion;

    private bool tieneEscudo;
    private bool tieneVelocidad;

    private float tiempoEscudo = 0f;
    private float tiempoVelocidad = 0f;

    void Start()
    {
        vidaActual = vidaMax;
        velocidadActual = velocidadBase;

        textoEscudo.text = "";
        textoVelocidad.text = "";

        // Apagar efectos al inicio
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

    // CURACIÓN
    public void Curar(float cantidad)
    {
        vidaActual = Mathf.Clamp(vidaActual + cantidad, 0, vidaMax);

        // Activar efecto temporal
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

        // ALERTA últimos 5 segundos
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

        if (tiempoEscudo <= 0)
        {
            tieneEscudo = false;
            textoEscudo.text = "";
            textoEscudo.enabled = true;

            fxEscudo.SetActive(false);
            fxEscudoParticulas.SetActive(false);
        }
    }

    // VELOCIDAD
    public void ActivarVelocidad(float duracion, float bonus)
    {
        tiempoVelocidad += duracion;

        if (!tieneVelocidad)
        {
            velocidadActual += bonus;
            fxVelocidad.SetActive(true);
            tieneVelocidad = true;
        }
    }

    void ActualizarVelocidad()
    {
        if (!tieneVelocidad) return;

        tiempoVelocidad -= Time.deltaTime;

        textoVelocidad.text = "Velocidad: " + Mathf.Ceil(tiempoVelocidad) + "s";

        // ALERTA últimos 5 segundos
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

    // MOVIMIENTO
    public float GetVelocidad()
    {
        return velocidadActual;
    }

    // DAÑO
    public void RecibirDanio(float daño)
    {
        if (tieneEscudo) return;

        vidaActual -= daño;
    }
}