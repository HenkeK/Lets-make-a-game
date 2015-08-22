using UnityEngine;

public class BasicShot : ProjectileBehavior
{
	public override void OnEnemyPlayerHit(Collider2D coll)
	{
		if (coll.name != shooter)
		{
			if (coll.tag == "Player")
			{
				coll.SendMessage("RpcTakeDamage", 1);
				Destroy(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}
