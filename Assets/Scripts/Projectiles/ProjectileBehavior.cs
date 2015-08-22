using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
	public bool autoDestroy = false;
	public float timeBeforeAutoDestroy = 3f;
	[HideInInspector]
	public string shooter;

	void Start()
	{
		if (autoDestroy)
		{
			Destroy(gameObject, timeBeforeAutoDestroy); 
		}
	}

	public virtual void OnEnemyPlayerHit(Collider2D coll)
	{
		Debug.Log("Hit enemy! Override OnEnemyPlayerHit to stop this message");
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		OnEnemyPlayerHit(coll);
	}
}
