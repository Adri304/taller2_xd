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
        StartCoroutine(CorutinaEscudo(duracion));
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
    }

   
    // VELOCIDAD

    public void ActivarVelocidad(float duracion, float bonus)
    {
        StartCoroutine(CorutinaVelocidad(duracion, bonus));
    }

    IEnumerator CorutinaVelocidad(float duracion, float bonus)
    {
        tieneBoostVelocidad = true;
        velocidadActual += bonus;

        yield return new WaitForSeconds(duracion);

        velocidadActual = velocidadBase;
        tieneBoostVelocidad = false;
    }
}
