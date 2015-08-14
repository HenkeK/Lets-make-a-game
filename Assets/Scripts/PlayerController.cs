using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
	Rigidbody2D rb;
	bool grounded = true;
	public float jumpForce = 30f;
	public Transform feet;
	public LayerMask whatIsGround;
	public float moveSpeed = 10f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		// If this GameObject isn't the local player
		if (!isLocalPlayer)
			return;

		if (grounded && Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(Vector2.up * jumpForce);
			//grounded = false;
		}

		if (Input.GetKey(KeyCode.A))
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

		if (Input.GetKey(KeyCode.D))
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

	}

	void FixedUpdate()
	{
		if (feet != null)
			if (Physics2D.Raycast(feet.position, -Vector2.up, 2f, whatIsGround).collider != null)
				grounded = true;
	}
}
