using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidDying : MonoBehaviour {

	public HumanoidClass Stats;
	public Animator animator;
	public PlayerInitialiser initialiser;

	void Start(){
//		Stats = initialiser.humanoidClass;
	}

	void Update () {
		DoYouDie ();
	}

	public void DoYouDie() { // put in update
		if (Stats.Health <= 0) {
			animator.SetBool ("Dying", true);
			Stats.Dead = true;
		}
	}
}
