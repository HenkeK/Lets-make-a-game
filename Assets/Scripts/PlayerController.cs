using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
	Rigidbody2D rb;
	bool grounded = true;
	public float jumpForce = 30f;
	public LayerMask whatIsGround;
	public float moveSpeed = 10f;
	Transform trans;
	public Transform projectile;
	bool readyToShoot = true;			//If delay between shoots i.e fire rate
	float shotDelay = 0.5f;
	float verticalProjectileForce;
	float horizontalProjectileForce;
	Vector2 shootDirection;
	float shootForce = 1000f;
	public Transform ability1Projectile;
	/*bool ability1Ready = true;			//CD for ability1
	float ability1CD = 4f; */

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		trans = GetComponent<Transform> ();
	}

	void Update()
	{
		// If this GameObject isn't the local player
		if (!isLocalPlayer)
			return;
		//Jump
		if (grounded && Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(Vector2.up * jumpForce);
		}
		//Move left
		if (Input.GetKey(KeyCode.A))
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
		//Move right
		if (Input.GetKey(KeyCode.D))
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

		if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && grounded && rb.velocity.x != 0)
			// If no movement key is pressed, interpolate the velocity towards 0 until it reaches stops
			rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, 0.2f), rb.velocity.y);
		//Shoot
		if (Input.GetMouseButtonDown(0)&& readyToShoot)
		{
			Vector3 shootTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			shootTowards.z = 0;
			verticalProjectileForce = shootTowards.y - trans.position.y;
			horizontalProjectileForce = shootTowards.x - trans.position.x;
			shootDirection = new Vector2(horizontalProjectileForce / (Mathf.Abs(horizontalProjectileForce) + Mathf.Abs(verticalProjectileForce)), verticalProjectileForce / (Mathf.Abs(horizontalProjectileForce) + Mathf.Abs(verticalProjectileForce)));
				
			Transform go = Instantiate(projectile, trans.position, trans.rotation) as Transform;
			go.gameObject.GetComponent<Rigidbody2D>().AddForce(shootDirection * shootForce);

			readyToShoot = false;
			Invoke("toggleReadyToShoot",shotDelay);
		}
		//Right click ability; ability1
		if (Input.GetMouseButtonDown(1)/*&& ability1Ready*/)
		{
			Vector3 shootTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			shootTowards.z = 0;
			verticalProjectileForce = shootTowards.y - trans.position.y;
			horizontalProjectileForce = shootTowards.x - trans.position.x;
			shootDirection = new Vector2(horizontalProjectileForce / (Mathf.Abs(horizontalProjectileForce) + Mathf.Abs(verticalProjectileForce)), verticalProjectileForce / (Mathf.Abs(horizontalProjectileForce) + Mathf.Abs(verticalProjectileForce)));

			Transform go = Instantiate(ability1Projectile, trans.position, trans.rotation) as Transform;
			go.gameObject.GetComponent<Rigidbody2D>().AddForce(shootDirection * shootForce);
		}
	}

	/*void OnGUI()
	{
		GUI.Label(new Rect(10, 200, 200, 50), string.Format("x:{0}, y:{1} \nmodX:{2} \nmodY:{3}", Input.mousePosition.x.ToString(), Input.mousePosition.y.ToString(), Camera.main.ScreenToWorldPoint(Input.mousePosition).x.ToString(), Camera.main.ScreenToWorldPoint(Input.mousePosition).y.ToString()));
	}*/

	void FixedUpdate()
	{
		if (Physics2D.Raycast(trans.position, Vector2.down, 0.56f * trans.localScale.y, whatIsGround).collider != null)
			grounded = true;
		else
			grounded = false;

	}
	void toggleReadyToShoot ()
	{
		readyToShoot = true;
	}
}
