using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : WeaponParentClass {

	public Bite(){
		WeaponID = 9001;
		WeaponName = "Bite";
		WeaponDamage = 2;
		WeaponDurability = 8;
		WeaponRange = .3f;
	}
}
