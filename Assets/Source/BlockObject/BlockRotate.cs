using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlockRotate : MonoBehaviour {

	Vector3 originalScale;

	bool IsRotating = false;

	public void StartRotatingRoutine(){

		if (IsRotating == false) {
			Debug.Log ("CC");
			StartCoroutine(makeSmallCube());
		}
	}


	IEnumerator makeSmallCube(){
		IsRotating = true;
		GetComponent<Rigidbody> ().isKinematic = true;
		GameObject.Find ("FadeOutScreen").GetComponent<Image>().enabled = true;


		originalScale = this.transform.localScale;
		Vector3 goalScale = new Vector3 (originalScale.x * 0.8f, originalScale.y * 0.8f, originalScale.z);
		int loop = 5;
		for (int i = 0; i < loop; i++) {
			Vector3 changeVec = Vector3.Lerp (originalScale, goalScale, (i + 1) / (float)loop);

			transform.localScale = changeVec;
			yield return new WaitForFixedUpdate ();
		}

		yield return StartCoroutine(RotatingCube());
	}


	IEnumerator RotatingCube(){

		Vector3 originalRotation = transform.localEulerAngles;

		Vector3 goalRotation = originalRotation;
		goalRotation.z += 90;


		int loop = 10;
		for (int i = 0; i < loop; i++) {
			Vector3 changeVec = Vector3.Lerp (originalRotation, goalRotation, (i + 1) / (float)loop);
			//Debug.Log ((i + 1 / (float)loop));
			transform.eulerAngles = changeVec;
			yield return new WaitForFixedUpdate ();
		}

		yield return StartCoroutine(makeOriginalCube());
	}

	IEnumerator makeOriginalCube(){
		Vector3 nowScale = this.transform.localScale;
		Vector3 goalScale = originalScale;

		int loop = 5;
		for (int i = 0; i < loop; i++) {
			Vector3 changeVec = Vector3.Lerp (nowScale, goalScale, (i + 1) / (float)loop);
			transform.localScale = changeVec;
			yield return new WaitForFixedUpdate ();
		}


		GetComponent<Rigidbody> ().isKinematic = false;
		yield return new WaitForSeconds (0.1f);

		GetComponent<BlockObject> ().UpdateBlockDirectionCode ();//.UpdateBlockDirectionCode ();
		IsRotating = false;
		GameObject.Find ("FadeOutScreen").GetComponent<Image>().enabled = false;
		yield return 0;
	}
}
