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

	void OnTriggerEnter2D(Collider2D coll) {
		if (gameObject.tag == "Projectile") {
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.x < .02f && gameObject.GetComponent<Rigidbody2D> ().velocity.y != .02f) {
				if (damagePlayer) {
					if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy") {
						coll.gameObject.GetComponent<HealthScript> ().damage ();
					}
				} else if (coll.gameObject.tag == "Enemy") {
					coll.gameObject.GetComponent<HealthScript> ().damage ();
				}
			}
		} else {
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

	void OnTriggerStay2D(Collider2D coll) {
		if (gameObject.tag == "Projectile") {
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.x < .02f && gameObject.GetComponent<Rigidbody2D> ().velocity.y != .02f) {
				if (damagePlayer) {
					if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy") {
						coll.gameObject.GetComponent<HealthScript> ().damage ();
					}
				} else if (coll.gameObject.tag == "Enemy") {
					coll.gameObject.GetComponent<HealthScript> ().damage ();
				}
			}
		} else {
			if (damagePlayer) {
				if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy") {
					coll.gameObject.GetComponent<HealthScript> ().damage ();
				}
			} else if (coll.gameObject.tag == "Enemy") {
				coll.gameObject.GetComponent<HealthScript> ().damage ();
			}
		}
	}
}
