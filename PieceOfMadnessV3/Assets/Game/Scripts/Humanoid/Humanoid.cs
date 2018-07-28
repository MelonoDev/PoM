using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Humanoid<T> where T:Humanoid {
	public GameObject GameObject;
	public T ScriptComponent;

	public Humanoid (string name){
		GameObject = new GameObject (name);
		ScriptComponent = GameObject.AddComponent<T> ();
	}
}

public enum State {
	Idle, 
	Move, 
	StandardAttacking,
	SpecialAttacking,
	Roll,
	Dead,
	Knockback,
	WindDown

}

public abstract class Humanoid : MonoBehaviour {
	
	//Relevant objects/components
	public HitBoxScript hitBoxScript;
	public GameObject Hitbox;
	public CharacterController Controller;
	public Animator HumanoidAnimator;
	public WeaponParentClass Weapon;
	public State currentState;
	public GameObject BloodParticles;

	//audio
	public AudioSource SwordStrikeAudio;
	public AudioSource StruckBloodAudio;
	public AudioSource DeathAudio;

	//Stats
	public float Speed;
	public int MaxHealth;
	public int Health;

	//Standard Attack
	public bool isStandardAttacking = false;
	protected float isStandardAttackingTimer = 0f;
	protected float standardAttackDuration = 1f;
	protected float standardAttackWindupTimer = 0f; 
	protected float standardAttackWindupDuration = .5f;

	//Special Attack
	public bool isSpecialAttacking = false;
	protected float isSpecialAttackingTimer = 0f;
	protected float specialAttackDuration = .3f;

	public bool HasBeenSpecialAttacked = false; //for invulnerability

	//Invulnerability
	public bool IsInvulnerable = false;
	protected float InvulnerabilityTimer = 0f;
	protected float InvulnerabilityDuration = 1f;

	public bool IsBleeding = false; //for getting hit visuals
	public float BleedTimer = 0;
	public float BleedDuration = .5f;

	//abstract functions
	public abstract void CheckState ();
	protected abstract void Move ();
	protected abstract void Die ();
	protected abstract void Test ();

	void Awake (){
		//set floats and ints
		Speed = 5f;
		MaxHealth = 5;
		Health = MaxHealth;

		//Get common components
		hitBoxScript = gameObject.transform.Find("HitboxSwordStrikeNormal").GetComponent<HitBoxScript>();
		Hitbox = gameObject.transform.Find("HitboxSwordStrikeNormal").gameObject;
		Controller = gameObject.GetComponent<CharacterController>();
		HumanoidAnimator = gameObject.GetComponentInChildren<Animator>();
		Weapon = new ShortSword();
		currentState = State.Idle;
		BloodParticles = gameObject.transform.Find ("ParticlesParent").Find ("BloodParticles").gameObject;
		BloodParticles.SetActive (false);

		//Get audio components
		SwordStrikeAudio = gameObject.transform.Find("SFXParent").Find("SwordStrikeAudio").GetComponent<AudioSource>();
		StruckBloodAudio = gameObject.transform.Find("SFXParent").Find("StruckBloodAudio").GetComponent<AudioSource>();

	}

	void Update (){

		Bleeding ();
	}

	//Check invulnerability and let it end in time
	protected void Invulnerable(){ //put in Update
		if (IsInvulnerable && !HasBeenSpecialAttacked) {
			InvulnerabilityTimer += Time.deltaTime;
			if (InvulnerabilityTimer >= InvulnerabilityDuration) {
				IsInvulnerable = false;
				InvulnerabilityTimer = 0;
			}
		} else if (IsInvulnerable && HasBeenSpecialAttacked) {
			InvulnerabilityTimer += Time.deltaTime;
			if (InvulnerabilityTimer >= specialAttackDuration) {
				IsInvulnerable = false;
				InvulnerabilityTimer = 0;
				HasBeenSpecialAttacked = false;
			}
		}
	}

	//StandardAttack
	protected virtual void StandardAttack(bool AttackTrigger){
		if (AttackTrigger) {
			if (!isStandardAttacking) {
				HumanoidAnimator.SetBool ("StandardAttackBool", AttackTrigger);

				if (gameObject.tag != "Player") {
					standardAttackWindupTimer += Time.deltaTime;
				} else {
					HumanoidAnimator.SetBool("RollBool", false);
					isStandardAttacking = true;
				}
			}

			if (standardAttackWindupTimer >= standardAttackWindupDuration) {
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
				if (gameObject.tag == "Player") {
					PlayerInventory.weapons [0].WeaponDurability -= 1;
				}
				HumanoidAnimator.SetBool ("StandardAttackBool", false);
				isStandardAttacking = false;
				isStandardAttackingTimer = 0f;
			}
		} // put other else ifs for other attacks here
		else {
			Hitbox.SetActive (false);
		}
	}



	public virtual void GetHit (int damage, string attackType, Humanoid attacker){
		if (!IsInvulnerable) {
			IsBleeding = true;
		}
		if ((attackType == "StandardAttack") && (!IsInvulnerable)) {
			StruckBloodAudio.pitch = Random.Range (.6f, 1.6f);
			StruckBloodAudio.Play ();
			Health -= damage;
			IsInvulnerable = true; //special attacks are combo attacks by the player, so no vulnerabiloty there
			KnockBack (attackType);
		} else if ((attackType == "SpecialAttack") && (!IsInvulnerable)) {
			StruckBloodAudio.pitch = Random.Range (.5f, 1.3f);
			StruckBloodAudio.Play ();
			Health -= damage * 2;
			IsInvulnerable = true;
			KnockBack (attackType);
		} else if (attackType == "NoAttack") {
			print ("This is not supposed to happen");
		}
	}

	protected virtual void KnockBack (string attackType){
		HumanoidAnimator.SetTrigger ("KnockBackTrigger");
		HumanoidAnimator.SetBool ("StandardAttackBool", false);
	}

	public virtual void Bleeding (){
	if (IsBleeding) {
		BloodParticles.SetActive (true);
		BleedTimer += Time.deltaTime;
		if (BleedTimer >= BleedDuration) {
			BleedTimer = 0f;
			IsBleeding = false;
		}
	} else {
		BloodParticles.SetActive (false);
	}
	}
}






