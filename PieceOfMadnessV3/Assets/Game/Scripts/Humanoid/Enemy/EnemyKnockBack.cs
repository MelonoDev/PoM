using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour {
	public float speed = 15.0f;
	public float duration = 0.25f;

	private float startTime = 0f;
	private CharacterController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	public void Knockback (Vector3 direction) {
		startTime = Time.deltaTime;
		while (Time.time < (startTime + duration)){
			controller.SimpleMove(direction*speed);
			return;
		}
	}
}

