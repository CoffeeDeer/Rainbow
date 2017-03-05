using UnityEngine;
using System.Collections;

public class MultipleCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!this.transform.CompareTag ("MovableMulti")) {
			Debug.LogError ("Not MultipleCube has this component");
		}
		else{
			foreach (Transform form in this.transform) {
				FixedBlock temp = form.gameObject.AddComponent<FixedBlock>();
				temp.enabled = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
