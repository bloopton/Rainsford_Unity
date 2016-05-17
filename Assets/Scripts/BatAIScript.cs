using UnityEngine;
using System.Collections;

public class BatAIScript : MonoBehaviour {

	public float range;
	MovementScript ms;
	private Animator animator;
	GameObject player;
	//Rigidbody2D playerRB;
	Transform playerT;
	bool hostile;
	bool attacking;


	// Use this for initialization
	void Start () {
		hostile = false;
		attacking = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerT = player.transform;
		//playerRB = player.GetComponent<Rigidbody2D> ();
		ms = GetComponent<MovementScript> ();
		animator = this.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//ms.moveTowardsXY (player.transform.position);
		//if player enters x-range
		if (Mathf.Abs (playerT.position.x - transform.position.x) < range) {
			animator.SetBool ("awake", true);
		}
		if (hostile) {
			track (playerT);
			//Debug.Log ("Bat tracking player");
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			Debug.Log ("Trigger enter");
			//animator.SetBool ("openMouth", true);
			//become hostile, chase
			attacking = true;
			animator.SetBool ("attacking", true);
			//ms.moveTowardsXY(other.transform.position);//swoop down
		}
	}


	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			Debug.Log ("Trigger exit");
			animator.SetBool ("attacking", false);
			attacking = false;
			//animator.SetBool ("openMouth", false);
			//return to intial y position
			//ms.moveTowardsY(other.transform.position);
		}
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

	public void makeHostile(){
		hostile = true;
		animator.SetBool ("hostile", true);
	}
	public void makeAttack(){
		animator.SetBool ("attacking", true);
	}


	public void track(Transform t){
		if (attacking == true) {
		//	ms.moveTowardsX (t.position);
			ms.moveTowardsX (new Vector2(t.position.x, t.position.y + t.GetComponent<BoxCollider2D>().size.y));

			//if (playerT.position.x < transform.position.x)
			//	gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, gameObject.GetComponent<Rigidbody2D>().velocity.y);
			//else if (playerT.position.x > transform.position.x)
			//	gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, gameObject.GetComponent<Rigidbody2D>().velocity.y);
		} else {
			//ms.moveTowardsXY(t.position);//swoop down
			ms.moveTowardsXY (new Vector2(t.position.x, t.position.y + t.GetComponent<BoxCollider2D>().size.y));

		}
	}


}
