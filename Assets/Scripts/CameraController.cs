using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public Transform player;
    	public bool followPlayer = true;
	public float turnspeed;
	public float cameraYDistance = 10;
	public float cameraZDistance = 10;
	
	// How far the camera is from the player
	private Vector3 offset;

	// Use this for initialization
	void Start() {
		offset = new Vector3 (player.position.x, player.position.y + cameraYDistance, player.position.z - cameraZDistance);
	}
	
	void LateUpdate () {
	        if (followPlayer) {
	        	offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnspeed, Vector3.up) * offset;
	        	transform.position = player.position + offset;
	        }
		transform.LookAt (player.position);
	}
}
