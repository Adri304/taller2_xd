using UnityEngine;
using TMPro;

public class UIVida : MonoBehaviour
{
    public TextMeshProUGUI textoVida;
    public PlayerHealth player;

    void Start()
    {
        textoVida.text = "Vida: " + player.vidaActual;
    }

    void Update()
    {
        textoVida.text = "Vida: " + player.vidaActual;
    }
}