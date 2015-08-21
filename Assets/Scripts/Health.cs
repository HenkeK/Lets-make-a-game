using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	int maxHealth = 5;
	int currentHealth = 1;
	public TextAsset heart;
	Texture2D heartTex = new Texture2D(10, 10);
	Animator anim;

	void Start () {
		heartTex.LoadImage(heart.bytes);
		currentHealth = maxHealth;
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		if (currentHealth<=0)
		{
			if (currentHealth < 0)
				currentHealth = 0;
			anim.SetBool("isDead", true);
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
	void GainHP(int hp) {
		currentHealth += hp;
	}
	void GainHP() {
		currentHealth++;
	}
	void RefillHealth() {
		currentHealth = maxHealth;
	}

	void OnGUI ()
	{

		for (int i = 1; i <= currentHealth; i++)
		{
			GUI.DrawTexture(new Rect(370 + 35 * i, 460, 30, 30), heartTex, ScaleMode.ScaleToFit);
		}
	}
}
