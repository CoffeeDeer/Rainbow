using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundPanelManager : MonoBehaviour {

	public GameObject GroundPanelParents;
	List<Transform> GroundPanelList ;

	// Use this for initialization
	void Start () {		
		GroundPanelList = new List<Transform>();
		foreach(Transform child in GroundPanelParents.transform)
		{
			GroundPanelList.Add (child);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log (GroundPanelList.Count);
			StartCoroutine (DropDownPanel ());
		}
	}

	IEnumerator DropDownPanel(){

		foreach (Transform child in GroundPanelList) {
			child.GetComponent<Rigidbody> ().useGravity = true;
			yield return new WaitForSeconds (0.1f);
		}

		yield return new WaitForSeconds (2);
		foreach (Transform child in GroundPanelList) {
			//child.
			//yield return WaitForSeconds (0.1f);
		}

		yield return 0;
	}


}
