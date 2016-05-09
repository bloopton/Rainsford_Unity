using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public KeyCode jump, moveRight, moveLeft, crouch, useGate;

	BoxCollider2D[] bColliders;
	BoxCollider2D bCollider;

	Vector2[] originalCsizes;

	private Animator animator;
	MovementScript ms;
	bool canEnter;//prevent gate spam
	bool isTeleporting;


	Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
		bColliders = GetComponents<BoxCollider2D> ();
		originalCsizes = new Vector2[bColliders.Length];
		//bCollider = bColliders [1];
		for(int i = 0; i < bColliders.Length; i++){
			originalCsizes[i] = bColliders[i].size;
		}

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
				for (int i = 0; i < bColliders.Length; i++) {
					BoxCollider2D bCollider = bColliders [i];
					if (bCollider.size == originalCsizes[i]) {
						bCollider.size = new Vector2 (bCollider.size.x, bCollider.size.y / 2);
						bCollider.offset = new Vector2 (0f, -bCollider.size.y / 2);
					}
				}
				animator.SetBool ("Crouched", true);
				if (Input.GetKey (moveRight)) {
					animator.SetInteger ("Direction", 0);
					if (playerRB.velocity.y == 0)
						animator.SetBool ("Running", true);
					ms.crawlRight ();

				} else if (Input.GetKey (moveLeft)) {
					animator.SetInteger ("Direction", 1);
					if (playerRB.velocity.y == 0)
						animator.SetBool ("Running", true);
					ms.crawlLeft ();				
				} else
					animator.SetBool ("Running", false);
			} else {
				for (int i = 0; i < bColliders.Length; i++) {
					bCollider = bColliders [i];
					if (bCollider.size != originalCsizes[i]) {
						bCollider.size = originalCsizes[i];
						bCollider.offset = new Vector2 (0f, -bCollider.size.y / 16);
					}
				}

				animator.SetBool ("Crouched", false);
			
				if (Input.GetKey (moveRight)) {
					animator.SetInteger ("Direction", 0);
					if (playerRB.velocity.y == 0)
						animator.SetBool ("Running", true);
					ms.moveRight ();
				} else if (Input.GetKey (moveLeft)) {
					animator.SetInteger ("Direction", 1);
					if (playerRB.velocity.y == 0)
						animator.SetBool ("Running", true);
					ms.moveLeft ();
				} else
					animator.SetBool ("Running", false);

				if (Input.GetKeyDown (jump)) {
					if (playerRB.velocity.y == 0) {
						ms.jump();
					}
				}
			}
		}
		//PlayerPrefs.SetFloat ("CurrentPosX", transform.position.x);//Player prefs
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
