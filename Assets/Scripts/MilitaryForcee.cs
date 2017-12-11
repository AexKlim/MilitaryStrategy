using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MilitaryForcee : MonoBehaviour {

	public GameObject target;
	protected NavMeshAgent agent;
	protected Animator animator;
	protected GameManager gameManager;
	public float reloadingTime;
	protected float reloadingTimer;
	[HideInInspector]public bool isSelected;
	public float seekRadius;
	[HideInInspector]public Vector3[] linePoints;
	[HideInInspector]public Vector3 placeInSquad;
	public int currentPoint;
	protected LayerMask enemiesLayerMask;

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
	}

	public virtual void Update(){
		reloadingTimer += Time.deltaTime;
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

	public virtual void MoveToTarget(){
		agent.SetDestination (target.transform.position);
	}
}
