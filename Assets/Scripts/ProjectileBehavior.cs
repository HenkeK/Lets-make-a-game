using UnityEngine;
using System.Collections;

public class ProjectileBehavior : MonoBehaviour
{
	public bool autoDestroy = false;
	public float timeBeforeAutoDestroy = 3f;
	public GameObject shooter;

	void Start()
	{
		if (autoDestroy)
		{
			Destroy(gameObject, timeBeforeAutoDestroy); 
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		/*if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Player2")
		{
			coll.gameObject.SendMessage("loseHealth", 1f);
		}*/

		Destroy(gameObject);
	}
}
