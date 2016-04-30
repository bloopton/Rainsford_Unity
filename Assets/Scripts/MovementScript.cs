using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public float speed;
	public float jumpHeight;
	public float maxSpeed;
	public float maxCrawlSpeed;
	Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
		speed = 15.0f;
		jumpHeight = 30.0f;
		playerRB = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	public void crawlRight(){
		Vector2 vel = playerRB.velocity;
		if (Mathf.Abs (playerRB.velocity.x) < maxCrawlSpeed)
			playerRB.AddForce (new Vector2 (speed / 2, 0), ForceMode2D.Impulse);//new physics
		//transform.rotation = Quaternion.Euler(0,0,0);

	}

	public void crawlLeft(){
		Vector2 vel = playerRB.velocity;
		if (Mathf.Abs (playerRB.velocity.x) < maxCrawlSpeed)
			playerRB.AddForce (new Vector2 (-speed / 2, 0), ForceMode2D.Impulse);//new physics
		//transform.rotation = Quaternion.Euler(0,180,0);

	}

	public void moveRight(){
		Vector2 vel = playerRB.velocity;
		if (Mathf.Abs (playerRB.velocity.x) < maxSpeed)
			playerRB.AddForce (new Vector2 (speed / 2, 0), ForceMode2D.Impulse);//new physics
		//transform.rotation = Quaternion.Euler(0,0,0);

	}

	public void moveLeft(){
		Vector2 vel = playerRB.velocity;
		if (Mathf.Abs (playerRB.velocity.x) < maxSpeed)
			playerRB.AddForce (new Vector2 (-speed / 2, 0), ForceMode2D.Impulse);//new physics
		//transform.rotation = Quaternion.Euler(0,180,0);
	
	}

	public void jump(){
		Vector2 vel = playerRB.velocity;
		float velX = vel.x;
		playerRB.velocity = new Vector2 (velX, jumpHeight);
	}
}
