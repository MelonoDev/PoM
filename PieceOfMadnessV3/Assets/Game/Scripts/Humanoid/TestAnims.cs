using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnims : MonoBehaviour {

	public Animator ThisAnimator;

	// Use this for initialization
	void Start () {
		ThisAnimator.SetBool ("WalkBool", true);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
