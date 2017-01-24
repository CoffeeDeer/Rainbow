using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Test : MonoBehaviour {

	public UnityEvent MyEvnet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PirntText(){
	

		return;
	}

	void OnMouseDown(){
		MyEvnet.Invoke ();
	}
}
