using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageText : MonoBehaviour {

	public Text DamText;

	void Awake () {
		DamText = gameObject.GetComponent<Text> ();
	}

	void Update () {
		if (PlayerInventory.weapons.Count > 0) {
			DamText.text = PlayerInventory.weapons [0].WeaponDamage.ToString () + " Att";
		} else {
			DamText.text = "0";
		}
	}
}