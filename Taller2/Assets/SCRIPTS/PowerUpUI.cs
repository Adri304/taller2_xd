using UnityEngine;
using TMPro;

public class PowerUpUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_InputField inputValor;

    private PlayerPowerUps player;
    private PowerUp.TipoPowerUp tipoSeleccionado;

    void Start()
    {
        panel.SetActive(false);
    }

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

   
    // BOTONES

    public void SeleccionarEscudo()
    {
        tipoSeleccionado = PowerUp.TipoPowerUp.Escudo;
    }

    public void SeleccionarVelocidad()
    {
        tipoSeleccionado = PowerUp.TipoPowerUp.Velocidad;
    }

    public void SeleccionarCuracion()
    {
        tipoSeleccionado = PowerUp.TipoPowerUp.Curacion;
    }

  
    // BOTÓN APLICAR
   
    public void BotonAplicar()
    {
        string texto = inputValor.text;

        float valor;

        if (!float.TryParse(texto, out valor))
        {
            Debug.Log("Valor inválido");
            return;
        }

        // LIMITES SEGÚN EL TIPO
        switch (tipoSeleccionado)
        {
            case PowerUp.TipoPowerUp.Curacion:
                valor = Mathf.Clamp(valor, 10f, 50f);
                player.Curar(valor);
                break;

            case PowerUp.TipoPowerUp.Escudo:
                valor = Mathf.Clamp(valor, 3f, 20f);
                player.ActivarEscudo(valor);
                break;

            case PowerUp.TipoPowerUp.Velocidad:
                valor = Mathf.Clamp(valor, 1f, 5f);
                player.ActivarVelocidad(5f, valor);
                break;
        }
    }
}