using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

	public GameObject Camera;

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeSelf) {
			transform.rotation = Quaternion.LookRotation (transform.position - Camera.transform.position);
		}
	}
}
