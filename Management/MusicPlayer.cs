using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
	private static AudioSource Source;

	void Awake ()
	{
		Source = GetComponent<AudioSource> ();
	}

	public static void Mute (bool mute)
	{
		Source.mute = mute;
	}
}
