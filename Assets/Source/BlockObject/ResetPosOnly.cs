using UnityEngine;
using System.Collections;

public class ResetPosOnly : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartResetPosCoroutine(){
		StartCoroutine (ResetPos ());
	}

	IEnumerator ResetPos()
	{
		Transform myForm = transform;
		/*
		if (transform.childCount != 0) {
			myForm = transform.GetChild(0);
			myForm.parent = null;
			transform.parent = myForm;
			myForm = transform.parent;
		}
		*/
		float XPos = myForm.position.x;
		float YPos = myForm.position.y;
		float targetXPos = Mathf.Round (XPos);
		float targetYPos = Mathf.Round (YPos);

		for (int i = 0; i< 10; i ++) {
			
			myForm.Translate((targetXPos -XPos)/10.0f ,(targetYPos - YPos)/10.0f,.0f,Space.World);
			yield return new WaitForSeconds(.03f);
		}


		//myForm.position = new Vector3 (targetXPos, targetYPos, myForm.position.z);

		
		yield return null;
	}
}
