using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MenuType
{
    [SerializeField] private Text highscoreText;
    protected override void Awake()
    {
        base.Awake();
        highscoreText.text = Data.HighScore + "";
    }

    public void Play()
    {
        State.SetGameState(Game.Start);
        State.SetMenuState(Menu.Game);
        AudioPlayer.Play(AudioPlayer.ClickUI);
    }


}
