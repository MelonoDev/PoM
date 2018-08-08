using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour {

	private bool playerInRange = false; //is the player inside the trigger
	public WeaponParentClass ThisWeapon;
	public int MaxWeaponInInventory = 5;

	void Update () {
		if (PlayerInventory.weapons.Count < MaxWeaponInInventory) {
			if (Player.PlayerIsPickingUp && playerInRange) {
				GiveNumberInInv.NewWeaponPickedUp = true; //lets inventory know you picked up a weapon
				PlayerInventory.weapons.Add (ThisWeapon);
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Player") {
			playerInRange = true;
		}
	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Player") {
			playerInRange = false;
		}
	}
}
