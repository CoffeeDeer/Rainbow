using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundPanelManager : MonoBehaviour {

	public GameObject GroundPanelParents;
	List<GameObject> GroundPanelList ;

	// Use this for initialization
	void Start () {		
		GroundPanelList = new List<GameObject>();
		foreach(Transform child in GroundPanelParents.transform)
		{
			GroundPanelList.Add (child.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void StartDropDownCoroutine(){
		StartCoroutine (DropDownPanel ());
	}

	IEnumerator DropDownPanel(){

		foreach (GameObject child in GroundPanelList) {
			child.AddComponent<DropDown> ();
			//child.GetComponent<Rigidbody> ().useGravity = true;
			yield return new WaitForSeconds (0.1f);
		}

		yield return new WaitForSeconds (2);
		foreach (GameObject child in GroundPanelList) {
			child.SetActive (false);
			yield return new WaitForSeconds (0.03f);
		}

		yield return 0;
	}

	public class DropDown :MonoBehaviour{
		float  acceleration = 0;

		void Start () {}

		// Update is called once per frame
		void Update () {
			this.transform.Translate(Vector3.forward*(Time.deltaTime*acceleration),Space.World);
			this.acceleration += 0.2f;
		}
	}


}
