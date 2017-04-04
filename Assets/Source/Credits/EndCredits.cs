using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndCredits : MonoBehaviour {

	public int top;
	public int bottom;

	// Use this for initialization
	void Start () {
		StartCoroutine (CreditsUp ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator CreditsUp(){
		float allPassTime = 20.0f;
		float passedTime = 0f;


		while(true){
			float percentage = passedTime/allPassTime;
			percentage = percentage > 1.0f ? 1 : percentage;

			float position = Mathf.Lerp (top, bottom, percentage);

			this.GetComponent<RectTransform>().localPosition = new Vector3 (0f, position, 0f);

			if (percentage == 1.0f) {
				break;
			}

			passedTime += Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}


		//20초에 걸쳐서 로드 된다고 해보자 

		yield return new WaitForSeconds (1.5f);

		GameObject.FindObjectOfType<SceneChanger> ().StageLoad ("Title");

		yield return 0;
	}


}
