using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float daño = 20f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerPowerUps player = other.GetComponent<PlayerPowerUps>();

        if (player != null)
        {
            player.RecibirDanio(daño);
        }
    }
}