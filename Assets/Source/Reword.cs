using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Reword : MonoBehaviour {

	public List<UnityEvent> RewordEventList;
	public List<float>      RewordEventDelayTimeList;

	// Use this for initialization
	void Start () {
	
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

		foreach (UnityEvent k in RewordEventList) {
			if(k != null)
				k.Invoke ();
			yield return new WaitForSeconds(RewordEventDelayTimeList[i++]);
		}

		yield return 0;
	}

}
