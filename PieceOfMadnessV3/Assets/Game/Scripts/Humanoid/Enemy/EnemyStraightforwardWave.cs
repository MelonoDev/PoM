using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStraightforwardWave : Enemy {

	private float standStillDistanceToPlayerMin = 7f;
	private float standStillDistanceToPlayerMax = 16f;
	private float standStillChance = 0f;
	private float standStillTimer = 0f;
	private float standStillDuration = 1f;

	protected override void MoveState () {
		Move ();
		HumanoidAnimator.SetBool ("WalkBool", true);
		HumanoidAnimator.SetBool ("IdleBool", false);			
		if ((Vector3.Distance (gameObject.transform.position, player.transform.position) > standStillDistanceToPlayerMin) && (Vector3.Distance (gameObject.transform.position, player.transform.position) < standStillDistanceToPlayerMax)){ 
			standStillTimer += Time.deltaTime;
			if (standStillTimer >= standStillDuration) {
				standStillTimer = 0f;
				standStillChance = Random.Range (0f, 1f);
				if (standStillChance > .05f) {
					currentState = State.Idle;

				}
			}
		}
		if (Vector3.Distance(gameObject.transform.position, player.transform.position) < attackDistanceToPlayer){
			currentState = State.Idle;
		}
	}

	protected override void IdleState(){
		HumanoidAnimator.SetBool ("WalkBool", false);
		HumanoidAnimator.SetBool ("IdleBool", true);
		agent.SetDestination (gameObject.transform.position);
		if ((Vector3.Distance (gameObject.transform.position, player.transform.position) > standStillDistanceToPlayerMin) && (Vector3.Distance (gameObject.transform.position, player.transform.position) < standStillDistanceToPlayerMax) && (standStillChance>.1f)){ 
			standStillTimer += Time.deltaTime;
			if (standStillTimer >= standStillDuration) {
				standStillTimer = 0f;
				standStillChance = Random.Range (0f, 1f);

			}
		} else if (Vector3.Distance (gameObject.transform.position, player.transform.position) < attackDistanceToPlayer) {
			currentState = State.StandardAttacking;
		} else {
			currentState = State.Move;
		}
	}

	//StandardAttack
	protected override void StandardAttack(bool AttackTrigger){
		if (AttackTrigger) {
			if (!isStandardAttacking) {
				HumanoidAnimator.SetBool ("StandardAttackBool", AttackTrigger);
				standardAttackWindupTimer += Time.deltaTime;

			}

			if (standardAttackWindupTimer >= standardAttackWindupDuration) {
				agent.angularSpeed = 0;

				isStandardAttacking = true;
				standardAttackWindupTimer = 0f;
			}
		}
		if (isStandardAttacking) {
			if (!SwordStrikeAudio.isPlaying && isStandardAttackingTimer <= 0){
				SwordStrikeAudio.Play ();
			}
			currentState = State.WindDown;
			Hitbox.SetActive (true);
			isStandardAttackingTimer += Time.deltaTime;
			if (isStandardAttackingTimer > standardAttackDuration) {
				HumanoidAnimator.SetBool ("StandardAttackBool", false);
				isStandardAttacking = false;
				isStandardAttackingTimer = 0f;
			}
		} // put other else ifs for other attacks here
		else {
			Hitbox.SetActive (false);
		}
	}
}
