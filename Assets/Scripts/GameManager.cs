using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject lightInfantryPrefab;
	public GameObject heavyInfantryPrefab;
	public GameObject spearmanPrefab;
	public GameObject archerPrefab;
	public GameObject catapultPrefab;
	public GameObject gameCamera;
	public Text team1Count;
	public Text team2Count;
	[HideInInspector] public int xSquadSise;
	[HideInInspector] public int zSquadSise;

	public GameObject spawnableUnit;

	public Collider interfaceCol;
	public Collider col;
	public Material team1Mat;
	public Material team2Mat;

	[HideInInspector] public bool startFight;

	public float cameraMoveSpeed;
	public float cameraScrollSpeed;
	public float cameraRotateSpeed;

	void Start () {
		xSquadSise = 1;
		zSquadSise = 1;
		spawnableUnit = lightInfantryPrefab;
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			startFight = !startFight;
		}
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		float r = Input.GetAxis ("RotateCamera");
		float w = Input.GetAxis ("Mouse ScrollWheel");
		if (v != 0) {
			gameCamera.transform.position += new Vector3(gameCamera.transform.forward.x, 0, gameCamera.transform.forward.z).normalized * v * cameraMoveSpeed;
		}
		if (h != 0) {
			gameCamera.transform.position += gameCamera.transform.right * h * cameraMoveSpeed;
		}
		if(r != 0){
			gameCamera.transform.RotateAround (gameCamera.transform.position, Vector3.up, r * cameraRotateSpeed);
		}
		if (w != 0) {
			gameCamera.transform.position += gameCamera.transform.forward * w * cameraScrollSpeed;
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			spawnableUnit = lightInfantryPrefab;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			spawnableUnit = heavyInfantryPrefab;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			spawnableUnit = spearmanPrefab;
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			spawnableUnit = archerPrefab;
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			spawnableUnit = catapultPrefab;
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (interfaceCol.Raycast (ray, out hit, 100f)) {
				return;
			}
			if (col.Raycast (ray, out hit, 1000f)) {
			for (int i = (-xSquadSise / 2); i < xSquadSise - (xSquadSise / 2); i++) {
				for (int j = (-zSquadSise / 2); j < zSquadSise - (zSquadSise / 2); j++) {
					GameObject newWarrior = Instantiate (spawnableUnit, hit.point + new Vector3 (i, 0, j), Quaternion.identity);
						newWarrior.layer = 8;
						newWarrior.GetComponent<UnitSetup> ().SetTeamColor (team1Mat);
					}
				}
			}
			team1Count.text = (Convert.ToInt32 (team1Count.text) + xSquadSise * zSquadSise).ToString();
		}
		if (Input.GetMouseButtonDown (1)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (interfaceCol.Raycast (ray, out hit, 100f)) {
				return;
			}
			if (col.Raycast (ray, out hit, 1000f)) {
				for (int i = (-xSquadSise / 2); i < xSquadSise - (xSquadSise / 2); i++) {
					for (int j = (-zSquadSise / 2); j < zSquadSise - (zSquadSise / 2); j++) {
						GameObject newWarrior = Instantiate (spawnableUnit, hit.point + new Vector3 (i, 0, j), Quaternion.identity);
						newWarrior.layer = 9;
						newWarrior.GetComponent<UnitSetup> ().SetTeamColor (team2Mat);
					}
				}
			}
			team2Count.text = (Convert.ToInt32 (team2Count.text) + xSquadSise * zSquadSise).ToString();
		}

	}
}
