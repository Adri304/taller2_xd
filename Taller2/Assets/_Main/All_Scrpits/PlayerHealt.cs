using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vidaMaxima = 5;
    public int vidaActual;

    void Awake() // 🔥 IMPORTANTE (no Start)
    {
        vidaActual = vidaMaxima;
    }

    public void recibirDaño(int cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            vidaActual = 0;
            morir();
        }
    }

    public void curar(int cantidad)
    {
        vidaActual += cantidad;

        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }
    }

    void morir()
    {
        Debug.Log("Murió");
    }
}