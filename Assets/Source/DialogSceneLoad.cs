using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogSceneLoad : MonoBehaviour {
	private static int challengeStage;
	private static int challengeSection;
	private static bool isLoadedFromMap;

	//3,4,4,3,3,3,4
	private string[,] Title = {
		{"Stage-1_1","Stage-1_2","Stage-1_3",null},
		{"Stage-2_1","Stage-2_2","Stage-2_3","Stage-2_4"},
		{"Stage-3_1","Stage-3_2","Stage-3_3","Stage-3_4"},
		{"Stage-4_1","Stage-4_2","Stage-4_3"   ,null},
		{"Stage-5_1","Stage-5_2","Stage-5_3",null},
		{"Stage-6_1","Stage-6_2","Stage-6_3",null},
		{"Stage-7_1","Stage-7_2","Stage-7_3","Stage-7_4"},
	};	// Use this for initialization

	// Use this for initialization
	void Start () {

		Debug.Log (isLoadedFromMap);
		Debug.Log (challengeStage + "  " + challengeSection);

		if (isLoadedFromMap) {
			//스테이지로 가야함 
			Debug.Log ("to Stage");
			//SceneManager.LoadScene ();
		} else {
			Debug.Log ((challengeStage-1) +"  "+ (challengeSection-1));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void LoadDialogFromMap(MapSelect.StageData stageData){
		challengeStage = stageData.stage;
		challengeSection = stageData.section;

		isLoadedFromMap = true;
	}

	public static void LoadDialogFromSection(MapSelect.StageData stageData){
		challengeStage = stageData.stage;
		challengeSection = stageData.section;

		isLoadedFromMap = false;
	}

}
