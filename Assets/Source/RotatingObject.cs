using UnityEngine;
using System.Collections;

public class RotatingObject : MonoBehaviour {

	public float rotateAngles;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 angles = new Vector3 (0, 0, rotateAngles);


		transform.Rotate (angles * Time.deltaTime);

	}

}
