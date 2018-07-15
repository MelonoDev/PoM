using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]



public class ScrPlayerController : MonoBehaviour {

	public float speed = 7.0F;
	public float rotateSpeed = 3.0F;

	private Vector3 moveVector;

	private CharacterController _controller;

	void Start()
	{
		_controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		Vector3 move = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		_controller.Move (move * Time.deltaTime * speed);


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
		if (_controller.isGrounded == false) {
			//Add our gravity Vector
			moveVector += Physics.gravity;
		}

		//Apply our move Vector , remeber to multiply by Time.delta
		_controller.Move (moveVector * Time.deltaTime);

	}


	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}



}
/*
CharacterController controller = GetComponent<CharacterController>();
transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
Vector3 forward = transform.TransformDirection(Vector3.forward);
float curSpeed = speed * Input.GetAxis("Vertical");
controller.SimpleMove(forward * curSpeed);
*/