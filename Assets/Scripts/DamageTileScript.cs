using UnityEngine;
using System.Collections;

public class DamageTileScript : MonoBehaviour {

	public bool damagePlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (damagePlayer) {
			if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy") {
				coll.gameObject.GetComponent<HealthScript> ().damage ();
			}
		}
		else if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.GetComponent<HealthScript> ().damage ();
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (damagePlayer) {
			if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy") {
				coll.gameObject.GetComponent<HealthScript> ().damage ();
			}
		}
		else if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.GetComponent<HealthScript> ().damage ();
		}
	}
}
