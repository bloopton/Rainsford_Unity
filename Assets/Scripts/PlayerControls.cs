using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public KeyCode jump, moveRight, moveLeft,crouch, useGate;
	private Animator animator;
	MovementScript ms;
	//public float speed;
	//public float jumpHeight;
	bool canEnter;//prevent gate spam
	bool isTeleporting;
	//public float maxSpeed;
	//public float maxCrawlSpeed;


	Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
		ms = gameObject.GetComponent<MovementScript>();
		isTeleporting = false;
		canEnter = true;
		animator = this.GetComponent<Animator> ();
		//speed = 15.0f;
		//jumpHeight = 30.0f;
		playerRB = gameObject.GetComponent<Rigidbody2D>();
		jump = KeyCode.W;
		crouch = KeyCode.S;
		moveRight =  KeyCode.D;
		moveLeft = KeyCode.A;
		useGate = KeyCode.E;
/*
		if (PlayerPrefs.GetInt ("Health", 100) <= 0) {
			PlayerPrefs.SetInt ("Health", PlayerPrefs.GetInt ("MaxHealth", 100));
		}
		PlayerPrefs.GetInt ("CurrentLevel", 0);
		PlayerPrefs.SetInt ("CurrentLevel", SceneManager.GetActiveScene ().buildIndex);
*/

		//playerRB.transform.position = new Vector2(PlayerPrefs.GetFloat ("CurrentPosX", -6.18f), PlayerPrefs.GetFloat ("CurrentPosY", -4.41f));

	
	}


	void Update(){
		if (playerRB.velocity.y > 0) {
			animator.SetBool ("Ground", false);
			animator.SetBool ("Jumping", true);
		} else if (playerRB.velocity.y < 0 ) {
			animator.SetBool ("Falling", true);
			animator.SetBool ("Jumping", false);
		} else{ 
			animator.SetBool ("Falling", false);
			animator.SetBool ("Jumping", false);
			animator.SetBool ("Ground", true);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {//changed from Update() due to physics

		//var horizontal = Input.GetAxis("Horizontal");
		if (!isTeleporting) {

			if (Input.GetKey (crouch)) {
				animator.SetBool ("Crouched", true);
				if (Input.GetKey (moveRight)) {
					animator.SetInteger ("Direction", 0);
					if (playerRB.velocity.y == 0)
						animator.SetBool ("Running", true);
					ms.crawlRight ();
					//MS://Vector2 vel = playerRB.velocity;
					//MS://if (Mathf.Abs (playerRB.velocity.x) < maxCrawlSpeed)
						//MS://playerRB.AddForce (new Vector2 (speed / 2, 0), ForceMode2D.Impulse);//new physics

				} else if (Input.GetKey (moveLeft)) {
					animator.SetInteger ("Direction", 1);
					if (playerRB.velocity.y == 0)
						animator.SetBool ("Running", true);
					ms.crawlLeft ();
					/* MS://
					Vector2 vel = playerRB.velocity;
					if (Mathf.Abs (playerRB.velocity.x) < maxCrawlSpeed)
						playerRB.AddForce (new Vector2 (-speed / 2, 0), ForceMode2D.Impulse);//new physics
						*/

				} else
					animator.SetBool ("Running", false);
			} else {
				animator.SetBool ("Crouched", false);
			
				if (Input.GetKey (moveRight)) {
					animator.SetInteger ("Direction", 0);
					if (playerRB.velocity.y == 0)
						animator.SetBool ("Running", true);
					ms.moveRight ();
					/*
					Vector2 vel = playerRB.velocity;
					if (Mathf.Abs (playerRB.velocity.x) < maxSpeed)
						playerRB.AddForce (new Vector2 (speed, 0), ForceMode2D.Impulse);//new physics
						*/
				} else if (Input.GetKey (moveLeft)) {
					animator.SetInteger ("Direction", 1);
					if (playerRB.velocity.y == 0)
						animator.SetBool ("Running", true);
					ms.moveLeft ();
					/*
					Vector2 vel = playerRB.velocity;
					if (Mathf.Abs (playerRB.velocity.x) < maxSpeed)
						playerRB.AddForce (new Vector2 (-speed, 0), ForceMode2D.Impulse);//new physics
						*/

				} else
					animator.SetBool ("Running", false);

				if (Input.GetKeyDown (jump)) {
					if (playerRB.velocity.y == 0) {
						ms.jump();
						/*
						Vector2 vel = playerRB.velocity;
						float velX = vel.x;
						playerRB.velocity = new Vector2 (velX, jumpHeight);
						*/
					}
				}
			}
		}
		//PlayerPrefs.SetFloat ("CurrentPosX", transform.position.x);//Player prefs
		Debug.Log ("X Velocity " + playerRB.velocity.x);
	}

	Collider2D gateCol;
	/*
	 * In game gates
	 */
	public void playerTeleport()
	{
		gateCol.gameObject.SendMessage ("teleport", null, SendMessageOptions.RequireReceiver);

	}
	/*
	 * In-between scene portals
	 */
	public void playerUsePortal()
	{
		gateCol.gameObject.SendMessage ("switchScene", null, SendMessageOptions.RequireReceiver);

	}


	/*
	Called as event in EnterBush animatino
	*/
	public void afterEnterBush()
	{
		canEnter = false;
		animator.SetBool ("EnteringBush", false);
		animator.SetBool ("ExitingBush", true);
	}
	/*
	Called as event in ExitBush animation
	*/
	public void afterExitBush()
	{
		animator.SetBool ("ExitingBush", false);
		canEnter = true;
		isTeleporting = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Message" || other.gameObject.tag == "Text") {
			other.gameObject.GetComponent<AlphaFade> ().StartCoroutine ("FadeIn");
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Gate") {
			//Debug.Log ("Ready to go!" + Time.frameCount);
			if(Input.GetKeyDown(useGate)){
				if(canEnter){
					isTeleporting = true;
					playerRB.transform.position = new Vector2(other.transform.position.x, playerRB.transform.position.y);
					gateCol = other;
					//set to bush enter
					animator.SetBool ("EnteringBush", true);
					//animatino triggers teleport funciton
					//animation triggers EnteringBush = false, ExitingBush = true
				}

			}
		}

		if (other.gameObject.tag == "Portal") {
			if(Input.GetKeyDown(useGate)){
				if(canEnter){
					isTeleporting = true;
					playerRB.transform.position = new Vector2(other.transform.position.x, playerRB.transform.position.y);
					gateCol = other;
					animator.SetBool ("EnteringPortal", true);
				}

			}
		}

	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Message" || other.gameObject.tag == "Text") {
			other.gameObject.GetComponent<AlphaFade> ().StartCoroutine ("FadeOut");
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Obstacle") {
			float velY = playerRB.velocity.y;
			playerRB.velocity = new Vector2 (0, velY);
			Debug.Log ("Collision enter");
		}
	}
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Obstacle") {
			float velY = playerRB.velocity.y;
			playerRB.velocity = new Vector2 (0, velY);
			Debug.Log ("Collision stay");
		}
	}
	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Obstacle") {
			Debug.Log ("Collision exit");
		}

	}
}
