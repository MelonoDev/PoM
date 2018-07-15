using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : HumanoidClass {

	public bool IsAttacking;

	public EnemyClass(){
		CurrentWeapon = new ShortSword();
		MoveSpeed = 5f;
		MaxHealth = 10;
		Health = MaxHealth;
		IsAttacking = false;
	}

	public void EnemyAttacks(){
//		animator.SetTrigger("Attack1Trigger");
		IsAttacking = true;
	}
}
