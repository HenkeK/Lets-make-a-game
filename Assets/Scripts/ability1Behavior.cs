using UnityEngine;
using System.Collections;

public class ability1Behavior : MonoBehaviour {

	float pushRadius = 7f;
	float basePushForce = 3000f;
	Transform trans;
	public LayerMask whatToPush;

	void Start () {
		trans = GetComponent<Transform>();
		Destroy(gameObject, 3f);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (Physics2D.OverlapCircle(gameObject.transform.position, pushRadius, whatToPush)) {
			GameObject player = Physics2D.OverlapCircle(gameObject.transform.position, pushRadius, whatToPush).gameObject;
			float verticalPushForce = (player.transform.position.y - trans.position.y) / (Mathf.Abs(player.transform.position.x - trans.position.x) + Mathf.Abs(player.transform.position.y - trans.position.y));
			float horizontalPushForce = (player.transform.position.x - trans.position.x)/ (Mathf.Abs(player.transform.position.x - trans.position.x) + Mathf.Abs(player.transform.position.y - trans.position.y));
			Vector2 appliedPushForce = new Vector2(horizontalPushForce * basePushForce /(1f *  Mathf.Abs(player.transform.position.x-trans.position.x)), verticalPushForce * basePushForce / (1f * Mathf.Abs(player.transform.position.y - trans.position.y)));
			//Vector2 appliedPushForce = new Vector2((horizontalPushForce * basePushForce) - (300f * Mathf.Abs(player.transform.position.x - trans.position.x)), (verticalPushForce * basePushForce) - (300f * Mathf.Abs(player.transform.position.y - trans.position.y)));
			player.GetComponent<Rigidbody2D>().AddForce(appliedPushForce);
			Debug.Log(Mathf.Abs(player.transform.position.x-trans.position.x));
			Debug.Log(appliedPushForce);
			Debug.Log(horizontalPushForce);
		}
		Destroy(gameObject);
	}

	void Update () {
	
	}
}
