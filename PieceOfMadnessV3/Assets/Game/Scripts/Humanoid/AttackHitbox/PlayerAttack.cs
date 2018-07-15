using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	private bool isStandardAttacking = false;
	private float isStandardAttackingTimer = 0f;
	private float attackDuration = 1f;

	public Animator playerAnimator;
	public Collider Hitbox;

	void Start(){
		playerAnimator = GameObject.Find ("PlayerModel").GetComponent<Animator>(); 
		Hitbox = gameObject.GetComponent<Collider> ();
		gameObject.tag = "Player";
		gameObject.layer = LayerMask.NameToLayer("PlayerLayer");
	}

	void Update (){
		if (Input.GetKeyDown ("mouse 0")) {
			Hitbox.enabled = true;
			StandardAttack();
		}

		if (isStandardAttacking) {
			isStandardAttackingTimer += Time.deltaTime;
			if (isStandardAttackingTimer > attackDuration) {
				PlayerInventory.weapons [0].WeaponDurability -= 1;
				isStandardAttacking = false;
				isStandardAttackingTimer = 0f;
			}
		} // put other else ifs for other attacks here
		else {
			Hitbox.enabled = false;
		}
	}

	// Update is called once per frame
	void StandardAttack () {
		playerAnimator.SetTrigger("Attack1Trigger");
		isStandardAttacking = true;
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Enemy") {
			if (isStandardAttacking) {
				other.gameObject.GetComponent<EnemyGetsHit> ().GetHitStandardAttack = true;
			}
		}
	}


	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.GetComponent<EnemyGetsHit> ().GetHitStandardAttack = false;
		}
	}
}

