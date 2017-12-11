﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MilitaryForcee {
	public GameObject shellPrefab;
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
		state = "Moving to DP";
	}

	public override	void Update () {
		base.Update ();
		if (!gameManager.startFight)
			return;
		if (GetComponent<Health> ().dead)
			return;
		float distanceToTarget = 99999999999999999f;
		if(target!=null){
			distanceToTarget = (transform.position - target.transform.position).magnitude;
			animator.SetBool ("HaveTarget", true);
		} else {
			animator.SetBool ("HaveTarget", false);
		}
		animator.SetFloat ("Speed", agent.velocity.magnitude / 2.5f);

		switch (state) {
		case "Moving to DP":
			if ((transform.position - (linePoints [currentPoint] + placeInSquad)).magnitude < 1f) {
				if (currentPoint < linePoints.Length) {
					if (currentPoint < linePoints.Length - 1) {
						currentPoint++;
						agent.SetDestination (linePoints [currentPoint] + placeInSquad);
					}
				}
			}
			target = FindTarget (seekRadius, 1);
			if (target != null) {
				state = "Moving to target";
				break;
			}
			if ((transform.position - (linePoints[linePoints.Length - 1] + placeInSquad)).magnitude < 0.3f) {
				state = "Seeking target";
				agent.isStopped = true;
				break;
			}
			break;


		case "Seeking target":
			target = FindTarget (100f, 20);
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
			agent.isStopped = false;
			MoveToTarget ();
			reseekTargetTimer += Time.deltaTime;
			break;

		case "Attacking":
			if (target == null) {
				state = "Seeking target";
				agent.isStopped = true;
				break;
			}
			if (target.GetComponent<Health> ().dead) {
				state = "Seeking target";
				agent.isStopped = true;
				break;
			}
			if (distanceToTarget > attackRange) {
				state = "Moving to target";
			}
			transform.LookAt (target.transform);
			if (reloadingTimer >= reloadingTime) {
				animator.SetBool ("Attack", true);
				reloadingTimer = 0;
			}
			break;
		}
	}

	void Shoot(){
		animator.SetBool ("Attack", false);
		if (target != null && target.GetComponent<Health> ()) {
			GameObject arrow = Instantiate (shellPrefab, firePos.position, firePos.rotation);
			arrow.GetComponent<Arrow> ().target = target.transform;
			arrow.GetComponent<Arrow> ().dmg = attackDmg;
			arrow.GetComponent<Arrow> ().owner = gameObject;
		}
	}
}