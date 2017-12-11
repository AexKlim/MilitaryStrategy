using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour {
	public GameObject[] unitButtons;
	public GameObject changeTeamButton;
	public Color pressedButtonColor;
	public Color releasedButtonColor;
	GameManager gameManger;


	void Start () {
		gameManger = FindObjectOfType<GameManager> ();
		int n = transform.Find ("UnitButtons").childCount;
		unitButtons = new GameObject[n];
		for (int i = 0; i < n; i++) {
			unitButtons [i] = transform.Find ("UnitButtons").GetChild (i).gameObject;
		}
		changeTeamButton = transform.Find ("ChangeTeamButton").gameObject;
	}


	public void IncrementField(Text text){
		text.text = (Convert.ToInt32 (text.text) + 1).ToString ();
	}

	public void DecrementField(Text text){
		if (Convert.ToInt32 (text.text) >= 2) {
			text.text = (Convert.ToInt32 (text.text) - 1).ToString ();
		}
	}

	public void changeGameManagerXsize(Text text){
		gameManger.xSquadSise = Convert.ToInt32(text.text);
	}

	public void changeGameManagerZsize(Text text){
		gameManger.zSquadSise = Convert.ToInt32(text.text);
	}

	public void changeGameManagerSpawnableUnit(GameObject unit){
		gameManger.spawnableUnit = unit;
	}

	public void pressButton(int numberInButtonsArray){
		for (int i = 0; i < unitButtons.Length; i++) {
			if (i == numberInButtonsArray) {
				unitButtons [i].GetComponent<Image> ().color = pressedButtonColor;
			} else {
				unitButtons [i].GetComponent<Image> ().color = releasedButtonColor;
			}

		}
	}

	public void changeTeam(){
		if (gameManger.teamNumber == 1) {
			gameManger.teamNumber = 2;
			changeTeamButton.transform.Find("Text").GetComponent<Text>().text = "Team 2";
		} else if(gameManger.teamNumber ==2){
			gameManger.teamNumber = 1;
			changeTeamButton.transform.Find("Text").GetComponent<Text>().text = "Team 1";
		}
	}
}