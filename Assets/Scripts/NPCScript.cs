using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCScript : MonoBehaviour {

	public GameObject NPCChat;
	public string text;
	public Text message;

	private float timer;
	private int count;

	void Start(){
		count = 0;
	}
	void Update (){
		timer -= Time.deltaTime;
		if ((timer <= 0f && NPCChat.activeSelf) && (count < text.Length)) {
			message.text = message.text + text[count];
			count += 1;
			timer = 0.01f;
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			NPCChat.SetActive(true);
		}
	}
	void OnTriggerExit(Collider other){
		count = 0;
		message.text = "";
		if (other.gameObject.CompareTag ("Player")) {
			NPCChat.SetActive (false);
		}
	}
}
