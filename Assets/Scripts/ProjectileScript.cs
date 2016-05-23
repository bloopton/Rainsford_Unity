using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	public GameObject rock;
	public GameObject instantiatedRock;

	int rockCount;
	public float duration;
	float timer;
	// Use this for initialization
	void Start () {
		duration = 3f;
		rockCount = 0;
		timer = 0f;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			while (rockCount < 1) {
				timer = 0f;
				instantiatedRock = GameObject.Instantiate (rock, new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity) as GameObject;
				rockCount++;
			}
		}

		if(rockCount > 0) 
			timer += Time.deltaTime;

		if (timer >= duration) {
			Debug.Log ("DESTROY ");
			Destroy (instantiatedRock);
			rockCount--;
		}

		Debug.Log ("Timer " + timer);
	}
}
