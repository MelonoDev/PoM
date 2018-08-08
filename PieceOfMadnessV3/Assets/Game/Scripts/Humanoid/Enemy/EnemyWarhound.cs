using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarhound : Enemy {

	private float noAttackingTimer = 0f;
	private float noAttackingDuration = 2f;
	private bool noAttackingBool = false;

	void Start (){
		Speed = 10f;
		MaxHealth = 4;
		Health = MaxHealth;
		Weapon = new Bite();
		StartTheEnemy ();
		standardAttackWindupDuration = .1f;
		standardAttackDuration = .2f;
		TurnRate = 9001f;
		WindDownDuration = .3f;
	}

	void Update(){
		if (noAttackingBool) {
			noAttackingTimer += Time.deltaTime;
		}
		if (noAttackingTimer >= noAttackingDuration) {
			noAttackingBool = false;
			noAttackingTimer = 0f;
		}
		Bleeding ();

	}

	public override void CheckState ()
	{
		switch (currentState) {
		case State.Idle:
			IdleState ();

			break;		
		case State.Move:
			MoveState ();

			break;
		case State.StandardAttacking:
			noAttackingBool = true;
			//checked in Update()
			break;
		case State.SpecialAttacking:
			//empty and unused for enemies
			break;
		case State.Roll:
			//empty and unused for enemies
			break;
		case State.Dead:
			Die ();
			break;
		case State.Knockback:
			KnockbackTimer += Time.deltaTime;
			Controller.Move (transform.TransformDirection(Vector3.back) * Time.deltaTime * KnockbackAmount);
			if (KnockbackTimer > KnockbackDuration) {
				currentState = State.Idle;
				KnockbackTimer = 0;
				agent.enabled = true;
			}

			break;
		case State.WindDown:
			WindDownTimer += Time.deltaTime;
			//agent.angularSpeed = TurnRate;

			if (WindDownTimer > WindDownDuration) {
				MoveAfterWindDownTimer += Time.deltaTime;
				Move ();
				HumanoidAnimator.SetBool ("WalkBool", true);
				if (MoveAfterWindDownTimer > MoveAfterWindDownDuration) {
					currentState = State.Idle;
					WindDownTimer = 0;
				}
			}
			break;
		case State.RunAway:
			agent.SetDestination (player.transform.position);


			break;
		}
	}

	protected override void IdleState(){
		HumanoidAnimator.SetBool ("WalkBool", false);
		HumanoidAnimator.SetBool ("IdleBool", true);
		if ((Vector3.Distance (gameObject.transform.position, player.transform.position) < attackDistanceToPlayer) && (noAttackingTimer <= 0f)) {
			currentState = State.StandardAttacking;
		} else {
			currentState = State.Move;
		}
	}

	protected override void MoveState () {
		Move ();


		HumanoidAnimator.SetBool ("WalkBool", true);
		HumanoidAnimator.SetBool ("IdleBool", false);			

		if (Vector3.Distance(gameObject.transform.position, player.transform.position) < attackDistanceToPlayer){
			currentState = State.Idle;
		}
	}
}
