using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwordInventory : MonoBehaviour {

	public Sprite SwordSprite;
	private Image swordImage;
	public int NumberInInventory;
	public Image OutlineImage;
	static public bool NewWeaponPickedUp = false;

	void Start () {
		swordImage = gameObject.GetComponent<Image> ();
		ChangeSprite ();
		OutlineImage = gameObject.transform.Find (gameObject.name + "Outline").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerInventory.weapons.Count > NumberInInventory) {
			if (PlayerInventory.weapons [0].WeaponDurability <= 0) {
				Invoke ("ChangeSprite", .1f);
			}
			if (NewWeaponPickedUp) {
				Invoke ("ChangeSprite", .1f);
				NewWeaponPickedUp = false;
			}

			DurabilityColour ();
		}
	}

	void ChangeSprite(){
		if (PlayerInventory.weapons.Count > NumberInInventory) {
			SwordSprite = Resources.Load<Sprite> (PlayerInventory.weapons [NumberInInventory].WeaponName) as Sprite;
		} else {
			SwordSprite = Resources.Load<Sprite> ("Default") as Sprite;
		}
		swordImage.sprite = SwordSprite;
	}

	void DurabilityColour(){
		if (PlayerInventory.weapons [NumberInInventory].WeaponDurability >= 4) {
			OutlineImage.color = new Color (0, 255, 0, .2f);
		} else if (PlayerInventory.weapons [NumberInInventory].WeaponDurability <= 1) {
			OutlineImage.color = new Color (255, 0, 0, .2f);
		} else {
			OutlineImage.color = new Color (255, 255, 0, .2f);

		}
			
	}
}
