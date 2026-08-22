using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{

	[SerializeField] private float moveSpeed;
	[SerializeField] private float jumpPower;
	[SerializeField] private float jumpBufferLength;
	[SerializeField] private float xDrag;
	[SerializeField] private float yDrag;
	[SerializeField] private float groundGrip;
	[SerializeField] private float groundCheckDistance;
	[SerializeField] private LayerMask groundCheckLayerMask;

	private float moveInput;
	private bool isGrounded;
	private float jumpBuffer;
	private bool jumping;

	private Rigidbody2D rb;


	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
	}


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

	public void OnJump(InputAction.CallbackContext ctx)
	{
		if (ctx.started)
		{
			jumping = true;
			print("jumping");
		}
		if (ctx.canceled)
		{
			jumping = false;
		}
	}

	private void FixedUpdate()
	{
		if (jumping)
		{
			jumpBuffer = jumpBufferLength;
		}
		isGrounded = Physics2D.Linecast(transform.position, transform.position + Vector3.down * groundCheckDistance, groundCheckLayerMask);

		//Apply move force
		Vector2 moveForce = new Vector2(moveInput * moveSpeed, 0f);
		rb.AddForce(moveForce);

		//Apply jump force
		if (jumpBuffer > 0f)
		{
			if (isGrounded)
			{
				rb.linearVelocityY = 0f;
				rb.AddForce(new Vector2(0f, jumpPower));
				jumpBuffer = 0f;
			}
			else
			{
				jumpBuffer -= Time.fixedDeltaTime;
			}
		}

		//Apply x drag force
		rb.AddForce(new Vector2(AxisDrag(rb.linearVelocityX, xDrag), 0f));

		//Apply y drag force
		rb.AddForce(new Vector2(0f, AxisDrag(rb.linearVelocityY, yDrag)));


		if (isGrounded)
		{
			// Apply extra grip drag when input direction doesn't equal velocity direction
			if (Math.Sign(rb.linearVelocityX) != Math.Sign(moveInput))
			{
				float XMagnitude = Mathf.Abs(rb.linearVelocityX);
				float velocityDirection = Math.Sign(rb.linearVelocityX);
				Vector2 groundGripForce = new Vector2(XMagnitude * -velocityDirection * groundGrip, 0f);
				rb.AddForce(groundGripForce);
			}
		}
	}

	//Apply Squared drag to a 1D force
	private float AxisDrag(float force, float drag)
	{
		float sqrMagnitude = Mathf.Pow(force, 2f);
		float velocityDirection = Math.Sign(force);
		float dragForce = sqrMagnitude * -velocityDirection * drag;
		return dragForce;
	}

}
