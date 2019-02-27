using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : StateListener
{
	private float timestamp = 0;

	private InterstitialAd interstitialAd;
	private AdRequest adRequest;

	[Header ("Display Settings")]
	[SerializeField][Range (0f, 1f)] private float chance;
	[SerializeField][Range (0, 60)] private int minimumTime;

	void Start ()
	{
		#if UNITY_ANDROID
		string appID = "ca-app-pub-5012032968577559~4853486995";
		#else
		string appID = "Dummy";
		#endif

		MobileAds.Initialize (appID);
		RequestInterstitial ();
	}

	bool RNG ()
	{
		if (Time.timeSinceLevelLoad - timestamp > minimumTime)
		if (UnityEngine.Random.Range (0, 1) < chance)
			return true;
		return false;
	}

	private void RequestInterstitial ()
	{
		#if UNITY_ANDROID
		string interstitialAdID = "ca-app-pub-5012032968577559/4905457885";
		#else
		string interstitialAdID = "dummy";
		#endif

		if (interstitialAd != null)
			interstitialAd.Destroy ();

		interstitialAd = new InterstitialAd (interstitialAdID);
		adRequest = new AdRequest.Builder ().Build ();
		interstitialAd.LoadAd (adRequest);

		interstitialAd.OnAdLoaded += HandleOnAdLoaded;
		interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		interstitialAd.OnAdOpening += HandleOnAdOpened;
		interstitialAd.OnAdClosed += HandleOnAdClosed;
		interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
	}

	#region States

	protected override void Activate ()
	{
		RequestInterstitial ();
	}

	protected override void Deactivate ()
	{
		if (RNG ())
			ShowAd ();
	}

	#endregion

	void ShowAd ()
	{
		if (interstitialAd.IsLoaded ()) {
			timestamp = Time.timeSinceLevelLoad;
			interstitialAd.Show ();
		}
	}

	#region AdCallbacks

	public void HandleOnAdLoaded (object sender, EventArgs args)
	{
		Debug.Log ("loaded ad");
	}

	public void HandleOnAdFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		Debug.Log ("failed to load ad");
	}

	public void HandleOnAdOpened (object sender, EventArgs args)
	{
		AudioPlayer.Mute (true);
		MusicPlayer.Mute (true);
		Debug.Log ("opened ad");
		//disable sound
	}

	public void HandleOnAdClosed (object sender, EventArgs args)
	{
		AudioPlayer.Mute (false);
		MusicPlayer.Mute (false);
		Debug.Log ("closed ad");
		//enable sound
	}

	public void HandleOnAdLeavingApplication (object sender, EventArgs args)
	{
		Debug.Log ("leftapp ad");
	}

	#endregion

}
