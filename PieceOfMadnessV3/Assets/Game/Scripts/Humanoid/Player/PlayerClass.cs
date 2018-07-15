using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : HumanoidClass {

	public bool Invulnerable;
	public float InvulnerabilityTimer;

	public PlayerClass(){
		HumanoidName = "Player";
		MaxHealth = 15;
		Health = MaxHealth;
		MoveSpeed = 7f;
		Invulnerable = false;
		InvulnerabilityTimer = 3f;
	}


}
