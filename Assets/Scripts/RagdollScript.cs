using UnityEngine;
using System.Collections;

public class RagdollScript : MonoBehaviour {

	public GameObject ragDoll;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void moveTo(Vector2 bodyPosition){
		ragDoll.transform.position = new Vector2(bodyPosition.x, bodyPosition.y);
	}
}
