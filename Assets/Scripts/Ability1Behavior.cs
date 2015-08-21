using UnityEngine;
using System.Collections;

public class Ability1Behavior : ProjectileBehavior
{
	void Start()
	{
		Destroy(gameObject, 3f);
	}
}
