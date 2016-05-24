using UnityEngine;
using System.Collections;

public class VisionScript : MonoBehaviour {

	WolfAIScript ws;

	// Use this for initialization
	void Start () {

		ws = gameObject.transform.parent.gameObject.GetComponent<WolfAIScript> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			ws.startHostileTimer = false;
			ws.hostileTimer = 0;
			ws.hostile = true;
			ws.animator.SetBool ("hostile", true);
			//become hostile, chase
			Debug.Log("Triggered");

		}
	}

	/*
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			hostile = true;
			//continue chasing, possibly redundant
		}
	}
	*/

	void OnTriggerExit2D(Collider2D other){
		Debug.Log ("Exitted");
		if (other.gameObject.tag == "Player") {
			ws.startHostileTimer = true;
			//turn off hostile BUT continue chasing for fixedTime.
			//return to patrol position/pattern
			Debug.Log("Triggered");

		}
	}

}
