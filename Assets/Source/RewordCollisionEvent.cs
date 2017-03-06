using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RewordCollisionEvent : MonoBehaviour {

	private UnityEvent[] RewordEventList = new UnityEvent[5];
	private float[]  RewordEventDelayTimeList = {3.0f,5.5f,1.0f,1.0f,0.0f};

	// Use this for initialization
	void Start () {

		//Debug.Log (GameObject.FindObjectOfType<GroundPanelManager> ());
		for (int i = 0; i < 5; i++) {
			RewordEventList [i] = new UnityEvent ();
		}
		RewordEventList [0].AddListener (GameObject.FindObjectOfType<GroundPanelManager> ().StartDropDownCoroutine);
		RewordEventList [0].AddListener (delegate{GameObject.Find("FadeOutScreen").GetComponent<Image>().enabled = true;});


		GameObject reword = GameObject.Find ("Reword");
		RewordEventList [1].AddListener (GameObject.FindObjectOfType<CameraControl> ().StartFocusChangeRoutine);
		RewordEventList [1].AddListener (delegate{reword.GetComponent<Animator> ().enabled = true;});
		RewordEventList [1].AddListener (delegate{GameObject.Find("Player").gameObject.SetActive(false);});
		RewordEventList [1].AddListener (delegate{reword.GetComponent<RotatingObject>().enabled = false;});

		Transform rewordHalo = reword.transform.GetChild (0);
		RewordEventList [2].AddListener (delegate{rewordHalo.gameObject.SetActive(true);});

		RewordEventList [3].AddListener (GameObject.FindObjectOfType<SceneChanger> ().StartSceneFadeoutEffect);

		RewordEventList [4].AddListener (GameObject.FindObjectOfType<SceneChanger> ().MapSceneLoad);

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {		
		if (other.gameObject.tag == "Player")
		{
			StartCoroutine (EventRoutine ());
		} 
	}

	IEnumerator EventRoutine(){
		int i = 0;
		Debug.Log ("CCaC");
		foreach (UnityEvent k in RewordEventList) {
			Debug.Log ("CCC");
			if(k != null)
				k.Invoke ();
			yield return new WaitForSeconds(RewordEventDelayTimeList[i++]);
		}

		yield return 0;
	}

}
