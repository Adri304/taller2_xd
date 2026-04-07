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
