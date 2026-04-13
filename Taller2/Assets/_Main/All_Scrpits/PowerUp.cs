using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum TipoPowerUp
    {
        Curacion,
        Escudo,
        Velocidad
    }

    [SerializeField] private TipoPowerUp tipo;

    private void OnTriggerEnter(Collider other)
    {
        PlayerPowerUps player = other.GetComponent<PlayerPowerUps>();

        if (player != null)
        {
            switch (tipo)
            {
                case TipoPowerUp.Curacion:
                    player.Curar(3f);
                    break;

                case TipoPowerUp.Escudo:
                    player.ActivarEscudo(20f);
                    break;

                case TipoPowerUp.Velocidad:
                    player.ActivarVelocidad(20f, 3f);
                    break;
            }

            Destroy(gameObject);
        }
    }
}