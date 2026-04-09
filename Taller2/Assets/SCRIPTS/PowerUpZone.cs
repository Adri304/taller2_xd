using UnityEngine;

public class PowerUpZone : MonoBehaviour
{
    [SerializeField] private PowerUpUI ui;

    private void OnTriggerEnter(Collider other)
    {
        PlayerPowerUps player = other.GetComponent<PlayerPowerUps>();

        if (player != null)
        {
            ui.SetPlayer(player);
            ui.AbrirUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerPowerUps player = other.GetComponent<PlayerPowerUps>();

        if (player != null)
        {
            ui.CerrarUI();
        }
    }
}