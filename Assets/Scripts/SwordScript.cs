using Unity.VisualScripting;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
	public Vector2 pos;
	public Vector2 vel;
	private Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		transform.position = transform.position + (Vector3)(vel * Time.deltaTime);
		vel -= new Vector2(0, -1 * Time.deltaTime);
		rb.linearVelocity = vel * 60;
	}

	void LateUpdate()
	{
		transform.position = pos;
	}
}

