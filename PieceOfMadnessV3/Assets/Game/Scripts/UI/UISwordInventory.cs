using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwordInventory : MonoBehaviour {

	public Sprite SwordSprite;
	private Image swordImage;
	public int NumberInInventory;
	public int InventoryNumber = 0;
	public Image OutlineImage;
	public GiveNumberInInv giveNumberInInv;
	public bool MyNewWeaponPickedUp = false;

	void Start () {
		giveNumberInInv = gameObject.GetComponentInParent<GiveNumberInInv> ();
		giveNumberInInv.MakeInvNum ();
		swordImage = gameObject.GetComponent<Image> ();
		OutlineImage = gameObject.transform.Find ("Outline").GetComponent<Image>();

		InventoryNumber += NumberInInventory;
		print (NumberInInventory.ToString () + " is the numinv");
		ChangeSprite ();
		//StartCoroutine(ChangeSprite());
	}
	
	// Update is called once per frame
	void Update () {

		if (PlayerInventory.weapons.Count > NumberInInventory) {
			if (PlayerInventory.weapons [0].WeaponDurability <= 0) {
				//Invoke ("ChangeSprite", .1f);
				ChangeSprite ();
				//StartCoroutine(ChangeSprite());
				print ("Weapon gotta be changed now");

			}
			if (MyNewWeaponPickedUp) {
				//Invoke ("ChangeSprite", .1f);
				ChangeSprite ();
				//StartCoroutine(ChangeSprite());

				MyNewWeaponPickedUp = false;
				print ("Weapon gotta be changed now");
			}
			DurabilityColour ();
		}
		ChangeSprite ();

	}

	private void ChangeSprite(){
		//yield return new WaitForSeconds (.1f);

		giveNumberInInv.MakeInvNum ();

		if (PlayerInventory.weapons.Count > NumberInInventory) {
			SwordSprite = Resources.Load<Sprite> (PlayerInventory.weapons [NumberInInventory].WeaponName) as Sprite;
		} else {
			SwordSprite = Resources.Load<Sprite> ("Default") as Sprite;
		}
		print ("Sometin loaded in " + NumberInInventory.ToString());

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
