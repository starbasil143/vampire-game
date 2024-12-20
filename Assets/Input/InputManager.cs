using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public static bool Casting;
    public static bool Dash;
    public static bool CastingUp;
    public static bool Guard;
    public static bool Continue;
    public static bool Pause;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _dashAction;
    private InputAction _castAction;
    private InputAction _guardAction;
    private InputAction _continueAction;
    private InputAction _pauseAction;


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Move"];
        _dashAction = _playerInput.actions["Dash"];
        _castAction = _playerInput.actions["Cast"];
        _guardAction = _playerInput.actions["Guard"];
        _continueAction = _playerInput.actions["Continue"];
        _pauseAction = _playerInput.actions["Pause"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        Dash = _dashAction.WasPressedThisFrame();
        Casting = _castAction.IsPressed();
        CastingUp = _castAction.WasReleasedThisFrame();
        Guard = _guardAction.WasPressedThisFrame();
        Continue = _continueAction.WasPressedThisFrame();
        Pause = _pauseAction.WasPressedThisFrame();
    }
}
