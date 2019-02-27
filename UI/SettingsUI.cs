using UnityEngine;
using UnityEngine.UI;

public class SettignsUI : MenuType
{
	void Awake ()
	{
		base.Awake ();
	}

	public void Return ()
	{
		State.SetMenuState (Menu.Main);
		AudioPlayer.Play (AudioPlayer.ClickUI);
	}



}
