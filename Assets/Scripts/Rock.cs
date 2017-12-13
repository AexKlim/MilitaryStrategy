using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {
	public GameObject owner;
	public float maxDmg;
	public float explosionRadius;
	public LayerMask mask;

	void Start () {

	}

	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Ground") {
			Collider[] units = Physics.OverlapSphere (transform.position,explosionRadius, mask);
			foreach (Collider unit in units) {
				if (unit.gameObject.GetComponent<Health> ()) {
					//float dmg = ((explosionRadius - Vector3.Distance (transform.position, unit.transform.position)) / explosionRadius) * maxDmg;
					//Debug.Log (dmg);
					unit.gameObject.GetComponent<Health> ().TakeDmg (maxDmg);
				}
			}
			Destroy (gameObject);
		}
	}
}
