using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
	[SerializeField]private ParticleSystem stars;
	public static ParticleSystem Stars;
	[SerializeField]private ParticleSystem confetti;
	public static ParticleSystem Confetti;

	void Awake ()
	{
		Stars = stars;
		Confetti = confetti;
	}

	public static void Play (ParticleSystem particle)
	{
		if (!particle.isPlaying)
			particle.Play ();
	}

}
