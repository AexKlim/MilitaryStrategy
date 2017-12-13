using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MilitaryForcee {
	public GameObject shellPrefab;
	public float shellExplosionRadius;
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
			GameObject rock = Instantiate (shellPrefab, firePos.position, firePos.rotation);
			float length = Mathf.Sqrt (((firePos.transform.position - target.transform.position).magnitude * Physics.gravity.y) / Mathf.Sin(firePos.localRotation.eulerAngles.x * Mathf.Deg2Rad * 2));
			Vector3 velocityy = firePos.forward * length;
			rock.GetComponent<Rigidbody> ().velocity = velocityy;
			rock.GetComponent<Rock> ().maxDmg = attackDmg;
			rock.GetComponent<Rock> ().owner = gameObject;
			rock.GetComponent<Rock> ().explosionRadius = shellExplosionRadius;
		}
	}
}
