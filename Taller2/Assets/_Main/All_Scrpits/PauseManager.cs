using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text textoPausa;

    [Header("Botón")]
    [SerializeField] private Button botonPausa;
    [SerializeField] private TMP_Text textoBoton;

    private bool enPausa = false;

    void Start()
    {
        Time.timeScale = 1f;
        textoPausa.gameObject.SetActive(false);

        ActualizarBoton();
    }

    void Update()
    {
        // ESC para pausar / reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausa();
        }
    }

    public void TogglePausa()
    {
        if (enPausa)
            Reanudar();
        else
            Pausar();
    }

    public void Pausar()
    {
        enPausa = true;
        Time.timeScale = 0f;

        textoPausa.gameObject.SetActive(true);
        textoPausa.text = "PAUSA\nPRESIONA ESC O CUALQUIER BOTÓN PARA CONTINUAR";

        ActualizarBoton();
    }

    public void Reanudar()
    {
        enPausa = false;
        Time.timeScale = 1f;

        textoPausa.gameObject.SetActive(false);

        ActualizarBoton();
    }

    void ActualizarBoton()
    {
        if (enPausa)
        {
            // AZUL MÁS VIVO
            textoBoton.text = "<b>REANUDAR</b>";
            textoBoton.fontSize = 50;

            Color azul = new Color(0.2f, 0.6f, 1f); // más vivo
            botonPausa.image.color = azul;
        }
        else
        {
            // NARANJA VIVO
            textoBoton.text = "<b>PAUSAR</b>";
            textoBoton.fontSize = 50;

            Color naranja = new Color(1f, 0.4f, 0f); // más fuerte
            botonPausa.image.color = naranja;
        }
    }
}