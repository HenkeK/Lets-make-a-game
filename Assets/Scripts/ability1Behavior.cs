using UnityEngine;
using System.Collections;

public class ability1Behavior : MonoBehaviour {

	float pushRadius = 4f;
	float basePushForce = 300f;

	// Use this for initialization
	void Start () {

		Destroy(gameObject, 3f);
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{

	}

	// Update is called once per frame
	void Update () {
	
	}
}
