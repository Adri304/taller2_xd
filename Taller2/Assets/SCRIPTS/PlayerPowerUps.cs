using UnityEngine;
using System.Collections;

public class PlayerPowerUps : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private float vidaMax = 100f;
    private float vidaActual;

    [Header("Velocidad")]
    [SerializeField] private float velocidadBase = 5f;
    private float velocidadActual;

    [Header("Estados")]
    [SerializeField] private bool tieneEscudo;
    [SerializeField] private bool tieneBoostVelocidad;

    [Header("Temporizadores")]
    [SerializeField] private float tiempoEscudoRestante;

    // Corrutinas activas (para evitar acumulación)
    private Coroutine escudoCoroutine;
    private Coroutine velocidadCoroutine;

    void Start()
    {
        vidaActual = vidaMax;
        velocidadActual = velocidadBase;
    }

    
    // VIDA

    public void Curar(float cantidad)
    {
        vidaActual = Mathf.Clamp(vidaActual + cantidad, 0, vidaMax);
    }

    public void RecibirDanio(float cantidad)
    {
        if (tieneEscudo) return;

        vidaActual -= cantidad;

        if (vidaActual <= 0)
            Debug.Log("Jugador muerto");
    }

   
    // ESCUDO

    public void ActivarEscudo(float duracion)
    {
        // Si ya hay escudo activo, lo reinicia
        if (escudoCoroutine != null)
        {
            StopCoroutine(escudoCoroutine);
        }

        escudoCoroutine = StartCoroutine(CorutinaEscudo(duracion));
    }

    IEnumerator CorutinaEscudo(float duracion)
    {
        tieneEscudo = true;
        tiempoEscudoRestante = duracion;

        while (tiempoEscudoRestante > 0)
        {
            tiempoEscudoRestante -= Time.deltaTime;
            yield return null;
        }

        tieneEscudo = false;
        escudoCoroutine = null;
    }

    
    // VELOCIDAD

    public void ActivarVelocidad(float duracion, float bonus)
    {
        // Reinicia si ya estaba activa
        if (velocidadCoroutine != null)
        {
            StopCoroutine(velocidadCoroutine);
            velocidadActual = velocidadBase; // reset previo
        }

        velocidadCoroutine = StartCoroutine(CorutinaVelocidad(duracion, bonus));
    }

    IEnumerator CorutinaVelocidad(float duracion, float bonus)
    {
        tieneBoostVelocidad = true;
        velocidadActual += bonus;

        yield return new WaitForSeconds(duracion);

        velocidadActual = velocidadBase;
        tieneBoostVelocidad = false;
        velocidadCoroutine = null;
    }
}
