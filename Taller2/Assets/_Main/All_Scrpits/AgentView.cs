using UnityEngine;  // Librer�a principal de Unity.

public class PlayerAnimatorView : MonoBehaviour
{
    // Enum para representar el estado l�gico actual de animaci�n.
    public enum AnimationState
    {
        Idle,
        Walk,
        Run
    }

    [Header("Referencias")]

    // Referencia al modelo de movimiento.
    [SerializeField] private PlayerMovementModel playerMovementModel;

    // Referencia al Animator del personaje.
    [SerializeField] private Animator animator;

    // NUEVO:
    // Este es el objeto visual que quieres rotar.
    // Puede ser el mismo personaje o el modelo hijo.
    [SerializeField] private Transform characterVisual;

    [Header("Par�metro del Animator")]

    // Nombre del par�metro float del Animator.
    [SerializeField] private string speedParameter = "Speed";

    [Header("Umbrales")]

    // Umbral para considerar Idle.
    [SerializeField] private float idleThreshold = 0.1f;

    // Umbral para considerar Run.
    [SerializeField] private float runThreshold = 4f;

    [Header("Rotaci�n")]

    // Qu� tan r�pido gira el personaje.
    [SerializeField] private float rotationSpeed = 10f;

    // Estado actual detectado.
    public AnimationState CurrentState { get; private set; }

    // Hash del par�metro Speed.
    private int _speedHash;

    private void Start()
    {
        // Convertimos el nombre del par�metro a hash.
        _speedHash = Animator.StringToHash(speedParameter);

        // Revisamos referencias.
        if (playerMovementModel == null)
        {
            Debug.LogError("[PlayerAnimatorView] Falta asignar PlayerMovementModel en el Inspector.");
        }

        if (animator == null)
        {
            Debug.LogError("[PlayerAnimatorView] Falta asignar Animator en el Inspector.");
        }

        if (characterVisual == null)
        {
            Debug.LogError("[PlayerAnimatorView] Falta asignar Character Visual en el Inspector.");
        }

        Debug.Log($"[PlayerAnimatorView] Par�metro de Animator configurado: {speedParameter}");
    }

    private void Update()
    {
        // Actualizamos animaci�n.
        UpdateAnimation();

        // Actualizamos rotaci�n visual.
        UpdateRotation();
    }

    private void UpdateAnimation()
    {
        // Si falta algo, salimos.
        if (playerMovementModel == null || animator == null) return;

        // Tomamos la velocidad actual desde el modelo.
        float speed = playerMovementModel.CurrentSpeed;

        // La enviamos al Animator.
        animator.SetFloat(_speedHash, speed);

        // Debug de Animator.
        if (speed > 0f)
        {
            Debug.Log($"[PlayerAnimatorView] Speed enviada al Animator: {speed}");
            Debug.Log($"[PlayerAnimatorView] Speed le�da dentro del Animator: {animator.GetFloat(_speedHash)}");
        }

        // Determinamos el estado l�gico actual.
        if (speed <= idleThreshold)
        {
            CurrentState = AnimationState.Idle;
        }
        else if (speed < runThreshold)
        {
            CurrentState = AnimationState.Walk;
        }
        else
        {
            CurrentState = AnimationState.Run;
        }

        // Debug del estado.
        if (speed > 0f)
        {
            Debug.Log($"[PlayerAnimatorView] Estado actual detectado: {CurrentState}");
        }
    }

    private void UpdateRotation()
    {
        // Si falta una referencia, no seguimos.
        if (playerMovementModel == null || characterVisual == null) return;

        // Tomamos la direcci�n actual del movimiento.
        Vector3 moveDirection = playerMovementModel.CurrentMoveDirection;

        // Si no hay direcci�n, no rotamos.
        if (moveDirection == Vector3.zero) return;

        // Calculamos la rotaci�n objetivo basada en la direcci�n.
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        // Rotamos suavemente hacia la direcci�n deseada.
        characterVisual.rotation = Quaternion.Slerp(
            characterVisual.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        // Debug �til para confirmar rotaci�n.
        Debug.Log($"[PlayerAnimatorView] Rotando hacia: {moveDirection}");
    }
}