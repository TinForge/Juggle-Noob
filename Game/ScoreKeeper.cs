using System;
using System.Collections.Generic;
using UnityEngine.Analytics;
using UnityEngine;

public class ScoreKeeper : StateListener
{
	private static int score = 0;

	public static Action<int> ScoreChange;
	public static Action<int> HighscoreChange;
	public static Action<bool> HighscoreColorChange;

	void Awake ()
	{
		base.Awake ();
	}

	void Start ()
	{
		//ScoreChange (score);
		//HighscoreChange (Data.HighScore);
	}


	#region State

	protected override void Activate ()
	{
		score = 0;
		ScoreChange (score);
		HighscoreColorChange(false);
	}

	protected override void Deactivate ()
	{
		CompareHighScore ();
			Analytics.CustomEvent ("AnalyticData", new Dictionary<string,object>{ { "highscore",score } });
	}

	#endregion

	#region external

	public static void SetScore ()
	{
		score++;
		ScoreChange (score);
		if (score > Data.HighScore)
			HighscoreColorChange(true);


	}

	#endregion

	#region internal

	protected static void CompareHighScore ()
	{
		if (Data.HighScore < score) {
			Data.HighScore = score;
			HighscoreChange (Data.HighScore);


			AudioPlayer.Play (AudioPlayer.Highscore);
			ParticlePlayer.Play (ParticlePlayer.Confetti);
		}
	}

	#endregion
}
