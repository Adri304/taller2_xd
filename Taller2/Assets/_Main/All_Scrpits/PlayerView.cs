using UnityEngine;                  // Librer�a principal de Unity.
using UnityEngine.InputSystem;      // Librer�a del nuevo Input System.

public class PlayerInputController : MonoBehaviour
{
    // Referencia privada a la acci�n de movimiento del Input System.
    private InputAction _moveAction;

    // Propiedad p�blica de solo lectura.
    public Vector2 MoveInput { get; private set; }

    private void Start()
    {
        // Busca la acci�n llamada "Move" dentro del Input Actions Asset.
        _moveAction = InputSystem.actions.FindAction("Move");

        // Si la encuentra, la habilita.
        if (_moveAction != null)
        {
            _moveAction.Enable();
            Debug.Log("[PlayerInputController] Acci�n 'Move' encontrada y habilitada correctamente.");
        }
        else
        {
            Debug.LogError("[PlayerInputController] No se encontr� la acci�n 'Move'.");
        }
    }

    private void Update()
    {
        // Si no existe la acci�n, salimos.
        if (_moveAction == null) return;

        // Leemos el input actual como Vector2.
        MoveInput = _moveAction.ReadValue<Vector2>();

        // Debug solo cuando hay input.
        if (MoveInput != Vector2.zero)
        {
            Debug.Log($"[PlayerInputController] Input detectado: {MoveInput}");
        }
    }

    private void OnDisable()
    {
        // Deshabilitamos la acci�n cuando el objeto se apaga.
        if (_moveAction != null)
        {
            _moveAction.Disable();
            Debug.Log("[PlayerInputController] Acci�n 'Move' deshabilitada.");
        }
    }
}