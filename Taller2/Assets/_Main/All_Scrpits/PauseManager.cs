using UnityEngine;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textoPausa;

    private bool enPausa = false;

    void Start()
    {
        Time.timeScale = 1f; // Asegura que el juego inicia normal
        textoPausa.gameObject.SetActive(false);
    }

    void Update()
    {
        // ESC para pausar y reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!enPausa)
                Pausar();
            else
                Reanudar();
        }

        // Cualquier tecla para continuar (EXCEPTO ESC)
        if (enPausa && Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
        {
            Reanudar();
        }
    }

    public void Pausar()
    {
        enPausa = true;
        Time.timeScale = 0f;

        textoPausa.gameObject.SetActive(true);
        textoPausa.text = "PAUSA\nPresiona cualquier tecla para continuar";
    }

    public void Reanudar()
    {
        enPausa = false;
        Time.timeScale = 1f;

        textoPausa.gameObject.SetActive(false);
    }
}