using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private void OnTriggerEnter(Collider other)
    {
        PlayerPowerUps player = other.GetComponent<PlayerPowerUps>();

        if (player != null)
        {
            winPanel.SetActive(true);

            Time.timeScale = 0f; // pausa el juego

            Debug.Log("Ganaste!");
        }
    }
}