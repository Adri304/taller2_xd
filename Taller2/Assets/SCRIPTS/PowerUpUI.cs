using UnityEngine;
using TMPro;
using System.Collections;

public class PowerUpUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_InputField inputValor;
    [SerializeField] private TMP_Text textoFeedback;

    private PlayerPowerUps player;
    private PowerUp.TipoPowerUp tipoSeleccionado;

    void Start()
    {
        panel.SetActive(false);
        textoFeedback.text = "";
    }

    // Se asigna cuando el jugador entra en la zona
    public void SetPlayer(PlayerPowerUps p)
    {
        player = p;
    }

    public void AbrirUI()
    {
        panel.SetActive(true);
    }

    public void CerrarUI()
    {
        panel.SetActive(false);
    }

   
    // BOTONES DE SELECCIÓN

    public void SeleccionarEscudo()
    {
        tipoSeleccionado = PowerUp.TipoPowerUp.Escudo;
        textoFeedback.text = "Seleccionaste Escudo";
    }

    public void SeleccionarVelocidad()
    {
        tipoSeleccionado = PowerUp.TipoPowerUp.Velocidad;
        textoFeedback.text = "Seleccionaste Velocidad";
    }

    public void SeleccionarCuracion()
    {
        tipoSeleccionado = PowerUp.TipoPowerUp.Curacion;
        textoFeedback.text = "Seleccionaste Curación";
    }

   
    // BOTÓN APLICAR  

    public void BotonAplicar()
    {
        string texto = inputValor.text;

        float valor;

        if (!float.TryParse(texto, out valor))
        {
            textoFeedback.text = "Valor inválido";
            StartCoroutine(LimpiarTexto());
            return;
        }

        switch (tipoSeleccionado)
        {
            case PowerUp.TipoPowerUp.Curacion:
                valor = Mathf.Clamp(valor, 10f, 50f);
                player.Curar(valor);
                textoFeedback.text = "Curado +" + valor;
                break;

            case PowerUp.TipoPowerUp.Escudo:
                valor = Mathf.Clamp(valor, 3f, 20f);
                player.ActivarEscudo(valor);
                textoFeedback.text = "Escudo activado por " + valor + "s";
                break;

            case PowerUp.TipoPowerUp.Velocidad:
                valor = Mathf.Clamp(valor, 1f, 5f);
                player.ActivarVelocidad(5f, valor);
                textoFeedback.text = "Velocidad +" + valor;
                break;
        }

        StartCoroutine(LimpiarTexto());
    }

 
    // LIMPIAR MENSAJE AUTOMÁTICO

    IEnumerator LimpiarTexto()
    {
        yield return new WaitForSeconds(2f);
        textoFeedback.text = "";
    }
}