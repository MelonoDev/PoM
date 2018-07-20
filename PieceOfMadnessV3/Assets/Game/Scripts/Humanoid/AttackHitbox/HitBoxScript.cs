using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour {
	public Humanoid ThisHumanoid;
	public int ThisWeaponDamage;
	private string thisAttackType;

	private string OtherTag;

	void Awake(){
		ThisHumanoid = gameObject.GetComponentInParent<Humanoid>();
	}

	void OnEnable(){
		if (ThisHumanoid.isStandardAttacking) {
			thisAttackType = "StandardAttack";
		} else {
			thisAttackType = "NoAttack";
		}
	}

	void Update(){
		if (gameObject.tag == "Player") {
			if (PlayerInventory.weapons.Count > 0) {
				ThisWeaponDamage = PlayerInventory.weapons [0].WeaponDamage;
			} else {
				ThisWeaponDamage = 0;
			}
		} else {
			ThisWeaponDamage = ThisHumanoid.Weapon.WeaponDamage;
		}				
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag != gameObject.tag) {
			other.gameObject.GetComponent<Humanoid> ().GetHit (damage: ThisWeaponDamage, attackType: thisAttackType,  attacker: ThisHumanoid);
		}
	}

	/*
	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == OtherTag) {
		}
	}
	*/
}
