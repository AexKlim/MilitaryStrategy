using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MilitaryForcee : MonoBehaviour {

	protected GameManager gameManager;
	protected NavMeshAgent agent;
	protected Animator animator;

	public float attackDmg;
	public float attackRange;
	public float reloadingTime;
	protected float reloadingTimer;

	public float seekRadius;
	protected GameObject target;
	protected float distanceToTarget;
	protected LayerMask enemiesLayerMask;
	[HideInInspector]public Material material;
	[Range (0.1f, 5f)]public float reseekTargetTime;
	protected float reseekTargetTimer;

	[HideInInspector]public bool isSelected;
	[HideInInspector]public Vector3[] linePoints;
	[HideInInspector]public Vector3 placeInSquad;
	protected int currentPoint;

	public enum States {enRoute, seekingTarget, movingToTarget, attacking};
	public States state;

	public virtual void Awake(){
		if (GetComponent<NavMeshAgent> ()) {
			agent = GetComponent<NavMeshAgent> ();
		}
		animator = GetComponent<Animator> ();
		gameManager = GameObject.FindObjectOfType<GameManager> ();
	}

	public virtual void Start () {
		reloadingTimer = reloadingTime;
		enemiesLayerMask = GetComponent<UnitSetup> ().enemiesLayerMask;
		linePoints = new Vector3[1];
		linePoints[0] = transform.position;
		state = States.enRoute;
	}

	public virtual void Update(){
		if (!gameManager.startFight || GetComponent<Health> ().dead)
			return;
		reloadingTimer += Time.deltaTime;
		distanceToTarget = 99999999999999999f;
		if (target != null) {
			distanceToTarget = (transform.position - target.transform.position).magnitude;
			animator.SetBool ("HaveTarget", true);
		} else {
			animator.SetBool ("HaveTarget", false);
		}
		animator.SetFloat ("Speed", agent.velocity.magnitude / 2.5f);

		switch (state) {
		case States.enRoute:
			EnRoute ();
			break;

		case States.seekingTarget:
			SeekingTarget ();
			break;

		case States.movingToTarget:
			MovingToTarget ();
			break;

		case States.attacking:
			Attacking ();
			break;
		}
	}

	public virtual GameObject FindTarget(float seekRadius, int areasNumber){
		for(int j = 1; j <= areasNumber; j++){
			Collider[] enemies = Physics.OverlapSphere (transform.position, seekRadius * j / areasNumber, enemiesLayerMask);
			if (enemies.Length == 0)
				continue;
			Collider target = enemies[0];
			float minDist = (enemies[0].transform.position - transform.position).magnitude;
			for (int i = 1; i < enemies.Length; i++) {
				float dist = (enemies [i].transform.position - transform.position).magnitude;
				if (dist < minDist) {
					target = enemies [i];
					minDist = dist;
				}
			}
			return target.gameObject;
		}
		return null;
	}


	//STATES______________________________________________________________________________

	public virtual void EnRoute(){
		if ((transform.position - (linePoints [currentPoint] + placeInSquad)).magnitude < 1f) {
			if (currentPoint < linePoints.Length) {
				if (currentPoint < linePoints.Length - 1) {
					currentPoint++;
					agent.SetDestination (linePoints [currentPoint] + placeInSquad);
				}
			}
		}
		if (linePoints.Length == 1) {
			state = States.seekingTarget;
		}
		target = FindTarget (seekRadius, 1);
		if (target != null) {
			state = States.movingToTarget;
			return;
		}
		if ((transform.position - (linePoints[linePoints.Length - 1] + placeInSquad)).magnitude < 1f) {
			state = States.movingToTarget;
			agent.isStopped = true;
			return;
		}
	}

	public virtual void SeekingTarget(){
		target = FindTarget (300f, 1);

		if (target == null) {
			return;
		}
		if (distanceToTarget > attackRange) {
			state = States.movingToTarget;
		} else {
			state = States.attacking;
		}
	}

	public virtual void MovingToTarget(){
		if (target == null) {
			state = States.seekingTarget;
			agent.isStopped = true;
			return;
		}
		if (target.GetComponent<Health>().dead) {
			state = States.seekingTarget;
			agent.isStopped = true;
			return;
		}
		if(reseekTargetTimer >= reseekTargetTime){
			state = States.seekingTarget;
			reseekTargetTimer = 0;
			return;
		}
		if (distanceToTarget <= attackRange) {
			state = States.attacking;
			agent.isStopped = true;
			return;
		}
		agent.isStopped = false;
		agent.SetDestination (target.transform.position);
		reseekTargetTimer += Time.deltaTime;
	} 

	public virtual void Attacking(){
		if (target == null) {
			state = States.seekingTarget;
			agent.isStopped = true;
			return;
		}
		if (target.GetComponent<Health>().dead) {
			state = States.seekingTarget;
			agent.isStopped = true;
			return;
		}
		if (distanceToTarget > attackRange) {
			state = States.movingToTarget;
		}
		transform.LookAt (target.transform);
		if (reloadingTimer >= reloadingTime) {
			animator.SetBool ("Attack", true);
			reloadingTimer = 0;
		}
	}

}
