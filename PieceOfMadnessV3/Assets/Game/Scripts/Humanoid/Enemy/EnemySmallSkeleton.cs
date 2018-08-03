using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySmallSkeleton : Enemy {

	void Start (){
		Speed = 2.5f;
		MaxHealth = 8;
		Health = MaxHealth;
		Weapon = new ShortSword();
		StartTheEnemy ();
	}

	protected override void MoveState () {
		Move ();
		HumanoidAnimator.SetBool ("WalkBool", true);
		HumanoidAnimator.SetBool ("IdleBool", false);			

		if (Vector3.Distance(gameObject.transform.position, player.transform.position) < attackDistanceToPlayer){
			currentState = State.Idle;
		}
	}

	protected override void IdleState(){
		HumanoidAnimator.SetBool ("WalkBool", false);
		HumanoidAnimator.SetBool ("IdleBool", true);
		if (Vector3.Distance (gameObject.transform.position, player.transform.position) < attackDistanceToPlayer) {
			currentState = State.StandardAttacking;
		} else {
			currentState = State.Move;
		}
	}
}
