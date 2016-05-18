using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    public int health = 1;
    public int speed = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void Kill () {
        	Destroy(gameObject);
    	}
}
