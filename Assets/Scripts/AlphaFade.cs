using UnityEngine;
using System.Collections;

public class AlphaFade : MonoBehaviour {

	public float fadeSpeed = 1f;
	public float fadeTime = 1f;
	private SpriteRenderer sprite;
	private MeshRenderer mr;


	void Start(){
		if (gameObject.tag == "Message") {
			sprite = GetComponent<SpriteRenderer>();
			Color c = sprite.color;
			c.a = 0;
			sprite.color = c;
		} else if (gameObject.tag == "Text") {
			mr = GetComponent<MeshRenderer> ();
			Color c = mr.material.color;
			c.a = 0;
			mr.material.color = c;
		}
	}
	// Update is called once per frame
	void Update () {
	}

	IEnumerator FadeIn() {
		if (gameObject.tag == "Message") {
			for (float f = 0; f <= 1f; f += 0.1f) {
				Color c = sprite.color;
				c.a = f;
				sprite.color = c;
				yield return null;
			}
		} else if (gameObject.tag == "Text") {
			for (float f = 0; f <= 1f; f += 0.1f) {
				Color c = mr.material.color;
				c.a = f;
				mr.material.color = c;
				yield return null;
			}
		}
	}

	IEnumerator FadeOut() {
		if (gameObject.tag == "Message") {
			for (float f = 1f; f >= -1; f -= 0.1f) {
				Color c = sprite.color;
				c.a = f;
				sprite.color = c;
				//Debug.Log ("Alpha: " + f);
				yield return null;
			}
		}
		else if (gameObject.tag == "Text") {
			for (float f = 1f; f >= -1; f -= 0.1f) {
				Color c = mr.material.color;
				c.a = f;
				mr.material.color = c;
				yield return null;
			}		
		}
	}
}