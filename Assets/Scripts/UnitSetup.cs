using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSetup : MonoBehaviour {
	public LayerMask enemiesLayerMask;
	public GameObject[] renderers;
	public GameObject selection;

	void Start () {
		IdentifyEnemies ();	
	}

	void IdentifyEnemies(){
		if (gameObject.layer == 8)
			enemiesLayerMask = LayerMask.GetMask("Team2");
		else if (gameObject.layer == 9)
			enemiesLayerMask = LayerMask.GetMask("Team1");
	}
		
	public void SetTeamColor(Material mat){
		for(int i = 0; i < renderers.Length; i++){
			MeshRenderer[] rends = GetComponentsInChildren<MeshRenderer> ();
			SkinnedMeshRenderer[] rends2 = GetComponentsInChildren<SkinnedMeshRenderer> ();
			for (int j = 0; j < rends.Length; j++) {
				rends [j].material = mat;
			}
			for (int k = 0; k < rends2.Length; k++) {
				rends2 [k].material = mat;
			}
		}
	}
}
