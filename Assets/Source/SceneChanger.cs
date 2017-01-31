using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	public GameObject FadeOutBlackImage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartSceneFadeoutEffect(){
		StartCoroutine (SceneFadeOut ());
	}

	IEnumerator SceneFadeOut(){
		const int loop = 30;

		Image fadeoutImage = FadeOutBlackImage.GetComponent<Image> ();
		Color color = fadeoutImage.color;

		for (int i = 0; i < loop; i++) {
			color.a = (i+1) / (float)loop;
			fadeoutImage.color = color;
			yield return new WaitForSeconds(0.03f);
		}

	}
}
