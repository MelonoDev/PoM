using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

	public int damage = 2;
	protected float SelfDestructTimer = 0f;
	public float SelfDestructDuration = 3f;
	public Humanoid Attacker;

	protected void SelfDestruct(){
		SelfDestructTimer += Time.deltaTime;
		if (SelfDestructTimer > SelfDestructDuration) {
			Destroy (gameObject);
		}
	}

	void Update (){
		SelfDestruct ();
	}

	void OnTriggerEnter (Collider other){
		if (other.name == "MainObjectPlayer") {
			if (!other.gameObject.GetComponent<Humanoid> ().IsInvulnerable) {
				Destroy (gameObject);
			}
			other.gameObject.GetComponent<Humanoid> ().GetHit (damage, "StandardAttack", Attacker);
		}
	}
}
