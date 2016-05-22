using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Character {
    public Text DeathText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public override void Update () {
                base.Update();
	
	}

	public override void Kill () {
		DeathText.text = "You died";
		GameObject.Find("Main Camera").GetComponent<CameraController>().followPlayer = false;
	}
}
