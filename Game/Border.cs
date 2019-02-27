using UnityEngine;

public class Border : StateListener
{
    private bool active;

    private BoxCollider2D collider;

    void Awake()
    {
        base.Awake();
        collider = GetComponent<BoxCollider2D>();
    }

    #region State

    protected override void Activate()
    {
        active = true;
        collider.enabled = true;
    }

    protected override void Deactivate()
    {
        active = false;
        collider.enabled = false;

    }

    #endregion


    void OnTriggerExit2D()
    {
        if (active)
            GameOver();

    }

    void GameOver()
    {
        State.SetGameState(Game.End);
        State.SetMenuState(Menu.Main);
        AudioPlayer.Play(AudioPlayer.GameOver);
    }

}
