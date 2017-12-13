using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public float maxHp;
	public float hp;
	public GameObject hpBar;
	private Animator animator;
	public Behaviour[] componentsDestroyOnDeath; 
	public GameObject[] objectsDestroyOnDeath;
	public bool dead;

	void Awake(){
		animator = GetComponent<Animator> ();
	}

	void Start () {
		hp = maxHp;	
	}

	void Update () {
		
	}

	public void TakeDmg(float dmg){
		hp -= dmg;
		if (hpBar != null) {
			hpBar.transform.localScale = new Vector3 (hp / maxHp, 1f, 1f);
		}
		if (hp <= 0) {
			OnDeath ();
		}
	}

	void OnDeath(){
		animator.SetInteger ("DeathNum", UnityEngine.Random.Range(1,3));
		animator.SetBool ("Dead", true);
		dead = true;
		//Destroy (gameObject,10f);
		transform.parent = null;
		gameObject.name = "Corpse";
		if (gameObject.layer == 8) {
			Text team1CountText = GameObject.Find("Interface").GetComponent<Interface>().team1Count.GetComponent<Text> ();
			team1CountText.text = (Convert.ToInt32 (team1CountText.text) - 1).ToString ();
		} else if(gameObject.layer == 9){
			Text team2CountText = GameObject.Find("Interface").GetComponent<Interface>().team2Count.GetComponent<Text> ();
			team2CountText.text = (Convert.ToInt32 (team2CountText.text) - 1).ToString ();
		}
		gameObject.layer = 10;
	}

	public void BecomeCourpse(){  //Выполняется в последний кадр анимации смерти
		for (int i = 0; i < componentsDestroyOnDeath.Length; i++) {
			Destroy (componentsDestroyOnDeath[i]);
		}
		for (int i = 0; i < objectsDestroyOnDeath.Length; i++) {
			Destroy (objectsDestroyOnDeath[i]);
		}
	}
}
