using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public void OpenPlaceInfo(Animator animator) {
		animator.SetBool("open", true);
	}

	public void ClosePlaceInfo(Animator animator) {
		animator.SetBool("open", false);
	}
}
