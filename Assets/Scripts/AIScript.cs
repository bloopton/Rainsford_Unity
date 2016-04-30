using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour {

	MovementScript ms;
	private float timer;
	private float hostileTimer;
	private Animator animator;
	private float duration;
	Rigidbody2D thisRB;
	bool isCrawlingRight;
	bool hostile;
	bool startHostileTimer;


	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		hostile = false;
		startHostileTimer = false;
		isCrawlingRight = true;
		thisRB = gameObject.GetComponent<Rigidbody2D> ();
		timer = 0f;
		hostileTimer = 0f;

		duration = 2f;
		ms = GetComponent<MovementScript> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!hostile)
			patrol ();
		else {
			if (isRight()) {
				ms.moveRight ();
			} else {
				ms.moveLeft ();
			}
		}
		if (startHostileTimer) {
			hostileTimer += Time.deltaTime;
			if (hostileTimer >= 2f) {
				animator.SetBool ("hostile", false);
				hostile = false;
				//transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				hostileTimer = 0f;
			}
		}
		if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D> ().velocity.x) > 0) {
			animator.SetBool ("moving", true);
		}
		else animator.SetBool ("hostile", false);
		
	}

	bool isRight()//possibly move to movement script
	{
		if (transform.rotation.y == 0)
			return true;//right
		else //(transform.rotation.y == 180)
			return false;//left driection
	}

	void patrol ()
	{
		if (isCrawlingRight) {
			ms.crawlRight ();
		} else {
			ms.crawlLeft ();
		}
		timer += Time.deltaTime;

		if (timer >= duration) {
			if (thisRB.velocity.x <= 0) {
				isCrawlingRight = true;
				transform.rotation = Quaternion.Euler(0,0,0);
			}
			else {
				isCrawlingRight = false;
				transform.rotation = Quaternion.Euler(0,180,0);
			}
			//transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
			if (hostile) {
				coll.gameObject.GetComponent<HealthScript> ().damage ();
			}
		}
	}
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy"){
		//if currently hostile, continue to damage
			if (hostile) {
				coll.gameObject.GetComponent<HealthScript> ().damage ();
			}
		}
	}
	void OnCollisionExit2D(Collision2D coll) {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			startHostileTimer = false;
			hostileTimer = 0;
			hostile = true;
			animator.SetBool ("hostile", true);
			//become hostile, chase
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
			startHostileTimer = true;
			//turn off hostile BUT continue chasing for fixedTime.
			//return to patrol position/pattern
		}
	}

}
