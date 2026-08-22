using System.Collections.Generic;
using UnityEngine;

public class SwordsManager : MonoBehaviour
{
	public List<SwordScript> swords;
	public SwordScript HeldSword = null;

	public static SwordsManager Instance = null;

	void Awake()
	{
		swords = new List<SwordScript>();

		if (Instance) { Debug.LogError("duplicate singleton!"); Destroy(gameObject); }
		Instance = this;
	}

	/// <summary>
	/// Get the closest sword within `maxDistance` distance, else return null.
	/// </summary>
	SwordScript GetClosestSword(Vector2 mousePos, float maxDistance = 0.6f)
	{
		float d = maxDistance;
		SwordScript closest = null;

		foreach (SwordScript a in swords)
		{
			float nd = Vector2.Distance(a.transform.position, mousePos);
			if (nd < d)
			{
				d = nd;
				closest = a;
			}
		}

		print(closest ? closest.name : "nothing grabbed");
		return closest;
	}

	Vector2 mousePos = Vector2.zero;
	void Update()
	{
		Vector2 tmp = Input.mousePosition;
		if (Input.GetMouseButtonDown(0))
		{
			HeldSword = GetClosestSword(tmp);
		}
		// if (Input.GetMouseButton(0))
		// {
		//
		// }
		if (Input.GetMouseButtonUp(0))
		{
			HeldSword.rb.linearVelocity = (mousePos - (Vector2)HeldSword.transform.position) * 12f;
			HeldSword = null;
		}
	}
}
