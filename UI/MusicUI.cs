using UnityEngine;
using UnityEngine.UI;

public class MusicUI : MonoBehaviour
{
	[SerializeField]private Image image;
	[SerializeField]private Sprite enabled;
	[SerializeField]private Sprite disabled;

	void Start ()
	{
		MusicPlayer.Mute (!Data.Music);

		if (Data.Music)
			image.sprite = enabled;
		else
			image.sprite = disabled;
	}

	public void ToggleMusic ()
	{
		Data.Music = !Data.Music;
		MusicPlayer.Mute (!Data.Music);

		if (Data.Music)
			image.sprite = enabled;
		else
			image.sprite = disabled;
	}
}
