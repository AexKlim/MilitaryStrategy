using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : MilitaryForcee {

	public override void Awake(){
		base.Awake ();
	}

	public override void Start () {
		base.Start ();
	}

	public override void Update () {
		base.Update ();
	}

	void HitTheTarget(){
		if(target != null && target.GetComponent<Health>())
			target.GetComponent<Health> ().TakeDmg (attackDmg);
		animator.SetBool ("Attack", false);
	}

	void RandomAttack(){
		animator.SetInteger ("AttackNum", Random.Range (1, 3));
	}
}
