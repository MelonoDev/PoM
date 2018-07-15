using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public EnemyClass enemyClass;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {
			if (enemyClass.IsAttacking) {
				if (other.gameObject.GetComponent<PlayerClass> ().Invulnerable == false) {
					other.gameObject.GetComponent<PlayerClass> ().Health -= enemyClass.CurrentWeapon.WeaponDamage;
				}
				other.gameObject.GetComponent<PlayerClass> ().Invulnerable = true;
			}
		}
	}
}
