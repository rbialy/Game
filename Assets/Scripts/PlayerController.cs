﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public float jumpSpeed;
	public Text countText;
	public Text winText;
	public GameObject MainCamera;
	public GameObject Sparks;
	public bool onEvenSurface;
	public float offset = 1.4305f;

	private Rigidbody rb;
	private int count;
	private bool isMoving;
	private bool isRolling;
	private bool isDashing;
	private float dashTime;
	private float gDistance;

	private float forward;
	private float back;
	private float left;
	private float right;

	private Vector3 vForward;
	private Vector3 vBack;
	private Vector3 vLeft;
	private Vector3 vRight;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		Sparks.SetActive (false);
		count = 0;
		isMoving = false;
		isRolling = false;
		isDashing = false;
		gDistance = GetComponent<Collider> ().bounds.extents.y;
		SetCountText ();
		winText.text = "";
		forward = 0;
		back = 0;
		left = 0;
		right = 0;
		vForward = MainCamera.transform.TransformDirection (Vector3.forward);
		vBack = MainCamera.transform.TransformDirection (Vector3.back);
		vLeft = MainCamera.transform.TransformDirection (Vector3.left);
		vRight = MainCamera.transform.TransformDirection (Vector3.right);
	}
	
	void Update(){
		Movement ();
	}
	
	void FixedUpdate(){
		if (isDashing) {
			Dash ();
		}
		
		//Movement Code In relation to Camera Orientation
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		if (moveHorizontal < 0) {
			left = -1;
			right = 0;
		}
		if (moveHorizontal > 0) {
			right = 1;
			left = 0;
		}
		if (moveVertical < 0) {
			back = -1;
			forward = 0;
		}
		if (moveVertical > 0) {
			forward = 1;
			back = 0;
		}
		if (moveHorizontal == 0) {
			left = 0;
			right = 0;
		}
		if (moveVertical == 0) {
			forward = 0;
			back = 0;
		}

		vForward = MainCamera.transform.TransformDirection (Vector3.forward);
		vBack = MainCamera.transform.TransformDirection (Vector3.back);
		vLeft = MainCamera.transform.TransformDirection (Vector3.left);
		vRight = MainCamera.transform.TransformDirection (Vector3.right);

		if (IsGrounded () && isMoving) {
			isRolling = false;
			rb.velocity = SetVelocity(moveHorizontal, moveVertical);
		}
		else if (IsGrounded () && !isMoving) {
			if (!isRolling && onEvenSurface) {
				rb.angularVelocity = new Vector3 (0.0f, 0.0f, 0.0f);
				rb.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
			}
		}
		else if (!IsGrounded () && isMoving) {	
			rb.velocity = SetVelocity(moveHorizontal, moveVertical);
		}
		
		if(XZMag() > speed){
			rb.velocity = new Vector3 ((rb.velocity.x / XZMag()) * speed, rb.velocity.y, (rb.velocity.z / XZMag()) * speed);
		}
		
		if (rb.velocity.y < -30.0f) {
			rb.AddForce (Vector3.up * 30.0f);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive(false);
			count += 1;
			SetCountText ();
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag ("Platform")) {
			if (Vector3.up != collision.transform.up) {
				onEvenSurface = false;
			} else {
				onEvenSurface = true;
			}
		} else {
			onEvenSurface = true;
		}
	}
	
	void SetCountText(){
		countText.text = "Score: " + count.ToString ();
		if (count >= 19) {
			winText.text = "LEVEL COMPLETE";
		}
	}
	
	void Movement(){
		if ((Input.GetKeyDown (KeyCode.Space) || (Input.GetAxis ("RightTrigger") > 0.0f)) && IsGrounded ()) {
			Jump ();
		}
		
		if (Input.GetAxis ("Horizontal") != 0.0f || Input.GetAxis ("Vertical") != 0.0f) {
			isMoving = true;
		}
		else {
			isMoving = false;
		}
		
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			//Rotate Player so that the leaves are facing the direction of attack
			dashTime = 0.2f;
			isDashing = true;
			Sparks.SetActive (true);
		}
	}
	
	void Dash(){
		rb.freezeRotation = true;
		Vector3 destination = new Vector3 (vForward.x * 5.0f, 0.0f, vForward.z * 5.0f);
		rb.MovePosition (transform.position + destination * Time.deltaTime * 6.1f);
		transform.LookAt (new Vector3(MainCamera.transform.position.x, 0.3f,MainCamera.transform.position.z));
		dashTime = dashTime - Time.deltaTime;
		
		if (dashTime <= 0) {
			Sparks.SetActive (false);
			rb.freezeRotation = false;
			isDashing = false;
		}
	}
	
	void Jump(){
		rb.velocity = new Vector3 (rb.velocity.x, jumpSpeed, rb.velocity.z);
		isRolling = true;
	}
	
	Vector3 SetVelocity(float mh, float mv){
		float xVelocity = (mv * speed * vForward.x * offset * forward) + (mv * speed * vBack.x * offset * back) + (mh * speed  * vLeft.x * left) + (mh * speed * vRight.x * right);
		float yVelocity = (mv * speed * offset * vForward.z * forward) + (mv * speed * offset * vBack.z * back) + (mh * speed * vLeft.z * left) + (mh * speed * vRight.z * right);
		return new Vector3 (xVelocity, rb.velocity.y, zVelocity);
	}
	
	bool IsGrounded() {
		// Ignore colliders in layer 8 (npc/player layer)
		int layerMask = 1 << 8;
		layerMask = ~layerMask;
		
		return Physics.Raycast(transform.position, -Vector3.up, gDistance + 0.3f, layerMask);
	}
	
	float XZMag(){
		return Mathf.Sqrt (Mathf.Pow (rb.velocity.x, 2.0f) + Mathf.Pow (rb.velocity.z, 2.0f));
	}
}
