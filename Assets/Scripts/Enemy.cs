using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character {

        public bool isPatrolling = true;
        public int damage = 1;

        // variables for making enemy blink upon death
        float blinkTimer = 0;
        float maxBlinkTimer = 0.3f;
        float blinkChangeColourTimer = 0.15f;
        int numberOfBlinks = 0;
        int maxNumberOfBlinks = 3;
        Color defaultColour;

        Rigidbody rb;

        List<Transform> waypointList;
        int currentWaypointIndex = 0;

        public override void Start() {
                base.Start();
                rb = GetComponent<Rigidbody>();
                waypointList = GetComponent<TransformList>().transformList;
                defaultColour = gameObject.GetComponent<Renderer>().material.color;
        }

        public virtual void Update() {
                base.Update();

                if (!IsAlive()) {
                        DeathAnimation();
                }

                if (isPatrolling) {
                        transform.LookAt(waypointList [currentWaypointIndex]);
                        rb.AddForce(Vector3.forward * speed);

                        Debug.Log(Vector3.Distance(transform.position, waypointList[currentWaypointIndex].position));
                        if (Vector3.Distance(transform.position, waypointList[currentWaypointIndex].position) < 0.1f) {
                                currentWaypointIndex++;
                                if (currentWaypointIndex >= waypointList.Count) {
                                        currentWaypointIndex = 0;
                                }
                        }
                }

        }

        public override void Kill() {

        }

        void DeathAnimation() {
                blinkTimer += Time.deltaTime;
                if (numberOfBlinks < maxNumberOfBlinks) {
                        if (blinkTimer > maxBlinkTimer) {
                                blinkTimer = 0;
                                numberOfBlinks++;
                        }
                        else if (blinkTimer > blinkChangeColourTimer) {
                                gameObject.GetComponent<Renderer>().material.color = Color.red;
                        }
                        else {
                                gameObject.GetComponent<Renderer>().material.color = defaultColour;
                        }
                }
                else {
                        Destroy(gameObject);
                }
        }

        void OnCollisionEnter(Collision other) {
                if (other.gameObject.tag == "Player") {
                        if (other.gameObject.GetComponent<PlayerController>().isDashing) {
                                health--;
                        }
                        else if (IsAlive()){
                                other.gameObject.GetComponent<Player>().health--;
                        }
                }
        }
}
