using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;
	bool grounded = false;
	public float jumpForce = 30f;
	public Transform feet;
	public LayerMask whatIsGround;
	public float moveSpeed = 10f;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (grounded && Input.GetButton (KeyCode.Space)) {
			rb.AddForce (Vector2.up * jumpForce);
			grounded = false;
		}

		if (Input.GetKey (KeyCode.A))
			rb.velocity = new Vector2 (-moveSpeed, rb.velocity.y); 

		if (Input.GetKey (KeyCode.D))
			rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);
	
	}

	void FixedUpdate () {

		if (Physics2D.Raycast (feet.position, -Vector2.up, 2f, whatIsGround).collider != null)
			grounded = true;

	}
}
