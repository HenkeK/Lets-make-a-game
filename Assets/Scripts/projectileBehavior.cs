﻿using UnityEngine;
using System.Collections;

public class projectileBehavior : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D coll)
	{
		/*if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Player2")
		{
			coll.gameObject.SendMessage("loseHealth", 1f);
		}*/
		Destroy(gameObject);

	}
}