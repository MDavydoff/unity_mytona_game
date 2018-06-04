using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	Animator anim;
	public float smooth = 1f;
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Quaternion lookLeft;
	private Quaternion lookRight;
	private Vector3 moveDirection = Vector3.zero;

	void Start(){
		Cursor.visible = false;
		anim = GetComponent<Animator>();
		Time.timeScale = 1;
		anim.SetBool ("IsStanding", true);
		lookRight = transform.rotation;
		lookLeft = lookRight * Quaternion.Euler(0, 180, 0); 
	}

	void Update() {
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {

			anim.SetBool ("IsRunning", false);
			anim.SetBool ("IsStanding", true);
			anim.SetBool ("IsJumping", false);

			moveDirection = new Vector3(-(Input.GetAxis("Vertical")), 0, Input.GetAxis("Horizontal"));

			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
				anim.SetBool ("IsStanding", false);
				anim.SetBool ("IsRunning", false);
				anim.SetBool ("IsJumping", true);
			if (Input.GetKey(KeyCode.A)){
				transform.rotation = lookLeft;
				moveDirection = transform.TransformDirection(-moveDirection);
				moveDirection *= speed;
				anim.SetBool ("IsStanding", false);
				anim.SetBool ("IsRunning", true);

			}

			if (Input.GetKey(KeyCode.D)){
				transform.rotation = lookRight;
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;
				anim.SetBool ("IsRunning", true);
				anim.SetBool ("IsStanding", false);
			}

		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}
