using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour {

	GameManager gameManager;
	LineRenderer lineRenderer;

	void Awake () {
		lineRenderer = transform.Find("Line").GetComponent<LineRenderer> ();
		gameManager = FindObjectOfType<GameManager> ();
		lineRenderer.SetPosition (0, transform.position);
	}

	void Update () {
		if (gameManager.startFight) {
			//lineRenderer.enabled = false;
		}
	}
}
