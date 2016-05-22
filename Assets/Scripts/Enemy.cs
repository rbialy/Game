using UnityEngine;
using System.Collections;

public class Enemy : Character {

        // variables for making enemy blink upon death
        float blinkTimer = 0;
        float maxBlinkTimer = 0.3f;
        float blinkChangeColourTimer = 0.15f;
        int numberOfBlinks = 0;
        int maxNumberOfBlinks = 3;
        Color defaultColour;

        int damage = 1;

        public override void Start() {
                base.Start();
                defaultColour = gameObject.GetComponent<Renderer>().material.color;
        }

        public virtual void Update() {
                base.Update();

                if (!IsAlive()) {
                        DeathAnimation();
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
