using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private InputAction _moveAction;

    public Vector2 MoveInput { get; private set; }

    // 👇 NUEVO
    public bool JumpPressed { get; private set; }

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");

        if (_moveAction != null)
        {
            _moveAction.Enable();
            Debug.Log("[PlayerInputController] Acción 'Move' habilitada.");
        }
        else
        {
            Debug.LogError("[PlayerInputController] No se encontró 'Move'.");
        }
    }

    private void Update()
    {
        if (_moveAction == null) return;

        MoveInput = _moveAction.ReadValue<Vector2>();

        // 👇 SALTO (tecla espacio)
        JumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;

        if (MoveInput != Vector2.zero)
        {
            Debug.Log($"[PlayerInputController] Input: {MoveInput}");
        }
    }

    private void OnDisable()
    {
        if (_moveAction != null)
        {
            _moveAction.Disable();
        }
    }
}