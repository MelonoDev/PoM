using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public static List<WeaponParentClass> weapons = new List<WeaponParentClass> ();
	public AudioSource SwordBreakAudio;

	private bool noItems = false;

	// Use this for initialization
	void Start () {
		SwordBreakAudio = gameObject.GetComponent<AudioSource> ();

		weapons.Add (new ShortSword ());
		weapons.Add (new ShortSword ());
		weapons.Add (new BroadSword ());


		selectWeapon ();


	}

	void Update (){
		if (weapons.Count > 0) {
			if (weapons [0].WeaponDurability <= 0) {
				SwordBreakAudio.Play ();
				weapons.RemoveAt (0);
				selectWeapon ();
			}
		} else {
			noItems = true;
		}

		if (noItems && weapons.Count > 0) {
			selectWeapon ();
			noItems = false;
		}
	}

	void selectWeapon (){
		int i = 0;

		if (weapons.Count > 0) {
			foreach (Transform weapon in transform) {
				if (i == weapons [0].WeaponID) {
					weapon.gameObject.SetActive (true);
				} else {
					weapon.gameObject.SetActive (false);
				}
				i++;
			} 
		} else {
			foreach (Transform weapon in transform) {
				weapon.gameObject.SetActive (false);
				i++;
			}
		}

	}
}
