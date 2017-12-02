using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour {
	GameManager gameManger;
	void Start () {
		gameManger = FindObjectOfType<GameManager> ();
	}

	void Update () {
		
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
}
