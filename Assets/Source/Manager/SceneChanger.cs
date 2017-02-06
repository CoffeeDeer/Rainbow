using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
		fadeoutImage.enabled = true;
		Color color = fadeoutImage.color;

		for (int i = 0; i < loop; i++) {
			color.a = (i+1) / (float)loop;
			fadeoutImage.color = color;
			yield return new WaitForSeconds(0.03f);
		}

	}

	public void SceneReload(){	
		int index = SceneManager.GetActiveScene ().buildIndex;	
		SceneManager.LoadScene (index);
	}

	public void MapSceneLoad(){
		SceneManager.LoadScene ("Title");
	}


	public void MapSceneLoadDelay(){
		StartCoroutine (TitleSceneload ());
	}

	private IEnumerator TitleSceneload(){
		yield return new WaitForSeconds (2.0f);
		MapSceneLoad ();
		yield return 0;

	}
}
