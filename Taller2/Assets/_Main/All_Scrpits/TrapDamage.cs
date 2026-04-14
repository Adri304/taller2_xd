using UnityEngine;
using System.Collections.Generic;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float dano = 5f;
    [SerializeField] private float intervalo = 2f; // 👈 cada 2 segundos

    // Guarda el tiempo del último daño por jugador
    private Dictionary<PlayerPowerUps, float> lastHitTime = new Dictionary<PlayerPowerUps, float>();

    private void OnTriggerStay(Collider other)
    {
        PlayerPowerUps player = other.GetComponentInParent<PlayerPowerUps>();

        if (player == null) return;

        // Si no existe en el diccionario, lo agregamos
        if (!lastHitTime.ContainsKey(player))
        {
            lastHitTime[player] = -intervalo;
        }

        // Verificamos si ya pasaron 2 segundos
        if (Time.time >= lastHitTime[player] + intervalo)
        {
            player.RecibirDanio(dano);
            lastHitTime[player] = Time.time;

            Debug.Log("💥 Daño aplicado con intervalo");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerPowerUps player = other.GetComponentInParent<PlayerPowerUps>();

        if (player != null && lastHitTime.ContainsKey(player))
        {
            lastHitTime.Remove(player); // limpiamos al salir
        }
    }
}