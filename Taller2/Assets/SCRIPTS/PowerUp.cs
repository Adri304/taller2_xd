using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum TipoPowerUp
    {
        Curacion,
        Escudo,
        Velocidad
    }

    public TipoPowerUp tipo;
    public float valor;
    public float duracion;
}


void OnTriggerEnter(Collider other)
{
    PowerUp power = other.GetComponent<PowerUp>();

    if (power != null)
    {
        ActivarPowerUp(power);
        Destroy(other.gameObject);
    }
}


void ActivarPowerUp(PowerUp power)
{
    switch (power.tipo)
    {
        case PowerUp.TipoPowerUp.Curacion:
            Curar(power.valor);
            break;

        case PowerUp.TipoPowerUp.Escudo:
            ActivarEscudo(power.duracion);
            break;

        case PowerUp.TipoPowerUp.Velocidad:
            ActivarVelocidad(power.duracion, power.valor);
            break;
    }
}


public float tiempoEscudoRestante;

tiempoEscudoRestante = duracion;

while (tiempoEscudoRestante > 0)
{
    tiempoEscudoRestante -= Time.deltaTime;
    yield return null;
}

public void ActivarEscudo(float duracion)
{
    StartCoroutine(CorutinaEscudo(duracion));
}

IEnumerator CorutinaEscudo(float duracion)
{
    tieneEscudo = true;

    yield return new WaitForSeconds(duracion);

    tieneEscudo = false;
}

public void RecibirDanio(float cantidad)
{
    if (tieneEscudo) return;

    vidaActual -= cantidad;

    if (vidaActual <= 0)
        Morir();
}


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

