using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	int maxHealth = 5;
	int currentHealth = 1;
	public TextAsset heart;
	Texture2D heartTex = new Texture2D(10, 10);

	void Start () {
		heartTex.LoadImage(heart.bytes);
		currentHealth = maxHealth;
	}
	
	void Update () {
		if (currentHealth<=0)
		{
			//Destroy(gameObject);
		}
		if (Input.GetKeyDown(KeyCode.T))
			currentHealth--;
	}

	void TakeDamage (int damage)
	{
		currentHealth -= damage;
	}
	void TakeDamage()
	{
		currentHealth--;
		Debug.Log(currentHealth);
	}

	void OnGUI ()
	{

		for (int i = 1; i <= currentHealth; i++)
		{
			GUI.DrawTexture(new Rect(370 + 35 * i, 460, 30, 30), heartTex, ScaleMode.ScaleToFit);
		}
	}
}
