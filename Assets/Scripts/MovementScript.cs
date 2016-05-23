using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public Vector2 initPos;
	public float jumpPower;
	public float runForce;
	public float runVel;
	public float crawlForce;
	public float crawlVel;
	Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
		
		playerRB = gameObject.GetComponent<Rigidbody2D>();
		initPos = playerRB.position;
		//jumpPower = 100.0f;
		//runForce = 300.0f;
		//runVel = 10.0f;
		//crawlForce = 300.0f;
		//crawlVel = 3.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0.02f) {
			transform.rotation = Quaternion.Euler(0,0,0);//face right
			//Debug.Log ("Switch to Right");

		} else if (gameObject.GetComponent<Rigidbody2D>().velocity.x < -0.02f) {
			transform.rotation = Quaternion.Euler(0,180,0);//face left
			//Debug.Log ("Switch to Left");

		}
	}

	public void crawlRight(){
		if (Mathf.Abs (playerRB.velocity.x) < crawlVel)
			playerRB.AddForce (new Vector2 (crawlForce, 0), ForceMode2D.Force);
	}

	public void crawlLeft(){
		if (Mathf.Abs (playerRB.velocity.x) < crawlVel)
			playerRB.AddForce (new Vector2 (-crawlForce, 0), ForceMode2D.Force);

	}

	public void moveRight(){
		if (Mathf.Abs (playerRB.velocity.x) < runVel)
			playerRB.AddForce (new Vector2 (runForce, 0), ForceMode2D.Force);
	}

	public void moveLeft(){
		if (Mathf.Abs (playerRB.velocity.x) < runVel)
			playerRB.AddForce (new Vector2 (-runForce, 0), ForceMode2D.Force);
	}

	public void moveRightVel(){
		Debug.Log ("Velocity");
		playerRB.velocity = new Vector2(runVel, playerRB.velocity.y);
	}

	public void moveLeftVel(){
		Debug.Log ("Velocity");

		playerRB.velocity = new Vector2(-runVel, playerRB.velocity.y);
	}


	public void moveUpVel(){
		playerRB.velocity = new Vector2(playerRB.velocity.x, runVel);
	}

	public void moveDownVel(){
		playerRB.velocity = new Vector2(playerRB.velocity.x, -runVel);
	}

	public void jump(){
		playerRB.AddForce (new Vector2 (0, jumpPower), ForceMode2D.Impulse);
	}

	//b-line, separate from physics
	public void moveTowardsXY(Vector2 pos){
		Debug.Log ("move towards xy");

		moveTowardsX (pos);
		moveTowardsY (pos);
		//playerRB.AddForce (new Vector2 (pos.x - transform.position.x, pos.y - transform.position.y), ForceMode2D.Impulse);
	}

	//just horizontal tracking
	public void moveTowardsX(Vector2 pos){
		Debug.Log ("move towards x");

		if (pos.x < transform.position.x)
			moveLeftVel();
		else if (pos.x > transform.position.x)
			moveRightVel();
		//playerRB.AddForce (new Vector2 (pos.x - transform.position.x, 0), ForceMode2D.Impulse);
	}

	//just horizontal tracking
	public void moveTowardsY(Vector2 pos){
		Debug.Log ("move towards y");
		if (pos.y < transform.position.y)
			moveDownVel();
		else if (pos.y > transform.position.y)
			moveUpVel();
		//playerRB.AddForce (new Vector2 (0, pos.x - transform.position.y), ForceMode2D.Impulse);
	}

	public void returnToInit(bool returnX, bool returnY){
		if (returnX) {
			if (returnY) {
				moveTowardsXY (initPos);
			}
			moveTowardsX (initPos);
		}
		//add stuff later if need to include y-only movement
	}
}
