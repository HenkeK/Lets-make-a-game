using UnityEngine;

public class ScoreKeeper : UnityEngine.UI.Image
{
	public Sprite oppositeStateSprite;

	public bool state = false;
	private bool oldState = false;

	void Update()
	{
		if (oldState != state)
		{
			oldState = state;
			OnStateChange(state);
		}
	}

	public void SetState(bool newState)
	{
		if (newState != state)
		{
			Sprite temp = sprite;

			sprite = oppositeStateSprite;
			oppositeStateSprite = temp;
			temp = null;

			state = !state;
		}
	}

	public void OnStateChange(bool newState)
	{

	}
}