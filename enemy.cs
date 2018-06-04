using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour {
	public Transform Player;
	Animator anim;
	NavMeshAgent nma;
	// Use this for initialization
	void Start () {
		nma = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
	}
	void Awake(){
	}
	// Update is called once per frame
	void Update () {
		nma.SetDestination(Player.position);
		anim.SetBool ("IsRunning", true);
		if(nma.enabled==false){
			anim.SetBool ("IsRunning", false);
		}
	}
}
