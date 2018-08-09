using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBowman : Enemy {

	public GameObject thisProjectile;
	protected float ProjectileSpeed = 11f;

	protected float noAttackingTimer = 0f;
	protected float noAttackingDuration = 3f;//has to be longer than winddownduration
	protected bool noAttackingBool = false;

	protected float rotateTimer = 0f;
	protected float rotateDuration = 2f;

	void Start(){
		StartTheEnemy ();
		attackDistanceToPlayer = 14f;
		Hitbox.SetActive (false);
		WindDownDuration = .3f;
		Speed = 5f;
		MaxHealth = 6;
		Health = MaxHealth;
		Weapon = new Bite ();
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
		agent.SetDestination (gameObject.transform.position);



		if ((Vector3.Distance (gameObject.transform.position, player.transform.position) < attackDistanceToPlayer) && (noAttackingTimer <= 0f)) {
			currentState = State.StandardAttacking;
		} else if (Vector3.Distance (gameObject.transform.position, player.transform.position) < attackDistanceToPlayer) {
			currentState = State.Rotate;
		} else {
			currentState = State.Move;
		}
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

			if (WindDownTimer > WindDownDuration) {
				MoveAfterWindDownTimer += Time.deltaTime;

				//Some animation that plays for knocking a bow would work here

				if (MoveAfterWindDownTimer > MoveAfterWindDownDuration) {
					currentState = State.Idle;
					WindDownTimer = 0;
				}
			}
			break;
		case State.RunAway:
			agent.SetDestination (player.transform.position);


			break;
		case State.Rotate:
			if (rotateTimer <= 0f) {
				agent.enabled = false;
			}
			transform.rotation = Quaternion.LookRotation (player.transform.position - transform.position);
			rotateTimer += Time.deltaTime;
			if (rotateTimer >= rotateDuration) {
				rotateTimer = 0;
				agent.enabled = true;
				currentState = State.Idle;
			}


			break;
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
			if (!SwordStrikeAudio.isPlaying && isStandardAttackingTimer <= 0) {
				GameObject tempObj;
				tempObj = Instantiate (thisProjectile) as GameObject;
				tempObj.transform.position = Hitbox.transform.position;
				tempObj.transform.rotation = transform.rotation;
				Rigidbody rb = tempObj.GetComponent<Rigidbody> ();
				rb.velocity = gameObject.transform.forward * ProjectileSpeed;


				SwordStrikeAudio.Play ();
			}
			currentState = State.WindDown;
			//Hitbox.SetActive (true);
			isStandardAttackingTimer += Time.deltaTime;
			if (isStandardAttackingTimer > standardAttackDuration) {
				HumanoidAnimator.SetBool ("StandardAttackBool", false);
				isStandardAttacking = false;
				isStandardAttackingTimer = 0f;
			}
		}
	}
}
