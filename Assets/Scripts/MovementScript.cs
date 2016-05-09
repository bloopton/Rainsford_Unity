using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public float jumpPower;
	public float runForce;
	public float runVel;
	public float crawlForce;
	public float crawlVel;
	Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
		playerRB = gameObject.GetComponent<Rigidbody2D>();
		jumpPower = 80.0f;
		runForce = 300.0f;
		runVel = 10;
		crawlForce = 200;
		crawlVel = 5;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
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

	public void jump(){
		playerRB.AddForce (new Vector2 (0, jumpPower), ForceMode2D.Impulse);
	}
}
