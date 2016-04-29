using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GateScript : MonoBehaviour {

	public GameObject player;
	private Animator animator;
	public GameObject otherGate;
	Rigidbody2D playerRB;
	GameObject[] parallaxObjs;
	public int nextLevel;


	// Use this for initialization
	void Start () {
		if(parallaxObjs == null)
			parallaxObjs = GameObject.FindGameObjectsWithTag("Parallax");

		playerRB = player.GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			animator.SetBool ("Exiting", false);
			animator.SetBool ("Entering", true);
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			animator.SetBool ("Hovering", true);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			animator.SetBool ("Exiting", true);
			animator.SetBool ("Entering", false);
			animator.SetBool ("Hovering", false);
		}

	}


	void switchScene(){
		SceneManager.LoadScene (nextLevel);
	}

	void teleport(){
		//float gateDistance = otherGate.transform.position.x - transform.position.x;
		float pInitX = playerRB.transform.position.x;
		playerRB.transform.position = new Vector3(otherGate.transform.position.x, playerRB.transform.position.y, playerRB.transform.position.z);
		float pFinX = playerRB.transform.position.x;
		float gateDistance = pFinX - pInitX;

		foreach (GameObject pObj in parallaxObjs) {
			MovementBind mBindScript = pObj.GetComponent<MovementBind>();
			//translate them right by a distance of original position + parallax*distance player has travelled b/w gates
			pObj.transform.position = new Vector3(pObj.transform.position.x + mBindScript.parallax * gateDistance, pObj.transform.position.y, pObj.transform.position.z);
		}
	}
}
