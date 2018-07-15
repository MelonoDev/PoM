using System.Collections;
using UnityEngine;

public class Humanoid<T> where T:Humanoid {
	public GameObject GameObject;
	public T ScriptComponent;

	public Humanoid (string name){
		GameObject = new GameObject (name);
		ScriptComponent = GameObject.AddComponent<T> ();
	}
}

public abstract class Humanoid : MonoBehaviour {
	//Relevant objects/components
	public HitBoxScript hitBoxScript;
	public GameObject Hitbox;
	public CharacterController Controller;
	public Animator HumanoidAnimator;
	public WeaponParentClass Weapon;

	//Stats
	public float Speed;
	public int MaxHealth;
	public int Health;

	//Standard Attack
	public bool isStandardAttacking = false;
	protected float isStandardAttackingTimer = 0f;
	protected float standardAttackDuration = 1f;
	public float standardAttackWindup = .2f; //set in hitbox to see

	//Special Attack
	public bool isSpecialAttacking = false;
	protected float isSpecialAttackingTimer = 0f;
	protected float specialAttackDuration = 1f;

	//Invulnerability
	public bool IsInvulnerable = false;
	protected float InvulnerabilityTimer = 0f;
	protected float InvulnerabilityLength = 1f;

	//abstract functions
	protected abstract void Move ();
//	protected abstract void Die ();
	protected abstract void Test ();

	void Awake (){
		//set floats and ints
		Speed = 2.1f;
		MaxHealth = 5;
		Health = MaxHealth;

		//Get common components
		hitBoxScript = gameObject.transform.Find("HitboxSwordStrikeNormal").GetComponent<HitBoxScript>();
		Hitbox = gameObject.transform.Find("HitboxSwordStrikeNormal").gameObject;
		Controller = gameObject.GetComponent<CharacterController>();
		HumanoidAnimator = gameObject.GetComponentInChildren<Animator>();
		Weapon = new ShortSword();
	}

	//Check invulnerability and let it end in time
	protected void Invulnerable(){ //put in Update
		if (IsInvulnerable) {
			InvulnerabilityTimer += Time.deltaTime;
			if (InvulnerabilityTimer >= InvulnerabilityLength){
				IsInvulnerable = false;
				InvulnerabilityTimer = 0;
			}
		}
		print (IsInvulnerable.ToString () + "Invulnerability");
	}

	//StandardAttack
	protected void StandardAttack(bool AttackTrigger){
		if (AttackTrigger) {
			if (!isStandardAttacking) {
				HumanoidAnimator.SetTrigger("StandardAttackTrigger");
			}
			isStandardAttacking = true;
		}
		if (isStandardAttacking) {
			Hitbox.SetActive (true);
			isStandardAttackingTimer += Time.deltaTime;
			if (isStandardAttackingTimer > standardAttackDuration) {
				if (gameObject.tag == "Player") {
					PlayerInventory.weapons [0].WeaponDurability -= 1;
				}
				isStandardAttacking = false;
				isStandardAttackingTimer = 0f;
			}
		} // put other else ifs for other attacks here
		else {
			Hitbox.SetActive (false);
		}
	}



	public void GetHit (int damage, string attackType, Humanoid attacker){
		if (!IsInvulnerable) {
			if (attackType == "StandardAttack") {
				Health -= damage;
				IsInvulnerable = true; //special attacks are combo attacks by the player, so no vulnerabiloty there
				print ("Has Invulnerability?");
				if (gameObject.tag == "Enemy") {
					KnockBack (attackType);
				}
			} else if (attackType == "SpecialAttack") {
				Health -= 2 * damage;
				KnockBack (attackType);
			} else if (attackType == "NoAttack") {
				print ("This is not supposed to happen");
			}
		}
	}

	protected virtual void KnockBack (string attackType){
		HumanoidAnimator.SetTrigger ("KnockBackTrigger");
	}

}






