using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0)) {			
			//StartCoroutine (CameraFocusChange());
		}
		//CameraFocusChange (reword);
	}

	IEnumerator CameraFocusChange(){

		Transform reword = GameObject.Find ("Reword").GetComponent<Transform>();
		Camera camera = this.GetComponent<Camera> ();

		float passedTime = 0;

		float nowViewFieldSize = camera.fieldOfView;
		float goalviewFieldSIze = 11;

		//1초 동안
		while (true) {
			passedTime += Time.fixedDeltaTime;
			passedTime = passedTime > 1 ? 1 : passedTime;

			Vector3 viewportPoint = camera.WorldToViewportPoint (reword.position);
			Vector3 goal = new Vector3 (0.5f, 0.5f, viewportPoint.z);

			Vector3 midPoint  = Vector3.Lerp (viewportPoint, goal, passedTime);
			Vector3 distance = midPoint - viewportPoint;

			distance.x *= -1;
			distance.y *= -1;

			this.transform.Translate (distance, Space.Self);


			// fieldofView
			float viewMidPoint =  Mathf.Lerp (nowViewFieldSize, goalviewFieldSIze, passedTime);
			camera.fieldOfView = viewMidPoint;

			if (passedTime >= 1)
				break;

			yield return new WaitForFixedUpdate ();
		}

		yield return 0;
	
	}



}
