using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject emptySquadPrefab;
	public GameObject lightInfantryPrefab;
	public GameObject heavyInfantryPrefab;
	public GameObject spearmanPrefab;
	public GameObject archerPrefab;
	public GameObject catapultPrefab;
	public Text team1Count;
	public Text team2Count;
	[HideInInspector] public int xSquadSise;
	[HideInInspector] public int zSquadSise;
	[HideInInspector] public GameObject spawnableUnit;
	[HideInInspector] public int teamNumber = 1;

	public Collider interfaceCol;
	public Collider col;
	public Material team1Mat;
	public Material team2Mat;

	[HideInInspector] public bool startFight;


	void Start () {
		xSquadSise = 1;
		zSquadSise = 1;
		spawnableUnit = lightInfantryPrefab;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			startFight = !startFight;
		}
		if (Input.GetMouseButtonDown (1)) {
			InstantiateSquad ();
		}
	}

	void InstantiateSquad(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (interfaceCol.Raycast (ray, out hit, 100f)) {
			return;
		}
		if (col.Raycast (ray, out hit, 1000f)) {
			GameObject squad = Instantiate(emptySquadPrefab, hit.point, Quaternion.identity);
			squad.name = spawnableUnit.name + "Squad";
			for (int i = (-xSquadSise / 2); i < xSquadSise - (xSquadSise / 2); i++) {
				for (int j = (-zSquadSise / 2); j < zSquadSise - (zSquadSise / 2); j++) {
					GameObject newWarrior = Instantiate (spawnableUnit, hit.point + new Vector3 (i, 0, j), Quaternion.identity, squad.transform.Find("Units").transform);
					newWarrior.GetComponent<MilitaryForcee> ().placeInSquad = new Vector3 (i, 0 , j);
					if (teamNumber == 1) {
						newWarrior.layer = 8;
						newWarrior.GetComponent<UnitSetup> ().SetTeamColor (team1Mat);
						newWarrior.GetComponent<MilitaryForcee> ().material = team1Mat;
					} else if (teamNumber == 2) {
						newWarrior.layer = 9;
						newWarrior.GetComponent<UnitSetup> ().SetTeamColor (team2Mat);
						newWarrior.GetComponent<MilitaryForcee> ().material = team2Mat;
					}
				}
			}
			if (teamNumber == 1) {
				team1Count.text = (Convert.ToInt32 (team1Count.text) + xSquadSise * zSquadSise).ToString ();
			} else if (teamNumber == 2) {
				team2Count.text = (Convert.ToInt32 (team2Count.text) + xSquadSise * zSquadSise).ToString ();
			}
		}
	}
}
