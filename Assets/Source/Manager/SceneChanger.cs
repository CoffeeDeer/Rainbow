using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	public GameObject FadeOutBlackImage;

	public int stage = -1;
	public int section = -1;

	// Use this for initialization
	void Start () {	
		StartCoroutine (SceneFadeIn ());
	}
	
	// Update is called once per frame
	void Update () { }	  

	//3,4,4,3,3,3,4
	private string[,] Title = {
		{"Stage-1_1","Stage-1_2","Stage-1_3",null},
		{"Stage-2_1","Stage-2_2","Stage-2_3","Stage-2_4"},
		{"Stage-3_1","Stage-3_2","Stage-3_3","Stage-3_4"},
		{"Stage-4_1","Stage-4_2","Stage-4_3"   ,null},
		{"Stage-5_1","Stage-5_2","Stage-5_3",null},
		{"Stage-6_1","Stage-6_2","Stage-6_3",null},
		{"Stage-7_1","Stage-7_2","Stage-7_3","Stage-7_4"},
		{"Stage-AA",null,null,null},
		{"Ending_Credit",null,null,null}
	};	// Use this for initialization

	public void SceneReload(){	
		string name = SceneManager.GetActiveScene ().name;	
		StartCoroutine (SceneLoad (name));
	}

	public void StageLoad(string sceneName){
		StartCoroutine (SceneLoad (sceneName));
	}

	public void StageLoad(SaveData.STAGEDATA stagedata){
		StartCoroutine (SceneLoad (Title[stagedata.stage,stagedata.section]));
	}

	// in dialog screen  when dialog skip button clicked
	public void DialogSkipLoad(){
		SaveData.STAGEDATA aa = DialogSceneLoad.getFormData;
		//Debug.Log ("CAlled");
		//fist section
		if (aa.section == 0)
			StageLoad (Title [aa.stage, aa.section]);
		else
			MapSelectSceneLoad ();
	} 

	private IEnumerator SceneLoad(string name){
		yield return StartCoroutine (SceneFadeOut ());

		SceneManager.LoadScene (name);
	}

	public void StageEnd(){

		SaveData savedata = new SaveManager ().LoadPlayData ();

		bool[,] dialogSkipArray = savedata.clearDataArray;

		new SaveManager ().updateClearData (stage, section);

		if (!dialogSkipArray [stage, section] && MapSelect.stageSize [stage] == (section+1)) {
			DialogSceneLoad.UpdataFromStageData(stage,section);
			StageLoad("DialogScene");
		} else {
			MapSelectSceneLoad ();	
			//new SaveManager
		}
	}

	private void MapSelectSceneLoad(){
		StartCoroutine (MapSelectSceneLoadRoutine ());
	}




	private IEnumerator MapSelectSceneLoadRoutine(){
		yield return StartCoroutine (SceneFadeOut());

		if (stage == -1 || section == -1)
			Debug.LogError ("scene chagenr stage&section is -1");
		
		//MapSelect.ClearStageUpdate (stage, section);

		SceneManager.LoadScene ("MapSelect");
		yield return 0;
	}

	private IEnumerator SceneFadeOut(){
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

	private IEnumerator SceneFadeIn(){
		const int loop = 30;

		Image fadeoutImage = FadeOutBlackImage.GetComponent<Image> ();
		fadeoutImage.enabled = true;
		Color color = fadeoutImage.color;

		for (int i = 0; i < loop; i++) {
			color.a = (loop-(i+1)) / (float)loop;
			fadeoutImage.color = color;
			yield return new WaitForSeconds(0.03f);
		}

		fadeoutImage.enabled = false;
	}
		
}