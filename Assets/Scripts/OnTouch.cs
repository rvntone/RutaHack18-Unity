using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouch : MonoBehaviour {

	TouchPhase touchPhase = TouchPhase.Ended;
	Animator animator;
	UIManager uiManager;
 
	// Use this for initialization
	void Start () {
		uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
		animator = GameObject.Find("PlaceInfo").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase){
			Vector3 position;
			if(Input.GetMouseButtonDown(0)){
				position = Input.mousePosition;
			}else{
				position = Input.GetTouch(0).position;
			}
			Ray ray = Camera.main.ScreenPointToRay(position);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Places"){
				uiManager.OpenPlaceInfo(animator);
			}else{
				Debug.Log(hit.transform.gameObject.tag);
			}
		}
	}
}
