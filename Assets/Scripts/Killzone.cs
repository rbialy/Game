using UnityEngine;
using System.Collections;

public class Killzone : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
            		other.gameObject.GetComponent<Player>().Kill();
		}
	}
}
