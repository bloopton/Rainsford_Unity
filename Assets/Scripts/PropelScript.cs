using UnityEngine;
using System.Collections;

public class PropelScript : MonoBehaviour {

	public float xForce;
	public float yForce;
	public GameObject player;
	float direction;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		//playerRB = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
		if (player.transform.rotation == Quaternion.Euler (0, 0, 0)) {
			//right
			direction = 1;
		} else {
			//left
			direction = -1;
		}
		gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (direction*xForce, yForce), ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		
	}}
