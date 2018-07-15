using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetsHit : MonoBehaviour {

	public bool GetHitStandardAttack = false;
	public bool GotHit = false;
	private EnemyKnockBack enemyKnockBack;

	// Use this for initialization
	void Start () {
		enemyKnockBack = gameObject.GetComponent <EnemyKnockBack>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (GetHitStandardAttack) {
			if (!GotHit) {
				enemyKnockBack.Knockback (Vector3.back);
				print ("ouch -TestEnemy");
				GotHit = true;
			}
		}
	}

}
