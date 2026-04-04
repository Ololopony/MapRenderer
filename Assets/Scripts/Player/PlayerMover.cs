using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    private float playerSpeed = 5.0f;
    private float gravityValue = -9.81f;

    [SerializeField]
    private CharacterController controller;
    private Vector3 playerVelocity;

    [Header("Input Actions")]
    [SerializeField]
    private InputActionReference moveAction;

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    void FixedUpdate()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
            transform.forward = move;

        playerVelocity.y += gravityValue * Time.deltaTime;

        Vector3 finalMove = move * playerSpeed + Vector3.up * playerVelocity.y;
        controller.Move(finalMove * Time.deltaTime);
    }
}
