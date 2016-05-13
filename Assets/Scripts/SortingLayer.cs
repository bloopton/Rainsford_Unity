using UnityEngine;
using System.Collections;

public class SortingLayer : MonoBehaviour {
	public string LAYER_NAME = "Default";
	public int sortingOrder = 0;
	private MeshRenderer rend;

	void Start()
	{
		rend = GetComponent<MeshRenderer>();

		if (rend)
		{
			rend.sortingOrder = sortingOrder;
			rend.sortingLayerName = LAYER_NAME;
		}
	}

}
