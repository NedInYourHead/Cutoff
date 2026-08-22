using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
	public List<SwordScript> swords;
	public SwordScript HeldSword = null;

	void Awake()
	{
		swords = new List<SwordScript>();
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
			float nd = Vector2.Distance(a.pos, mousePos);
			if (nd < d)
			{
				d = nd;
				closest = a;
			}
		}

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
			HeldSword.vel = mousePos - HeldSword.pos;
			HeldSword = null;
		}
	}
}
