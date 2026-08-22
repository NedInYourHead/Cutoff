using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// THIS CLASS MANAGES THROWING THE SWORDS
/// </summary>
public class SwordsManager : MonoBehaviour
{
	public GameObject SWORD_PREFAB;

	public float THROW_STRENGTH = 1.0f;

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
		swords.Add(o.GetComponent<SwordScript>());
	}

	/// <summary>
	/// Get the closest sword within `maxDistance` distance, else return null.
	/// </summary>
	SwordScript GetClosestSword(Vector2 mousePos, float maxDistance = 2.4f)
	{
		float d = maxDistance;
		SwordScript closest = null;

		foreach (SwordScript a in swords)
		{
			print(mousePos);
			print(a.transform.position);
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
		Vector2 tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos = tmp;
		if (Input.GetMouseButtonDown(0))
		{
			HeldSword = GetClosestSword(tmp);
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (HeldSword) HeldSword.rb.linearVelocity = (mousePos - (Vector2)HeldSword.transform.position) * THROW_STRENGTH;
			HeldSword = null;
		}
	}
}
