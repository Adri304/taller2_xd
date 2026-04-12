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

    private bool tieneEscudo;
    private bool tieneVelocidad;

    // TIEMPOS ACUMULABLES
    private float tiempoEscudo = 0f;
    private float tiempoVelocidad = 0f;

    void Start()
    {
        vidaActual = vidaMax;
        velocidadActual = velocidadBase;

        textoEscudo.text = "";
        textoVelocidad.text = "";
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
    }

    // ESCUDO (ACUMULABLE)
    public void ActivarEscudo(float duracion)
    {
        tiempoEscudo += duracion;
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
        }
    }

    // VELOCIDAD (ACUMULABLE)
    public void ActivarVelocidad(float duracion, float bonus)
    {
        tiempoVelocidad += duracion;

        if (!tieneVelocidad)
        {
            velocidadActual += bonus;
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
        }
    }

    // Para movimiento
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