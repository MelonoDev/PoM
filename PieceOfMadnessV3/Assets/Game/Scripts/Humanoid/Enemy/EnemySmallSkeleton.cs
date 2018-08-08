using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySmallSkeleton : EnemyStraightforwardWave {

	void Start (){
		Speed = 4f;
		MaxHealth = 15;
		Health = MaxHealth;
		Weapon = new ShortSword();
		StartTheEnemy ();
	}


}
