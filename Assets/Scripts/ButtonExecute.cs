using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonExecute : MonoBehaviour {
	public float timeToSelect = 5.0f;
	public GameObject progress;
	private float countDown;
	private Clicker clicker = new Clicker();
	private GameObject hitButton;

	private AdsHandler adsHandler;

	void Start () {
		countDown = timeToSelect;
		this.hitButton = null;
		adsHandler = this.GetComponent<AdsHandler> ();
	}

	void Update () {
		Transform camera = Camera.main.transform;
		Ray ray = new Ray (camera.position, camera.rotation * Vector3.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit) && hit.transform.gameObject.tag == "Button") {
			this.hitButton = hit.transform.parent.gameObject;	
			countDown -= Time.deltaTime;
			if (countDown < 0.0f) {	
				this.GetComponent<AdsHandler> ().RequestAd ();
				countDown = timeToSelect;
			}
			RectTransform rectTransform = progress.GetComponent<RectTransform> ();
			rectTransform.sizeDelta = new Vector2 (640.0f * (1 - countDown / timeToSelect), rectTransform.sizeDelta.y); 
		} else {
			countDown = timeToSelect;
			RectTransform rectTransform = progress.GetComponent<RectTransform> ();
			rectTransform.sizeDelta = new Vector2 (0, rectTransform.sizeDelta.y); 
			this.hitButton = null;
		}
	}
}
