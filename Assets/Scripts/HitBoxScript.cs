using UnityEngine;
using System.Collections;

public class HitBoxScript : MonoBehaviour {

	WolfAIScript ws;

	// Use this for initialization
	void Start () {
	
		ws = gameObject.transform.parent.gameObject.GetComponent<WolfAIScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy"){
			//if currently hostile, damage
			if (ws.hostile) {
				coll.gameObject.GetComponent<HealthScript> ().damage ();
			}
		}
	}
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy"){
			//if currently hostile, continue to damage
			if (ws.hostile) {
				coll.gameObject.GetComponent<HealthScript> ().damage ();
			}
		}
	}
	void OnCollisionExit2D(Collision2D coll) {

	}
}
