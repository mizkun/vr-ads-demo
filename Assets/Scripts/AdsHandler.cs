using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using GoogleMobileAds.Android;
using System;
using UnityEngine.UI;

public class AdsHandler : MonoBehaviour {
	public const string NativeAdUnitId = "/6499/mizutani_test_unit";
	public const string TemplateId = "10096530";
	public const string ImageName = "MainImage";
	public String AdStatus = "Not Requested Yet";

	private bool nativeAdLoaded;
	private CustomNativeTemplateAd nativeAd;

	void Start () {
		this.nativeAd = null;
		this.nativeAdLoaded = false;
	}

	void Update () {
		if (this.nativeAdLoaded) {
			ChangeTextures ();
		}
	}

	public void RequestAd () {
		Debug.Log("Requested!");
		AdLoader adLoader = new AdLoader.Builder(NativeAdUnitId)
			.forCustomNativeAd(TemplateId)
			.Build();
		adLoader.onCustomNativeTemplateAdLoaded += this.HandleCustomNativeAdLoaded;
		adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
		adLoader.LoadAd(new AdRequest.Builder().Build());
	}

	private void HandleCustomNativeAdLoaded(object sender, CustomNativeEventArgs args) {
		this.nativeAdLoaded = true;
		this.nativeAd = args.nativeAd;
		this.nativeAd.RecordImpression ();
		Debug.Log("HandleCustomNativeAdLoaded");
	}

	private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		Debug.Log("HandleNativeAdFailedToLoad");
	}

	private void ChangeTextures () {
		Debug.Log("ChangeTextures");
		GameObject.FindWithTag ("Ads")
			.GetComponent<Renderer> ()
			.material
			.mainTexture = this.nativeAd.GetTexture2D (ImageName);
		this.nativeAdLoaded = false;
	}

	public CustomNativeTemplateAd getNativeAd (){
		return this.nativeAd;
	}
}
