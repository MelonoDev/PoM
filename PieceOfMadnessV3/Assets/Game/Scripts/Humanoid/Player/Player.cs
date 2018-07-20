using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Player : Humanoid{

	static public bool PlayerIsPickingUp; //is the player rolling

	void Start(){
		gameObject.tag = "Player";
		Test ();
		Speed = 7;
		standardAttackDuration = .3f;
	}

	void Update (){
		if (PlayerInventory.weapons.Count > 0) {
			StandardAttack ((Input.GetKeyDown ("mouse 0")));
		} else {
			Hitbox.SetActive (false);
		}
		Invulnerable ();
		Move ();
		if (Health <= 0) {
			Die ();
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			PlayerIsPickingUp = true;
		} else {
			PlayerIsPickingUp = false;
		}
	}

	void FixedUpdate(){
	}

	protected override void Test ()
	{
//		print ("Player made");
	}

	private Vector3 moveVector;
	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}
	protected override void Move()	{
		Vector3 move = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		Controller.Move (move * Time.deltaTime * Speed);

		if ((Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) && ((!isStandardAttacking) || (!isSpecialAttacking))){
			HumanoidAnimator.SetBool ("WalkBool", true);
			HumanoidAnimator.SetBool ("IdleBool", false);

		} else {
			HumanoidAnimator.SetBool ("WalkBool", false);
			HumanoidAnimator.SetBool ("IdleBool", true);

		}

		//>>>An answered UnityForums question by Benzed with some modification
		//Get the Screen positions of the object
		Vector2 positionOnScreen = new Vector2 (move.x, move.z);
		positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);

		//Get the Screen position of the mouse
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint (Input.mousePosition);

		//Get the angle between the points
		float angle = AngleBetweenTwoPoints (positionOnScreen, mouseOnScreen);

		//Ta Daaa
		transform.rotation = Quaternion.Euler (new Vector3 (0f, -angle, 0f));
		//<<<

		//REeset the MoveVector
		moveVector = Vector3.zero;

		//Check if character is grounded
		if (Controller.isGrounded == false) {
			//Add our gravity Vector
			moveVector += Physics.gravity;
		}

		//Apply our move Vector , remeber to multiply by Time.delta
		Controller.Move (moveVector * Time.deltaTime);

	}

	protected override void KnockBack (string attackType)
	{
		throw new System.NotImplementedException ();
	}

	public override void CheckState ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void Die ()
	{
		print ("Player dead yo");
	}
}
