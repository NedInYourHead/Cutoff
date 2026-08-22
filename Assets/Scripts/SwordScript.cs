using Unity.VisualScripting;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
	public Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
}

