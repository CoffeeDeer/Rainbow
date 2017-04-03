using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogInStage : MonoBehaviour {

	public GameObject TextPanel;
	public List<string> TextList;

	float PreTime = 0f;
	int number =0; 

	// Use this for initialization
	void Start () {
		NextSpeech ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if(Time.fixedTime-PreTime > 0.25f)
			{
				PreTime = Time.fixedTime;
				NextSpeech();
			}
		}
	}

	// Update is called once per frame
	public void NextSpeech () {

		if (number == TextList.Count) {

			//EndResponseEvent.Invoke();
			TextPanel.SetActive(false);
			//this.gameObject.SetActive(false);
			//transform.parent.gameObject.GetComponent<DialogManager>().DialogOff();
			return;
		}


		TextPanel.GetComponentInChildren<Text>().text = TextList[number++];
	}
}
