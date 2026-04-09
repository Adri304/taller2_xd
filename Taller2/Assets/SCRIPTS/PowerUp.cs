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

