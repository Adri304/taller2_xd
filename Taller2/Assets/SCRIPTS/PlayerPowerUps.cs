using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{

    [Header("Vida")]
    [SerializeField] private float vidaMax = 100f;
    private float vidaActual;

    [Header("Velocidad")]
    [SerializeField] private float velocidadBase = 5f;
    private float velocidadActual;

    [Header("Estados")]
    [SerializeField] private bool tieneEscudo;
    [SerializeField] private bool tieneBoostVelocidad;

    
    void Start()
    {
        vidaActual = vidaMax;
        velocidadActual = velocidadBase;
    }

    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
            Morir();
    }

    public void Curar(float cantidad)
    {
        vidaActual = Mathf.Clamp(vidaActual + cantidad, 0, vidaMax);
    }

    void Morir()
    {
        Debug.Log("Jugador muerto");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
