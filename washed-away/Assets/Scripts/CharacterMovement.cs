using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private PlayerInputActions playerInput;
    private Vector2 currentMovementInput;

    // Add this flag
    [HideInInspector] public bool canMove = true;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerInput.Player.Move.performed += ctx => currentMovementInput = ctx.ReadValue<Vector2>();
        playerInput.Player.Move.canceled += ctx => currentMovementInput = Vector2.zero;
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void FixedUpdate()
    {
        if (!canMove) return; // This stops movement during dialogue

        Vector3 movement = new Vector3(currentMovementInput.x, currentMovementInput.y, 0f);
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Public methods to control movement from other scripts
    public void DisableMovement() => canMove = false;
    public void EnableMovement() => canMove = true;
}