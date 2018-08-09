using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : Humanoid{

	static public bool PlayerIsPickingUp; //is the player rolling

	//Rolling stats
	private bool rollActivate = false;
	private float rollTimer = 0;
	private float rollDuration = .34f;
	private Vector3 rollToPosition;
	private float rollSpeed = .4f;

	private Slider healthSlider;

	//Special Attack
	private AudioSource SwordStrikeSpecialAudio;
	private int specialAttackStrikes = 3; //Amount of strikes. also has to be changed in special attack
	private float specialAttackWindDown = .1f;
	private float specialAttackMoveSpeed = .125f;
	private Vector3 specialAttackToPosition;

	//UI elements
	public GameObject DeathMessage;

	void Start(){
		gameObject.tag = "Player";
		Test ();
		Speed = 7;
		standardAttackDuration = .3f;
		MaxHealth = 20;
		Health = MaxHealth;
		healthSlider = GameObject.Find ("HealthSlider").GetComponent<Slider> ();
		healthSlider.maxValue = MaxHealth;
		UpdateHealth ();
		DeathMessage = GameObject.Find ("DeathMessage").gameObject;
		DeathMessage.SetActive (false);
		SwordStrikeSpecialAudio = gameObject.transform.Find("SFXParent").Find("SwordStrikeSpecialAudio").GetComponent<AudioSource>();
	}

	void Update (){
		Bleeding ();
		if (PlayerInventory.weapons.Count > 0 && !rollActivate) {
			StandardAttack (Input.GetKeyDown ("mouse 0"));
			SpecialAttack (Input.GetKeyDown ("mouse 1"));
		} else {
			Hitbox.SetActive (false);
		}

		if (Health <= 0) {
			Die ();
		}

		if ((Input.GetKeyDown (KeyCode.LeftShift)) && (!isStandardAttacking) && (!isSpecialAttacking)) {
			rollActivate = true;
		}
		InvincibilityObject.SetActive (IsInvulnerable);
		if (Health > MaxHealth) {
			Health = MaxHealth;
			UpdateHealth();
		}
	}



	void FixedUpdate(){
		if (rollActivate) {
			Roll ();
		}
		if ((!rollActivate) && (!isSpecialAttacking)) {
			Invulnerable ();
			Move ();
		}
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
		//HumanoidAnimator.SetTrigger ("KnockBackTrigger");
		UpdateHealth ();
	}

	public override void CheckState ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void Die ()
	{
		print ("Player dead yo");
		HumanoidAnimator.SetTrigger ("DeathTrigger");

		DeathMessage.SetActive (true);
		Destroy (Controller);
		Destroy (Hitbox);
		Destroy (this);
		//Destroy (BloodParticles);
		Destroy (InvincibilityObject);	
	}

	void Roll(){
		if (rollTimer <= 0) {
			GoToPoint ();
			HumanoidAnimator.SetTrigger ("RollTrigger");

		}

		rollToPosition = transform.TransformDirection(Vector3.left)*rollSpeed;

		rollTimer += Time.deltaTime;
		Controller.Move (rollToPosition);
		PlayerIsPickingUp = true;



		if (rollTimer >= rollDuration){
			rollTimer = 0;
			IsInvulnerable = false;
			InvulnerabilityTimer = 0;
//			HumanoidAnimator.SetBool ("RollBool", false);
			PlayerIsPickingUp = false;
			rollActivate = false;
		}
	}

	void UpdateHealth (){
		healthSlider.value = Health;
	}
		
	void SpecialAttack(bool AttackTrigger){
		if (AttackTrigger) {
			if (!isSpecialAttacking) {
				isSpecialAttacking = true;
				//place GoToPoint here to be able to have one direction the player goes in
				//HumanoidAnimator.SetBool("RollBool", false);
			}
		}
		if (isSpecialAttacking) {

			if (isSpecialAttackingTimer <= 0) {
				HumanoidAnimator.SetBool ("SpecialAttackBool", true);
				HumanoidAnimator.SetTrigger ("SpecialAttackTrigger");
				if (!SwordStrikeSpecialAudio.isPlaying) {
					SwordStrikeSpecialAudio.pitch = (Random.Range (.8f, 1.2f));
					SwordStrikeSpecialAudio.Play ();
				}
				//place GoToPoint here to be able to change direction inbetween strikes
				GoToPoint (); 
			}
			Hitbox.SetActive (true);

			specialAttackToPosition = transform.TransformDirection(Vector3.left)*specialAttackMoveSpeed;
			Controller.Move (specialAttackToPosition);

			isSpecialAttackingTimer += Time.deltaTime;
			if (isSpecialAttackingTimer > specialAttackDuration - specialAttackWindDown) {
				Hitbox.SetActive (false);
			}
			if (isSpecialAttackingTimer > specialAttackDuration) {
				HumanoidAnimator.SetBool ("SpecialAttackBool", false);
				if (specialAttackStrikes <= 0) {
					isSpecialAttacking = false;
					PlayerInventory.weapons [0].WeaponDurability -= 100;
					specialAttackStrikes = 3;
				}
				specialAttackStrikes -= 1;
				isSpecialAttackingTimer = 0f;
				IsInvulnerable = false;
			}
		}
	}

	void GoToPoint(){ //secure the player looks at where the mouse is
		//Get the Screen positions of the object
		Vector2 positionOnScreen = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.z);
		positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);

		//Get the Screen position of the mouse
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint (Input.mousePosition);

		//Get the angle between the points
		float angle = AngleBetweenTwoPoints (positionOnScreen, mouseOnScreen);

		//Ta Daaa
		transform.rotation = Quaternion.Euler (new Vector3 (0f, -angle, 0f));

		IsInvulnerable = true;
	}

	public void GetHealed (int HealAmount){
		Health += HealAmount;
		UpdateHealth ();
	}

	public override void GetHit (int damage, string attackType, Humanoid attacker)
	{
		base.GetHit (damage, attackType, attacker);
		UpdateHealth ();
	}
}
