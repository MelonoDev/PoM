using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveNumberInInv : MonoBehaviour {
	
	static public bool NewWeaponPickedUp = false;

	// Use this for initialization
	void Awake () {
		MakeInvNum ();
	}

	void Update(){
		if (NewWeaponPickedUp) {
			NewWeaponPickedUp = false;
			foreach (Transform invSword in transform) {
				invSword.gameObject.GetComponent<UISwordInventory> ().MyNewWeaponPickedUp = true;
			}
		}
	}
	
	// Update is called once per frame
	public void MakeInvNum() {
		int i = 0;
		foreach (Transform invSword in transform) {
			invSword.gameObject.GetComponent<UISwordInventory> ().NumberInInventory = i;
//			print (invSword.gameObject.GetComponent<UISwordInventory> ().NumberInInventory.ToString () + "updated");
			i++;
		}
	}
}
