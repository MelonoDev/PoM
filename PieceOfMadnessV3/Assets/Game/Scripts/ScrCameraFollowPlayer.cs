﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrCameraFollowPlayer : MonoBehaviour {

	public Transform PlayerObject;
	private float CameraHeight = 20f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float POx = PlayerObject.transform.position.x;
		float POz = PlayerObject.transform.position.z;

		gameObject.transform.position = new Vector3(POx, CameraHeight, POz-7);
	}
}
