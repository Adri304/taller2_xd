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