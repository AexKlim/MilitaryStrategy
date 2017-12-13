using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MilitaryForcee {
	public GameObject shellPrefab;
	public Transform firePos;

	public override void Awake(){
		base.Awake ();
	}

	public override void Start () {
		base.Start ();
	}

	public override	void Update () {
		base.Update ();
	}

	void Shoot(){
		animator.SetBool ("Attack", false);
		if (target != null && target.GetComponent<Health> ()) {
			GameObject arrow = Instantiate (shellPrefab, firePos.position, firePos.rotation);
			arrow.GetComponent<Arrow> ().target = target.transform;
			arrow.GetComponent<Arrow> ().dmg = attackDmg;
			arrow.GetComponent<Arrow> ().owner = gameObject;
			arrow.transform.Find ("Mesh").GetComponent<MeshRenderer> ().material = material;
		}
	}
}