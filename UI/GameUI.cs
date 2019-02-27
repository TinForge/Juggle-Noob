using UnityEngine;
using UnityEngine.UI;

public class GameUI : MenuType
{
	[SerializeField] private Text scoreText;

	void Awake ()
	{
		base.Awake ();
		ScoreKeeper.ScoreChange += SetScore;
	}

	public void SetScore (int score)
	{
		scoreText.text = score + "";
	}

}
