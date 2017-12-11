using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitSelection : MonoBehaviour {

	public GUISkin skin;
	public LayerMask militaryForceMask;

	public List<GameObject> selectedUnits;
	public GameObject selectedSquad;
	public float lineCutSize;

	Collider col;


	void Awake () {
		selectedUnits = new List<GameObject> ();
		col = FindObjectOfType<GameManager> ().col;
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			SelectSquad();
			if(selectedSquad != null)
				selectedSquad.transform.Find("Line").GetComponent<LineRenderer> ().positionCount = 1;
		}

		if (Input.GetMouseButton (0) && selectedSquad != null) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			LineRenderer lr = selectedSquad.transform.Find("Line").GetComponent<LineRenderer> ();
			if (col.Raycast (ray, out hit, 1000f)) {
				if ((lr.GetPosition (lr.positionCount - 1) - hit.point).magnitude >= lineCutSize) {
					lr.positionCount++;
					lr.SetPosition (lr.positionCount - 1, hit.point + new Vector3(0, 0.1f, 0));
				}
			}
		}

		if (Input.GetMouseButtonUp (0) && selectedSquad != null) {
			MilitaryForcee[] units = selectedSquad.transform.GetComponentsInChildren<MilitaryForcee> ();
			LineRenderer lr = selectedSquad.transform.Find("Line").GetComponent<LineRenderer> ();
			foreach (MilitaryForcee unit in units) {
				unit.linePoints = new Vector3[lr.positionCount];
				for (int i = 0; i < lr.positionCount; i++) {
					unit.linePoints [i] = lr.GetPosition (i);
				}
			}
		}
	}

	void OnGUI(){
		//GUI.skin = skin;
//		if (Input.GetMouseButton (0) && (Input.mousePosition - firstClick).magnitude > 10f) {
			//GUI.Box (GetRectFromPoints (firstClick, Input.mousePosition), GUIContent.none);
//		}
	}

	private Rect GetRectFromPoints(Vector3 one, Vector3 two){
		float height = two.x - one.x;
		float width = (Screen.height - two.y) - (Screen.height - one.y);
		return new Rect (one.x, Screen.height - one.y, height, width);
	}

	private void SelectUnits(Vector3 one, Vector3 two){
		foreach (GameObject unitt in selectedUnits) {
			unitt.GetComponent<MilitaryForcee>().isSelected = false;
			unitt.GetComponent<UnitSetup> ().selection.SetActive (false);
		}
		selectedUnits.Clear();
		Ray ray1 = Camera.main.ScreenPointToRay (one);
		RaycastHit hit1;
		Ray ray2 = Camera.main.ScreenPointToRay (two);
		RaycastHit hit2;
		if (col.Raycast (ray1, out hit1, 1000f) && col.Raycast (ray2, out hit2, 1000f)) {
			Vector3 p1 = hit1.point;
			Vector3 p2 = hit2.point;
			Vector3 center = new Vector3 ((p1.x + p2.x)/2, (p1.y + p2.y)/2, (p1.z + p2.z)/2); 
			Debug.DrawLine (Camera.main.transform.position, p1);
			Debug.DrawLine (Camera.main.transform.position, p2);
			Collider[] selectedColls = Physics.OverlapBox (center, new Vector3( Mathf.Abs(p2.x-p1.x)/2, 100, Mathf.Abs(p2.z-p1.z)/2), Quaternion.identity, militaryForceMask);
			for (int i = 0; i < selectedColls.Length; i++) {
				selectedUnits.Add (selectedColls[i].gameObject);
			}
			foreach (GameObject unitt in selectedUnits) {
				unitt.GetComponent<MilitaryForcee>().isSelected = true;
				unitt.GetComponent<UnitSetup> ().selection.SetActive (true);
			}
		}
	}     // Maybe i ll use it later

	private void SelectSquad(){
		if (selectedSquad != null) {
			Transform units = selectedSquad.transform.Find ("Units");
			for (int i = 0; i < units.childCount; i++) {
				units.GetChild (i).GetComponent<MilitaryForcee> ().isSelected = false;
				units.GetChild (i).GetComponent<UnitSetup> ().selection.SetActive (false);
			}
		}
		selectedSquad = null;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast (ray, out hit, 1000f, militaryForceMask);
		if (hit.collider != null) {
			selectedSquad = hit.collider.transform.parent.parent.gameObject;
		}
		if (selectedSquad != null) {
			Transform units = selectedSquad.transform.Find ("Units");
			for (int i = 0; i < units.childCount; i++) {
				units.GetChild (i).GetComponent<MilitaryForcee> ().isSelected = true;
				units.GetChild (i).GetComponent<UnitSetup> ().selection.SetActive (true);
			}
		}
	}
}
