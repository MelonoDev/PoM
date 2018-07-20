using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour {

	private bool playerInRange = false; //is the player inside the trigger
	public WeaponParentClass ThisWeapon;

	void Update () {
		if (Player.PlayerIsPickingUp && playerInRange) {
			PlayerInventory.weapons.Add(ThisWeapon);
			Destroy (gameObject);
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
