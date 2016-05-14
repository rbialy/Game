using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Killzone : MonoBehaviour {

	public Text DeathText;
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			DeathText.text = "You Died";
		}
	}
}
