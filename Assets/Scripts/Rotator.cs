using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	void Update () {
		transform.Rotate (new Vector3 (80, 50, 100) * Time.deltaTime);
	}
}
