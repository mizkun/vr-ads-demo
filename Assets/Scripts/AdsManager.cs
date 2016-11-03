using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using GoogleMobileAds.Android;
using System;
using UnityEngine.UI;


// this custom script is attached to the main camera
public class AdsManager : MonoBehaviour {

	// GameObjects we make public so we can set them in the UI
	public Button requestAdButton;
	public Material cubeMaterial;
	public Material GroundTextMaterial;
	public Font TextFont;

	// constant strings we need to make native ad request
//	public const string NativeAdUnitId = "/6499/example/unity-custom-native"; 
//	public const string TemplateId = "10085730"; 
//	public const string ImageName = "Image2";

	public const string NativeAdUnitId = "/6499/mizutani_test_unit";
	public const string TemplateId = "10096530";
	// https://ics.corp.google.com/dfp/6499#delivery/CreateCustomAdFormat/creativeTemplateId=10088490
	public const string ImageName = "MainImage";

	private bool nativeAdLoaded;

	// this object will hold the native components
	private CustomNativeTemplateAd nativeAd;

	void Start () {
		this.nativeAdLoaded = false;
		// register listener for ad request button
		requestAdButton.onClick.AddListener (RequestNativeAd);
	}

	// called on each frame
	void Update() {
		if (this.nativeAdLoaded) 
			// change texture of objects once loaded
			ChangeTextures ();
	}

	// make native ad request
	private void RequestNativeAd()
	{
		// create AdLoader object which requires the native template ID
		AdLoader adLoader = new AdLoader.Builder(NativeAdUnitId)
			.forCustomNativeAd(TemplateId)
			.Build();

		// add delegates to event handlers for success & fail
		adLoader.onCustomNativeTemplateAdLoaded += this.HandleCustomNativeAdLoaded;
		adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;

		// load the ad
		adLoader.LoadAd(new AdRequest.Builder().Build());
	}


	// called when ad finishes loading; provides reference to native ad
	private void HandleCustomNativeAdLoaded(object sender, CustomNativeEventArgs args)
	{
		this.nativeAdLoaded = true;
		this.nativeAd = args.nativeAd;
	}

	// called if ad fails to load
	private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		// TODO(mdiblasio): handle failed condition by alerting user
	}
		
	// change the texture of the cube object
	private void ChangeTextures () {

		// Method 1: change texture of existing material (on Cube1)
		cubeMaterial.mainTexture = this.nativeAd.GetTexture2D(ImageName);

		// Method 2: get reference to object & it's material and change texture
		GameObject.FindWithTag("Cube2")
			.GetComponent<Renderer>()
			.material
			.mainTexture = this.nativeAd.GetTexture2D(ImageName);


		// TODO(mdiblasio): add text to the ground
//		// change ground text
//		GameObject textObject = new GameObject("GroundText");
//		GameObject ground = GameObject.FindWithTag("Ground");
//		textObject.transform.parent = ground.transform;
//		textObject.transform.position = new Vector3(0, 0.1f, 0);
//		textObject.AddComponent<TextMesh>();
//
//		TextMesh textMeshComponent = textObject.GetComponent<TextMesh>();
//		MeshRenderer meshRendererComponent = textObject.GetComponent<MeshRenderer>();
//
//		// TODO(mdiblasio): change to 'DisplayText'
//		// string adText = this.nativeAd.GetText("MainText").Replace('-', '\n');
//		string adText = this.nativeAd.GetText("DisplayText").Replace('-', '\n');
//
//		textMeshComponent.text = adText;
//		textMeshComponent.fontSize = 8;
//		textMeshComponent.anchor = TextAnchor.MiddleCenter;
//		textMeshComponent.transform.Rotate(new Vector3(90, 0, 0));
//		textMeshComponent.font = this.TextFont;
//		meshRendererComponent.material = this.GroundTextMaterial;



	}
		

}























