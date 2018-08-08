using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBigSkeleton : EnemyStraightforwardWave {

	void Start (){
		Speed = 6f;
		MaxHealth = 20;
		Health = MaxHealth;
		Weapon = new BroadSword();
		StartTheEnemy ();
		attackDistanceToPlayer = 8f;
	}


}
