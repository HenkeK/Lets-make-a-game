using UnityEngine;
using System.Collections;

public class Ability2Behavior : MonoBehaviour {

	float colliderRemovalTime = 3f;

	void Start () {
		Destroy(gameObject, 6f);
	
	}
	
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag != "Player")
		{
			Collider2D [] coll2D = coll.gameObject.GetComponents<Collider2D>();
			Collider2D collToAffect;
            foreach (Collider2D c in coll2D) {
				if (gameObject.GetComponent<Collider2D>().IsTouching(c)) {
					collToAffect = c;
					collToAffect.enabled = false;
					StartCoroutine(ReEnableCollider(collToAffect));
					break;
				}
			}
			gameObject.GetComponent<Collider2D>().enabled = false;
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
		else {
			Destroy(gameObject);
		}
	}
	IEnumerator ReEnableCollider(Collider2D coll) {
		yield return new WaitForSeconds(colliderRemovalTime);
		coll.enabled = true;
		Destroy(gameObject);
	}
}
