using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour {

	public float moveSpeed;
	public float scrollSpeed;
	public float rotateSpeed;


	void Start () {
		
	}

	void FixedUpdate(){
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		float r = Input.GetAxis ("RotateCamera");
		float w = Input.GetAxis ("Mouse ScrollWheel");
		if (v != 0) {
			transform.position += new Vector3 (transform.forward.x, 0, transform.forward.z).normalized * v * moveSpeed;
		}
		if (h != 0) {
			transform.position += transform.right * h * moveSpeed;
		}
		if (r != 0) {
			transform.RotateAround (transform.position, Vector3.up, r * rotateSpeed);
		}
		if (w != 0) {
			transform.position += transform.forward * w * scrollSpeed;
		}
	}
}
