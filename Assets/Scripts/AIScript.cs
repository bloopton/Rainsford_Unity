using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour {

	MovementScript ms;
	private float timer;
	private float duration;
	Rigidbody2D thisRB;
	bool isCrawlingRight;

	// Use this for initialization
	void Start () {
		isCrawlingRight = true;
		thisRB = gameObject.GetComponent<Rigidbody2D> ();
		timer = 0f;
		duration = 3f;
		ms = GetComponent<MovementScript> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		patrol ();
	}

	void patrol ()
	{
		if (isCrawlingRight) {
			ms.crawlRight ();
		} else
			ms.crawlLeft ();
		timer += Time.deltaTime;

		if (timer >= duration) {
			if (thisRB.velocity.x <= 0)
				isCrawlingRight = true;
			else
				isCrawlingRight = false;
			timer = 0f;
		}
		Debug.Log ("X Velocity " + thisRB.velocity.x);
	}

	IEnumerator delay(){
		yield return new WaitForSeconds (4);
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
