using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
	Rigidbody2D rb;
	Transform trans;
	Vector2 shootDirection;
	public GameObject projectile;
	public GameObject ability1Projectile;
	public LayerMask whatIsGround;

	public int health = 2;
	public float moveSpeed = 10f;
	public float jumpForce = 30f;
	bool grounded = true;
	bool readyToShoot = true;		//If delay between shoots i.e fire rate
	float shotDelay = 0.5f;
	public float shootForce = 1.5f;
	/*bool ability1Ready = true;	//CD for ability1
	float ability1CD = 4f; */

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		trans = GetComponent<Transform>();

		try
		{
			ClientScene.RegisterPrefab(projectile);
			ClientScene.RegisterPrefab(ability1Projectile);
		}
		catch (System.Exception)
		{
			Debug.LogError("One of the projectiles are missing in Player");
		}
	}

	void Update()
	{
		// If this GameObject isn't the local player
		if (!isLocalPlayer)
			return;		// Don't do anything

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
			CmdShoot(projectile, GetShotAngle());
			readyToShoot = false;
			Invoke("ToggleReadyToShoot", shotDelay);
		}

		//Right click ability; ability1
		if (Input.GetMouseButtonDown(1)/*&& ability1Ready*/)
		{
			CmdShoot(ability1Projectile, GetShotAngle());
		}
	}

	void FixedUpdate()
	{
		if (Physics2D.Raycast(trans.position, Vector2.down, 0.56f * trans.localScale.y, whatIsGround).collider != null)
			grounded = true;
		else
			grounded = false;
	}

	[Command]
	void CmdShoot(GameObject bullet, Vector2 angle)
	{
		//Debug.Log(angle.ToString());
		GameObject go = Instantiate(projectile, trans.position, Quaternion.identity) as GameObject;
		NetworkServer.Spawn(go);

		// Doesn't work on connected clients for whatever reason, even though it is exactly like in the reference
		// http://docs.unity3d.com/Manual/UNetActions.html
		//go.GetComponent<Rigidbody2D>().velocity = angle * shootForce;

		// Temporary(?) workaround
		RpcAddForceToProjectile(go, angle * shootForce);
	}

	[ClientRpc]
	void RpcAddForceToProjectile(GameObject projectile, Vector2 force)
	{
		projectile.GetComponent<Rigidbody2D>().velocity = force;
	}

	Vector2 GetShotAngle()
	{
		Vector3 shootTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		shootTowards.z = 0;
		float verticalProjectileForce = shootTowards.y - trans.position.y;
		float horizontalProjectileForce = shootTowards.x - trans.position.x;
		shootDirection = new Vector2(horizontalProjectileForce / (Mathf.Abs(horizontalProjectileForce) + Mathf.Abs(verticalProjectileForce)), verticalProjectileForce / (Mathf.Abs(horizontalProjectileForce) + Mathf.Abs(verticalProjectileForce)));

		return shootDirection;
	}

	void ToggleReadyToShoot()
	{
		readyToShoot = true;
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<SpriteRenderer>().color = HexToColor("4382FFFF");
	}

	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
	}
}
