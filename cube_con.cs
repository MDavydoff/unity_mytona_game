using UnityEngine;
using System.Collections;

public class cube_con : MonoBehaviour {
	Animator anim;
	public float smooth = 1f;
	public float speed = 6.0F;
	public float jumpSpeed = 100.0F;
	public float gravity = 20.0F;
	private Quaternion lookLeft;
	private Quaternion lookRight;
	Rigidbody rb;
	public Vector3 jump;
	public float jumpForce = 2.0f;
	void Start(){
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody> ();
		Time.timeScale = 1;
		anim.SetBool ("IsStanding", true);
		lookRight = transform.rotation;
		lookLeft = lookRight * Quaternion.Euler (0, 180, 0);
	}

	void Update() {
		CharacterController controller = GetComponent<CharacterController> ();
		if (controller.isGrounded) {
			anim.SetBool ("IsRunning", false);
			anim.SetBool ("IsStanding", true);
			anim.SetBool ("IsJumping", false);
			anim.SetBool ("IsRunningLeft", false);

			if (Input.GetKey (KeyCode.Space)) {
				rb.AddForce (0, 50, 0);
			}

			if (Input.GetKey (KeyCode.D)) {
				transform.rotation = lookRight;
				anim.SetBool ("IsStanding", false);
				anim.SetBool ("IsRunning", true);
				anim.SetBool ("IsRunningLeft", false);
			}
			if (Input.GetKey (KeyCode.A)) {
				transform.rotation = lookLeft;
				anim.SetBool ("IsStanding", false);
				anim.SetBool ("IsRunningLeft", true);
			}

		}
	}

}