using UnityEngine;
using UnityEngine.UI;

public class MainUI : MenuType
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highscoreText;
	[SerializeField] private Image highscoreImage;
	public Color gold;
	public Color grey;

	void Awake()
    {
        base.Awake();

        ScoreKeeper.ScoreChange += SetScore;
        ScoreKeeper.HighscoreChange += SetHighscore;
		ScoreKeeper.HighscoreColorChange += SetHighscoreColor;

		SetHighscore(0);
    }

    public void SetScore(int score)
    {
        scoreText.text = score + "";
    }

    public void SetHighscore(int highscore)
    {
        highscoreText.text = "Highest: " + Data.HighScore + "";
    }

	public void SetHighscoreColor(bool isHighscore)
	{
		if (isHighscore)
			highscoreImage.color = gold;
		else
			highscoreImage.color = grey;

	}

    public void Replay()
    {
        State.SetGameState(Game.Start);
        State.SetMenuState(Menu.Game);
        AudioPlayer.Play(AudioPlayer.ClickUI);
    }

    public void Return()
    {
        State.SetMenuState(Menu.Main);
        AudioPlayer.Play(AudioPlayer.ClickUI);
    }

}
