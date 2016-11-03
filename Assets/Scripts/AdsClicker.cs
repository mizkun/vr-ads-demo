using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Android;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class AdsClicker : MonoBehaviour {
	public float timeToSelect = 5.0f;
	public GameObject progress;
	public GameObject adsTextCanvas;
	public Text adsText;
	
	private float countDown;
	private GameObject hitAds;

	private AdsHandler adsHandler;

	void Start () {
		countDown = timeToSelect;
		adsTextCanvas.GetComponent<Canvas> ().enabled = false;
		this.hitAds = null;
		adsHandler = this.GetComponent<AdsHandler> ();
	}

	void Update () {
		Transform camera = Camera.main.transform;
		Ray ray = new Ray (camera.position, camera.rotation * Vector3.forward);
		RaycastHit hit;
		CustomNativeTemplateAd nativeAd = adsHandler.getNativeAd ();

		if (Physics.Raycast (ray, out hit) && hit.transform.gameObject.tag == "AdsCube" && nativeAd != null) {
			this.adsText.text = nativeAd.GetText ("DisplayText");
			adsTextCanvas.GetComponent<Canvas> ().enabled = true;
			this.hitAds = hit.transform.parent.gameObject;
			countDown -= Time.deltaTime;
			if (countDown < 0.0f) {
				//nativeAd.PerformClick ("VR HA Test");
				SceneManager.LoadScene (nativeAd.GetText ("NextScene"));
				countDown = timeToSelect;
			}
			RectTransform rectTransform = progress.GetComponent<RectTransform> ();
			rectTransform.sizeDelta = new Vector2 (640.0f * (1 - countDown / timeToSelect), rectTransform.sizeDelta.y); 
		} else {
			adsTextCanvas.GetComponent<Canvas> ().enabled = false;
			countDown = timeToSelect;
			RectTransform rectTransform = progress.GetComponent<RectTransform> ();
			rectTransform.sizeDelta = new Vector2 (0, rectTransform.sizeDelta.y); 
			this.hitAds = null;
		}
	}
}
