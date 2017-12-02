using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MilitaryForcee {
	public GameObject shellPrefab;
	public float force;
	public Transform firePos;
	public float attackDmg;
	public float attackRange;
	public string state;
	[Range (0.1f, 5f)]public float reseekTargetTime;
	private float reseekTargetTimer;

	public override void Awake(){
		base.Awake ();
	}

	public override void Start () {
		base.Start ();
		state = "Seeking target";
		agent.stoppingDistance = attackRange;
	}

	public override	void Update () {
		if (!gameManager.startFight)
			return;
		if (GetComponent<Health> ().dead)
			return;
		float distanceToTarget = 99999999999999999f;
		if(target!=null)
			distanceToTarget = (transform.position - target.transform.position).magnitude;
		animator.SetFloat ("Speed", agent.velocity.magnitude / 2.5f);

		switch (state) {
		case "Seeking target":
			target = FindTarget (100f);
			if (target == null) {
				break;
			}
			if (distanceToTarget > attackRange) {
				state = "Moving to target";
			} else {
				state = "Attacking";
			}
			break;

		case "Moving to target":
			if (target == null) {
				state = "Seeking target";
				agent.isStopped = true;
				break;
			}
			if (target.GetComponent<Health>().dead) {
				state = "Seeking target";
				animator.SetBool ("Attack", false);
				agent.isStopped = true;
				break;
			}
			if(reseekTargetTimer >= reseekTargetTime){
				state = "Seeking target";
				reseekTargetTimer = 0;
				break;
			}
			if (distanceToTarget <= attackRange) {
				state = "Attacking";
				agent.isStopped = true;
				break;
			}
			animator.SetBool ("Attack", false);
			agent.isStopped = false;
			MoveToTarget ();
			reseekTargetTimer += Time.deltaTime;
			break;

		case "Attacking":
			if (target == null) {
				state = "Seeking target";
				animator.SetBool ("Attack", false);
				agent.isStopped = true;
				break;
			}
			if (target.GetComponent<Health>().dead) {
				state = "Seeking target";
				animator.SetBool ("Attack", false);
				agent.isStopped = true;
				break;
			}
			if (distanceToTarget > attackRange) {
				state = "Moving to target";
				animator.SetBool ("Attack",false);
			}
			transform.LookAt (target.transform);
			animator.SetBool ("Attack",true);
			break;
		}
	}

	void Shoot(){
		if (target != null && target.GetComponent<Health> ()) {
			GameObject rock = Instantiate (shellPrefab, firePos.position, firePos.rotation);
			rock.GetComponent<Rigidbody> ().AddForce (firePos.forward * force);
		}
	}
}
