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
		animator.SetInteger ("DeathNum", Random.Range(1,3));
		animator.SetBool ("Dead", true);
		dead = true;
		gameObject.layer = 10;
		Destroy (gameObject,10f);
		for (int i = 0; i < componentsDestroyOnDeath.Length; i++) {
			Destroy (componentsDestroyOnDeath[i]);
		}
		for (int i = 0; i < objectsDestroyOnDeath.Length; i++) {
			Destroy (objectsDestroyOnDeath[i]);
		}
		transform.parent = null;
		gameObject.name = "Corpse";
	}
}
