﻿using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {

	public float duration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Destroy (gameObject, duration);
	}
}
