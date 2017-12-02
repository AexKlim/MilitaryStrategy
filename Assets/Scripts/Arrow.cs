using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	public GameObject owner;
	public Transform target;
	public float speed;
	public float dmg;

	void Start () {
		
	}

	void Update () {
		if (target != null) {
			transform.position += ((target.position + Vector3.up - transform.position).normalized * Time.deltaTime * speed);
			transform.LookAt (target.position + Vector3.up);
		} else
			Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other){
		if (target != null) {
			if (other.gameObject == target.gameObject) {
				Destroy (gameObject);
				other.gameObject.GetComponent<Health> ().TakeDmg (dmg);
			}
		}
	}
}
