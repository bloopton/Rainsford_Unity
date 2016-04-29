using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public int health;
	// Use this for initialization
	void Start () {
	
	}

	public void damage()
	{
		health--;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
