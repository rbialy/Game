using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    public int health = 1;
    public int speed = 10;
	// Use this for initialization
	public virtual void Start () {
	
	}
	
	// Update is called once per frame
	public virtual void Update () {
                if (!IsAlive()) {
                        Kill();
                }
	}

        public bool IsAlive() {
                return health > 0;
        }

	public virtual void Kill () {
        	Destroy(gameObject);
    	}
}
