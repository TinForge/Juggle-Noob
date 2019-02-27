using UnityEngine;

public class Master : MonoBehaviour
{
    void Start()
    {
        State.SetMenuState(Menu.Intro);
        Application.targetFrameRate = 60;
    }

    void OnApplicationFocus(bool state)
    {
        if (state)
        {
            Application.targetFrameRate = 60;
        }
    }


    void Update()
    {
        //Gravity Wave
        float r = Mathf.PingPong(Time.timeSinceLevelLoad * 3, Data.GravityMax - Data.GravityMin) + Data.GravityMin;
        Physics2D.gravity = Vector2.down * r;
    }

}
