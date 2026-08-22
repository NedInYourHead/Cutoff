using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    private float moveInput;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            moveInput = ctx.ReadValue<float>();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX += moveInput * moveSpeed * Time.fixedDeltaTime;
    }
}
