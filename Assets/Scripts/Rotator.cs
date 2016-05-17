using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	public float xRotationSpeed = 80;
	public float yRotationSpeed = 50;
	public float zRotationSpeed = 100;
	
	void Update () {
		transform.Rotate (new Vector3 (xRotationSpeed, yRotationSpeed, zRotationSpeed) * Time.deltaTime);
	}
}
