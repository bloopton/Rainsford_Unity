using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy"){
		//if currently hostile, damage
		}
	}
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy"){
		//if currently hostile, continue to damage
		}
	}
	void OnCollisionExit2D(Collision2D coll) {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			//become hostile, chase
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			//continue chasing, possibly redundant
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			//turn off hostile BUT continue chasing for fixedTime.
			//return to patrol position/pattern
		}
	}

}
