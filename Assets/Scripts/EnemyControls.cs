using UnityEngine;
using System.Collections;

public class EnemyControls : MonoBehaviour {

	public Transform player;
	Rigidbody2D enemyRB;

	public float speed;
	public float vision;
	private bool isAware;
	// Use this for initialization
	void Start () {
		speed = 5.0f;
		vision = 4.0f;
		isAware = false;
		enemyRB = GetComponent<Rigidbody2D> ();
	}

	IEnumerator checkPlayerProximity(){
		//replacement for vision-cone/rectangle
		if (Mathf.Abs (player.position.x - transform.position.x) < vision) {
			isAware = true;
		} else
			isAware = false;
		yield return null;
			
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine(checkPlayerProximity());
		if (isAware == true) {
			if (player.position.x <= transform.position.x) {
				//player to left
				enemyRB.velocity = new Vector2 (-speed, 0);
			} else {
				enemyRB.velocity = new Vector2 (speed, 0);
			}
		}
	}
}
