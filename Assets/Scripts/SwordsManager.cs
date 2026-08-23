using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// THIS CLASS MANAGES THROWING THE SWORDS
/// </summary>
public class SwordsManager : MonoBehaviour
{
	public GameObject SWORD_PREFAB;

	[Header("Gameplay Variables")]
	public float THROW_STRENGTH = 1.0f;
	public float GRAB_RANGE = 2.0f;
	public float FOLLOW_SPEED = 5.0f;

	[Header("debug")]
	public List<SwordScript> swords;
	public SwordScript HeldSword = null;

	public static SwordsManager Instance = null;

	void Awake()
	{
		swords = new List<SwordScript>();

		print("inst");
		if (Instance) { Debug.LogError("duplicate singleton!"); Destroy(gameObject); }
		Instance = this;
	}
	void Start()
	{
		var o = Instantiate(SWORD_PREFAB, transform);
	}

	/// <summary>
	/// Get the closest sword within `maxDistance` distance, else return null.
	/// </summary>
	SwordScript GetClosestSword(Vector2 mousePos)
	{
		float d = GRAB_RANGE;
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
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (HeldSword)
		{
			Vector2 md = (mousePos - (Vector2)HeldSword.transform.position);
			if (md.magnitude > GRAB_RANGE)
			{
				if (HeldSword) HeldSword.rb.linearVelocity = md.normalized * THROW_STRENGTH;
				HeldSword = null;
			}
			else
			{
				HeldSword.rb.linearVelocity = md.normalized * FOLLOW_SPEED;
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			HeldSword = GetClosestSword(mousePos);
		}
	}
}
