using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WeaponParentClass {

	public int WeaponID;
	public string WeaponName;
	public int WeaponDamage;
	public int WeaponDurability;
	public float WeaponRange;

	public WeaponParentClass(){
		WeaponID = 0;
		WeaponName = "Default";
		WeaponDamage = 1;
		WeaponDurability = 3;
		WeaponRange = 1;
	}


}

//string newWeaponName, int newWeaponDamage, int newWeaponDurability, float newWeaponRange