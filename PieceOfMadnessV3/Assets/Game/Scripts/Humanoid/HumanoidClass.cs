using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidClass {

	public string HumanoidName;
	public int MaxHealth;
	public int Health;
	public WeaponParentClass CurrentWeapon;
	public float MoveSpeed;
	public bool Dead;
	public Collider Hitbox; // assign in prefab

	public HumanoidClass(){
		HumanoidName = "DefaultName";
		MaxHealth = 10;
		Health = MaxHealth;
		CurrentWeapon = new ShortSword();
		MoveSpeed = 5f;
		Dead = false;
	}
}
