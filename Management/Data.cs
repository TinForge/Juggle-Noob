using UnityEngine;

public class Data : MonoBehaviour
{
    #region Ball Data

    public static int MaxForce = 100;
    public static int Frequency = 10;
    public static float Dampening = 1f;
    public static float GravityMin = 7;
    public static float GravityMax = 30;
    public static float Drag = 1;
    public static float Handicap = 2;

    #endregion

    #region PlayerPrefs

    private const string FirstTimeKEY = "firsttime";
    private const string HighScoreKEY = "highscore";
    private const string MusicKEY = "music";
    private const string SoundKEY = "sound";

    void Awake()
    {
        if (!PlayerPrefs.HasKey(FirstTimeKEY))
        {
            FirstTime = 1;
            HighScore = 0;
            Music = true;
            Sound = true;
        }
    }

    public static int FirstTime
    {
        get { return PlayerPrefs.GetInt(FirstTimeKEY); }
        set { PlayerPrefs.SetInt(FirstTimeKEY, value); }
    }

    public static int HighScore
    {
        get { return PlayerPrefs.GetInt(HighScoreKEY); }
        set { PlayerPrefs.SetInt(HighScoreKEY, value); }
    }

    public static bool Music
    {
        get { return PlayerPrefs.GetInt(MusicKEY) > 0 ? true : false; }
        set { PlayerPrefs.SetInt(MusicKEY, value == true ? 1 : -1); }
    }

    public static bool Sound
    {
        get { return PlayerPrefs.GetInt(SoundKEY) > 0 ? true : false; }
        set { PlayerPrefs.SetInt(SoundKEY, value == true ? 1 : -1); }
    }

    #endregion

}
