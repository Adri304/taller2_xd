using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUIManager : MonoBehaviour
{
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");

        Time.timeScale = 1f;

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // 👈 esto sí funciona en editor
        #else
            Application.Quit(); // 👈 esto funciona en build
        #endif
    }
}