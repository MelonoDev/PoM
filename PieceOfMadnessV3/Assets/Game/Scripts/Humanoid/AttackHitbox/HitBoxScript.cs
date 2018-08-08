using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour {
	public Humanoid ThisHumanoid;

	public int ThisWeaponDamage;
	private string thisAttackType;
	private Vector3 standardScale;

	private string OtherTag;



	void Awake(){
		ThisHumanoid = gameObject.GetComponentInParent<Humanoid>();
		standardScale = transform.localScale;
	}

	void OnEnable(){

		if (ThisHumanoid.isStandardAttacking) {
			thisAttackType = "StandardAttack";
		} else if (ThisHumanoid.isSpecialAttacking) {
			thisAttackType = "SpecialAttack";
		} else {
			thisAttackType = "NoAttack";
		}
	}
		
	void Update(){
		if (gameObject.tag == "Player") {
			if (PlayerInventory.weapons.Count > 0) {
				ThisWeaponDamage = PlayerInventory.weapons [0].WeaponDamage;
				transform.localScale = new Vector3 (standardScale.x * PlayerInventory.weapons [0].WeaponRange, standardScale.y * PlayerInventory.weapons [0].WeaponRange, standardScale.z * PlayerInventory.weapons [0].WeaponRange);
			} else {
				ThisWeaponDamage = 0;
				print ("oops");
			}
		} else {
			ThisWeaponDamage = ThisHumanoid.Weapon.WeaponDamage;
			transform.localScale = new Vector3 (standardScale.x * ThisHumanoid.Weapon.WeaponRange, standardScale.y * ThisHumanoid.Weapon.WeaponRange, standardScale.z * ThisHumanoid.Weapon.WeaponRange);
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag != gameObject.tag) {
			other.gameObject.GetComponent<Humanoid> ().GetHit (damage: ThisWeaponDamage, attackType: thisAttackType, attacker: ThisHumanoid);
			if (thisAttackType == "SpecialAttack") {
				other.gameObject.GetComponent<Humanoid> ().HasBeenSpecialAttacked = true;
			}
		}
	}
}
