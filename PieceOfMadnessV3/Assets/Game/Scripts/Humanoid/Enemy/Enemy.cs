using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Humanoid{

	//Move to the player
	public GameObject player;
	public float noticeDistanceToPlayer = 10;
	public float attackDistanceToPlayer = 4;
	private NavMeshAgent agent;
	public float TurnRate = 9001;

	//State Timers
	protected float WindDownTimer = 0f; 
	protected float WindDownDuration = 1.5f;

	protected float MoveAfterWindDownTimer = 0f; 
	protected float MoveAfterWindDownDuration = 2f;

	protected float KnockbackTimer = 0f;
	protected float KnockbackDuration = 1.3f;
	protected float KnockbackAmount = 2f;

	void Start (){
		gameObject.tag = "Enemy";
//		gameObject.layer = LayerMask.NameToLayer("EnemyLayer");
		player = GameObject.Find ("MainObjectPlayer");
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
			HumanoidAnimator.SetBool ("WalkBool", false);
			HumanoidAnimator.SetBool ("IdleBool", true);
			if (Vector3.Distance(gameObject.transform.position, player.transform.position) < noticeDistanceToPlayer){
				currentState = State.Move;
			}
			if (Vector3.Distance(gameObject.transform.position, player.transform.position) < attackDistanceToPlayer){
				currentState = State.StandardAttacking;
			}

			break;		
		case State.Move:
			Move ();
			HumanoidAnimator.SetBool ("WalkBool", true);
			HumanoidAnimator.SetBool ("IdleBool", false);			
			if (Vector3.Distance(gameObject.transform.position, player.transform.position) >= noticeDistanceToPlayer){
				currentState = State.Idle;
			}
			if (Vector3.Distance(gameObject.transform.position, player.transform.position) < attackDistanceToPlayer){
				currentState = State.Idle;
			}

			break;
		case State.StandardAttacking:
			//checked in Update()
			agent.angularSpeed = 0;
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

		//drop yo wepon
		GameObject weaponDropInstance;
		GameObject weaponDrop = (Resources.Load(Weapon.WeaponName)) as GameObject;

		weaponDropInstance = Instantiate(weaponDrop, gameObject.transform.position, gameObject.transform.rotation);
		weaponDropInstance.GetComponent<WeaponPickUp>().ThisWeapon = Weapon;

		Destroy (Controller);
		Destroy (Hitbox);
		Destroy (this);
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
}
