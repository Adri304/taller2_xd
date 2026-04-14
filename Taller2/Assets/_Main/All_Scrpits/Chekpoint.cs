using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerPowerUps player = other.GetComponent<PlayerPowerUps>();

        if (player != null)
        {
            player.SetCheckpoint(transform.position);

            Debug.Log("Checkpoint activado");
        }
    }
}