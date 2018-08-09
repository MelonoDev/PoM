using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDurabilityText : MonoBehaviour {

	public Text DurText;

	void Awake () {
		DurText = gameObject.GetComponent<Text> ();
	}

	void Update () {
		if (PlayerInventory.weapons.Count > 0) {
			DurText.text = PlayerInventory.weapons [0].WeaponDurability.ToString () + " Dur";
		} else {
			DurText.text = "0";
		}
	}
}
