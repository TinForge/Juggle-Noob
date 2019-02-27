using System;
using UnityEngine;

/// <summary>
/// Sets State here and dispatches events to subscribers
/// </summary>
public static class State
{
    public static Action OnGameStart;
    public static Action OnGameEnd;

    public static void SetGameState(Game state)
    {
        Debug.Log("Set Game to:" + state);

        switch (state)
        {
            case Game.Start:
                OnGameStart();
                break;
            case Game.End:
                OnGameEnd();
                break;
        }
    }

    public static Action<Menu> OnMenuCalled;

    public static void SetMenuState(Menu state)
    {
        Debug.Log("OnMenu: " + state);
        OnMenuCalled(state);
    }

}

public enum Game
{
    Start,
    End
}

public enum Menu
{
    Intro,
    Main,
    Game
}