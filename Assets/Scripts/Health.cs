using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	int maxHealth = 5;
	int currentHealth;
	public TextAsset heart;
	Texture2D heartTex = new Texture2D(10, 10);

	// Use this for initialization
	void Start () {
		heartTex.LoadImage(heart.bytes);
		currentHealth = maxHealth;
		Invoke("TakeDamage",3);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth<=0)
		{
			Destroy(gameObject);
		}
	}

	void TakeDamage (int damage)
	{
		currentHealth -= damage;
	}
	/*void TakeDamage()
	{
		currentHealth--;
		Debug.Log(currentHealth);
	}*/

	void OnGUI ()
	{
		for (int i = 1; i <= currentHealth; i++)
		{
			GUI.DrawTexture(new Rect(280 + 35 * i, 600, 30, 30), heartTex, ScaleMode.ScaleToFit);
		}
	}
}
