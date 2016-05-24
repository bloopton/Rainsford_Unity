using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	public GameObject rock;
	BoxCollider2D[] bColliders;

	int rockCount;
	public float duration;
	float timer;

	Animator animator;
	Rigidbody2D playerRB;

	public GameObject player;
	float direction;


	// Use this for initialization
	void Start () {


		player = GameObject.FindGameObjectWithTag ("Player");
		bColliders = player.GetComponents<BoxCollider2D> ();

		animator = gameObject.GetComponent<Animator> ();
		playerRB = gameObject.GetComponent<Rigidbody2D> ();
		rockCount = 0;
		timer = 0f;

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(player.transform.rotation.eulerAngles);


		if (Input.GetKeyDown (KeyCode.Space)) {
			if (playerRB.velocity.x == 0 && playerRB.velocity.y == 0) {
				if (rockCount < 1) {
					//play animation with throwRock() event
					animator.SetBool ("ThrowingRock", true);
				}
			}
		}

		if(rockCount > 0) 
			timer += Time.deltaTime;

		if (timer >= duration) {
			rockCount--;
			timer = 0;
		}
		//Debug.Log ("Timer " + timer);
	}

	public void throwRock(){
		if (player.transform.rotation == Quaternion.Euler (0, 0, 0)) {
			//right
			Instantiate (rock, new Vector3 (gameObject.transform.position.x + 2*bColliders[0].size.x, gameObject.transform.position.y, 0), Quaternion.identity);
			Debug.Log("Right");

		} else {
			//left
			Debug.Log("Left");
			Instantiate (rock, new Vector3 (gameObject.transform.position.x - 2*bColliders[0].size.x, gameObject.transform.position.y, 0), Quaternion.identity);
		}
		rockCount++;
		animator.SetBool("ThrowingRock", false);
	}
}
