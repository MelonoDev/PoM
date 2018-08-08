using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Humanoid{

	//Move to the player
	public float noticeDistanceToPlayer = 10f;
	public float attackDistanceToPlayer = 5f;
	public NavMeshAgent agent;
	public float TurnRate = 9001;

	//State Timers
	protected float WindDownTimer = 0f; 
	protected float WindDownDuration = 1.5f;

	protected float MoveAfterWindDownTimer = 0f; 
	protected float MoveAfterWindDownDuration = 2f;

	protected float KnockbackTimer = 0f;
	protected float KnockbackDuration = 1.3f;
	protected float KnockbackAmount = 2f;




	void Start(){
		StartTheEnemy ();
	}

	public void StartTheEnemy (){
		gameObject.tag = "Enemy";
		standardAttackWindupDuration = 1.35f; //change the windup to attack
		agent = gameObject.GetComponent<NavMeshAgent>(); 
		agent.speed = Speed;
	}

	void FixedUpdate (){
		Test ();
		Invulnerable ();
		CheckState ();
		StandardAttack (currentState == State.StandardAttacking);
		if (Health <= 0) {
			currentState = State.Dead;
		}

		InvincibilityObject.SetActive (IsInvulnerable);

		//print (Vector3.Distance (gameObject.transform.position, player.transform.position).ToString ());
	}



	protected override void Test ()
	{
//		print (Health.ToString () + " EnemyHealth");
	}
		
	protected override void Move ()
	{
		/*
		Vector3 relativePos = player.transform.position - transform.position;
		Controller.Move (transform.TransformDirection(Vector3.forward) * Time.deltaTime * Speed);
		transform.rotation = Quaternion.LookRotation(relativePos);
*/

		agent.angularSpeed = TurnRate;

		agent.SetDestination (player.transform.position);
	}

	protected override void KnockBack (string attackType)
	{
		agent.enabled = false;

		base.KnockBack (attackType);
		isStandardAttacking = false;
		Vector3 relativePos = player.transform.position - transform.position;
		Controller.Move (transform.TransformDirection(Vector3.forward) * Time.deltaTime * Speed);
		transform.rotation = Quaternion.LookRotation(relativePos);

		ResetTimers ();
		currentState = State.Knockback;
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
		}
	}

	protected override void Die ()
	{
		HumanoidAnimator.SetTrigger ("DeathTrigger");


		if (Weapon.WeaponID < 9000) {
			//drop yo wepon
			//if (!HasBeenSpecialAttacked) {
				GameObject weaponDropInstance;
				GameObject weaponDrop = (Resources.Load (Weapon.WeaponName)) as GameObject;

				weaponDropInstance = Instantiate (weaponDrop, gameObject.transform.position, gameObject.transform.rotation);
				weaponDropInstance.GetComponent<WeaponPickUp> ().ThisWeapon = Weapon;
			//}
		}

		Destroy (Controller);
		Destroy (Hitbox);
		Destroy (this);
		Destroy (BloodParticles);
		Destroy (InvincibilityObject);
	}

	//reset the timers in case of interruption of a state, in the middle of any timer
	protected void ResetTimers(){
		WindDownTimer = 0;
		MoveAfterWindDownTimer = 0;
		KnockbackTimer = 0;
		InvulnerabilityTimer = 0;
		isSpecialAttackingTimer = 0;
		isStandardAttackingTimer = 0;
		standardAttackWindupTimer = 0;
	}

	protected virtual void MoveState(){
		Move ();
		HumanoidAnimator.SetBool ("WalkBool", true);
		HumanoidAnimator.SetBool ("IdleBool", false);			
		if (Vector3.Distance(gameObject.transform.position, player.transform.position) >= noticeDistanceToPlayer){
			currentState = State.Idle;
		}
		if (Vector3.Distance(gameObject.transform.position, player.transform.position) < attackDistanceToPlayer){
			currentState = State.Idle;
		}
	}

	protected virtual void IdleState(){
		HumanoidAnimator.SetBool ("WalkBool", false);
		HumanoidAnimator.SetBool ("IdleBool", true);
		if (Vector3.Distance(gameObject.transform.position, player.transform.position) < noticeDistanceToPlayer){
			currentState = State.Move;
		}
		if (Vector3.Distance(gameObject.transform.position, player.transform.position) < attackDistanceToPlayer){
			currentState = State.StandardAttacking;
		}
	}


}
