using UnityEngine;
using System.Collections;

public class MovementBind : MonoBehaviour {

	public GameObject targetObj;
	GameObject[] cols;

	public bool isCamera;
	bool bumped;
	Transform target;
	Rigidbody2D targetRB;//player rigidbody
	Rigidbody2D thisRB;//background/camera rigidbody
	public Transform startPos;
	public Transform endPos;
	public float parallax;
	float trackDistance;
	// Use this for initialization
	void Start () {
		if(cols == null)
			cols = GameObject.FindGameObjectsWithTag("Obstacle");

		target = targetObj.transform;
		bumped = false;
		thisRB = GetComponent<Rigidbody2D> ();
		targetRB = targetObj.GetComponent<Rigidbody2D>();
		trackDistance = Mathf.Abs(Camera.main.transform.position.x - startPos.position.x);
		//distance between camera and start position intially
	}
	
	// Update is called once per frame
	void Update () {
		if (targetRB == null) {
			targetRB = GameObject.Find ("Body").GetComponent<Rigidbody2D> ();
		}
		foreach (GameObject col in cols) {
			if (targetRB.IsTouching (col.GetComponent<Collider2D> ())) {
				bumped = true;
				//Debug.Log ("Bumped");
			}
		}

		if (targetObj == null) {
			targetObj = GameObject.Find ("Body");
			target = targetObj.transform;
		}
		if (Mathf.Abs (target.position.x - endPos.position.x) <= trackDistance || target.position.x < 0) {//in start or end condition
			if (!isCamera) {
				thisRB.velocity = new Vector2 (0, 0);
			}
		}
		//If not in end condition, possibly track 
		if (Mathf.Abs (target.position.x - endPos.position.x) > trackDistance) {
			//if start condition, wait until player is at position x = 0 to track
			if (target.position.x >= 0) {
				if (isCamera) {
					transform.position = new Vector3 (target.position.x, transform.position.y, transform.position.z);
				} else if (!isCamera) {
					if (bumped == false) {
						thisRB.velocity = new Vector2 (parallax * targetRB.velocity.x, 0);
					} else {
						thisRB.velocity = new Vector2 (0, 0);
					}
				}
			}
		}

		bumped = false;
	}
}
