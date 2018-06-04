using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class kyle : MonoBehaviour {
	Animator anim;
	public float smooth = 1f;
	public float speed = 6.0F;
	public float jumpSpeed = 100.0F;
	public float gravity = 20.0F;
	float spheres = 0F;
	private Quaternion lookLeft;
	private Quaternion lookRight;
	private Quaternion lookBack;
	private Quaternion lookForward;
	Rigidbody rb;
	public Vector3 jump;
	public GameObject child_fx;
	public GameObject child_fx_1;
	public GameObject child_fx_2;
	public GameObject child_fx_3;
	public GameObject exit;
	public GameObject sphere;
	public float jumpForce = 2.0f;
	public Text ui_task;
	public Text ui_time;
	public Button reload;
	float targetTime = 120.0f;
	bool check=false;
	float part_play=0f;
	bool gameLives=true;
	bool gameCompleted=false;
	public NavMeshAgent enemy_1;
	public NavMeshAgent enemy_2;
	public NavMeshAgent enemy_3;
	public string lvl2;
	void Start(){
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody> ();
		Time.timeScale = 1;
		anim.SetBool ("IsStanding", true);
		lookForward = transform.rotation;
		lookBack = lookForward * Quaternion.Euler (0, 180, 0);
		lookLeft = lookForward * Quaternion.Euler (0, 270, 0);
		lookRight = lookForward * Quaternion.Euler (0, 90, 0);
		reload.onClick.AddListener(Reload);
	}

	void Update() {
		Scene scene=SceneManager.GetActiveScene ();
		if(targetTime>=0){
			targetTime -= Time.deltaTime;
		}else{
			targetTime = 0;
		}
		CharacterController controller = GetComponent<CharacterController> ();
		reload.gameObject.SetActive (false);
		reload.GetComponentInChildren<Text>().text = "Начать с начала";
		if (gameLives == true) {
			if (controller.isGrounded) {
				anim.SetBool ("IsRunning", false);
				anim.SetBool ("IsStanding", true);
				anim.SetBool ("IsJumping", false);
				anim.SetBool ("IsRunningLeft", false);

				if (Input.GetKey (KeyCode.D)) {
					transform.rotation = lookRight;
					anim.SetBool ("IsStanding", false);
					anim.SetBool ("IsRunning", true);
				}
				if (Input.GetKey (KeyCode.W)) {
					transform.rotation = lookForward;
					anim.SetBool ("IsStanding", false);
					anim.SetBool ("IsRunning", true);
				}
				if (Input.GetKey (KeyCode.S)) {
					transform.rotation = lookBack;
					anim.SetBool ("IsStanding", false);
					anim.SetBool ("IsRunning", true);
				}
				if (Input.GetKey (KeyCode.A)) {
					transform.rotation = lookLeft;
					anim.SetBool ("IsStanding", false);
					anim.SetBool ("IsRunning", true);
				}

			}
		}

		if (spheres == 3F) {
			Destroy (exit);
			ui_task.text="Все сферы собраны. Выход открыт.";
		}
		ui_time.text="Осталось "+Mathf.Round(targetTime)+" секунд";
		if (targetTime <= 0.0f)
		{
			timerEnded();
		}
		if(scene.name=="lvl2"){
			ui_task.text="Найдите выход.";

		}
	}
	void OnCollisionEnter (Collision col)
	{
		
		if(col.gameObject.tag == "sphere"){
			sphere = col.gameObject;
			child_fx = col.gameObject.transform.GetChild(1).gameObject;
			check = true;
			child_fx.GetComponent<ParticleSystem>().Play();
			Invoke("PlaySphere", 0.305f);
		}
		if(col.gameObject.tag=="escape"){
			targetTime = 0f;
			gameLives = false;
			gameCompleted = true;
			timerEnded();
			child_fx_1 = col.gameObject.transform.GetChild(0).gameObject;
			child_fx_2 = col.gameObject.transform.GetChild(1).gameObject;
			child_fx_3 = col.gameObject.transform.GetChild(2).gameObject;
			enemy_1.enabled = false;
			enemy_2.enabled = false;
			enemy_3.enabled = false;

			Scene scene=SceneManager.GetActiveScene ();
			if (scene.name == "lvl1") {
				SceneManager.LoadScene ("lvl2");
			} else {
				Invoke("PlayWin1", 1f);
			}
		}
		if(col.gameObject.tag=="deathpit"){
			targetTime = 0f;
			gameLives = false;
			gameCompleted = false;
			Destroy (gameObject);
			timerEnded();
			Scene scene=SceneManager.GetActiveScene ();
			SceneManager.LoadScene (scene.name);
			Debug.Log (scene.name);
		}
	}
	void LoadLvl2(){
		reload.GetComponentInChildren<Text>().text = "Следующий уровень";
		SceneManager.LoadScene ("lvl2");
	}
	void PlaySphere(){
		Destroy(sphere);
		if (check == true) {
			spheres++;
		}
		check = false;
		ui_task.text="Собрано сфер: "+spheres+" из 3";
	}
	void PlayWin1(){
		child_fx_2.GetComponent<ParticleSystem>().Play();
		Invoke ("PlayWin2",1f);
	}
	void PlayWin2(){
		child_fx_3.GetComponent<ParticleSystem>().Play();
		Invoke ("PlayWin3",1f);
	}
	void PlayWin3(){
		child_fx_1.GetComponent<ParticleSystem>().Play();
		Invoke ("PlayWin1",1f);
	}
	void timerEnded(){
		anim.SetBool ("IsRunning", false);
		anim.SetBool ("IsStanding", true);
		anim.SetBool ("IsJumping", false);
		anim.SetBool ("IsRunningLeft", false);
		reload.gameObject.SetActive (true);
		gameLives = false;
		if (gameCompleted == true) {
			ui_time.text = "Вы победили";
			ui_task.text = "Выход найден!";
		} else {
			ui_time.text = "Вы проиграли";
			ui_task.text = "Игра окончена";
		}
	}
	void Reload(){
		Scene scene=SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
	}
}