using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	RagdollScript ragdoll;
	public int health;
	Renderer rend;
	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<Renderer> ();
		if (gameObject.tag == "Player")
			ragdoll = GetComponent<RagdollScript> ();
	}

	public void damage()
	{
		health--;
	}

	public void die()
	{
		if (gameObject.tag == "Player") {
			rend.material.color = new Color (1, 1, 1, 0);
			ragdoll.moveTo (gameObject.transform.position);
		}
		Destroy (gameObject);

	}

	// Update is called once per frame
	void Update () {
		if (health < 0)
			die ();
		//Debug.Log (gameObject.tag + "Health" + health);
	}
}
