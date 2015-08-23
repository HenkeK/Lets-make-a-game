using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
	Rigidbody2D rb;
	Transform trans;
	Vector2 shootDirection;
	public Transform projectile;
	public Transform ability1Projectile;
	public Transform ability2Projectile;
	public LayerMask whatIsGround;

	public float moveSpeed = 10f;
	public float jumpForce = 30f;
	bool grounded = true;
	bool readyToShoot = true;           //If delay between shoots i.e fire rate
	float shotDelay = 0.5f;
	float verticalProjectileForce;
	float horizontalProjectileForce;
	float shootForce = 1000f;
	/*bool ability1Ready = true;			//CD for ability1
	float ability1CD = 4f; */

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		trans = GetComponent<Transform>();
	}

	void Update()
	{
		// If this GameObject isn't the local player
		if (!isLocalPlayer)
			return;

		//Jump
		if (grounded && Input.GetKeyDown(KeyCode.Space))
			rb.AddForce(Vector2.up * jumpForce);

		//Move left
		if (Input.GetKey(KeyCode.A))
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

		//Move right
		if (Input.GetKey(KeyCode.D))
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

		// If no movement key is pressed, interpolate the velocity towards 0 until it reaches stops
		if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && grounded && rb.velocity.x != 0)
			rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, 0.2f), rb.velocity.y);

		//Shoot
		if (Input.GetMouseButtonDown(0) && readyToShoot)
		{
			Vector3 shootTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			shootTowards.z = 0;
			verticalProjectileForce = shootTowards.y - trans.position.y;
			horizontalProjectileForce = shootTowards.x - trans.position.x;
			shootDirection = new Vector2(horizontalProjectileForce / (Mathf.Abs(horizontalProjectileForce) + Mathf.Abs(verticalProjectileForce)), verticalProjectileForce / (Mathf.Abs(horizontalProjectileForce) + Mathf.Abs(verticalProjectileForce)));

			Transform go = Instantiate(projectile, trans.position, trans.rotation) as Transform;
			go.gameObject.GetComponent<Rigidbody2D>().AddForce(shootDirection * shootForce);

			readyToShoot = false;
			Invoke("toggleReadyToShoot", shotDelay);
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
		//Middle mousebutton ability
		if (Input.GetMouseButtonDown(2)) {
			Vector3 shootTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			shootTowards.z = 0;
			verticalProjectileForce = shootTowards.y - trans.position.y;
			horizontalProjectileForce = shootTowards.x - trans.position.x;
			shootDirection = new Vector2(horizontalProjectileForce / (Mathf.Abs(horizontalProjectileForce) + Mathf.Abs(verticalProjectileForce)), verticalProjectileForce / (Mathf.Abs(horizontalProjectileForce) + Mathf.Abs(verticalProjectileForce)));

			Transform go = Instantiate(ability2Projectile, trans.position, trans.rotation) as Transform;
			go.gameObject.GetComponent<Rigidbody2D>().AddForce(shootDirection * shootForce);
		}
	}

	void FixedUpdate()
	{
		if (Physics2D.Raycast(trans.position, Vector2.down, 0.56f * trans.localScale.y, whatIsGround).collider != null)
			grounded = true;
		else
			grounded = false;
	}

	void toggleReadyToShoot()
	{
		readyToShoot = true;
	}

	public override void OnStartLocalPlayer()
	{
		Debug.Log("!");
		GetComponent<SpriteRenderer>().color = HexToColor("4382FFFF");

		//base.OnStartLocalPlayer();
	}

	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
	}
}
