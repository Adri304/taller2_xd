using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float dano = 20f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo tocó el pincho");

        PlayerPowerUps player = other.GetComponent<PlayerPowerUps>();

        if (player != null)
        {
            Debug.Log("Es el jugador, aplicando daño");
            player.RecibirDanio(dano);
        }
    }
}