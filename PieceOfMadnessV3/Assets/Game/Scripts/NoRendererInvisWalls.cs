using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRendererInvisWalls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (Transform invisWall in transform) {
			invisWall.gameObject.GetComponent<Renderer> ().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
