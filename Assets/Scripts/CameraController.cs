using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public Transform player;
	public float turnspeed;

	private Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		offset = new Vector3 (player.position.x, player.position.y + 10.0f, player.position.z - 10.0f);
	}
	void LateUpdate () {
		offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * turnspeed, Vector3.up) * offset;
		transform.position = player.position + offset;
		transform.LookAt (player.position);
	}
}
