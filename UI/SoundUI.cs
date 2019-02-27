using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
	[SerializeField]private Image image;
	[SerializeField]private Sprite enabled;
	[SerializeField]private Sprite disabled;

	void Start ()
	{
		AudioPlayer.Mute (!Data.Sound);

		if (Data.Sound)
			image.sprite = enabled;
		else
			image.sprite = disabled;
	}

	public void ToggleSound ()
	{
		Data.Sound = !Data.Sound;
		AudioPlayer.Mute (!Data.Sound);

		if (Data.Sound)
			image.sprite = enabled;
		else
			image.sprite = disabled;
	}

}
