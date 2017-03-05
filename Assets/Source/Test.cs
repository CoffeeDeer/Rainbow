using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Test : MonoBehaviour {
	//Cleae Stage  씬통신 위해 static  
	private static int clear_Stage =7;
	private static int clear_Section = 4;

	// 시작은 0스이지 4부터 시작 
	static int challengeStage = 7;
	static int challengeSection = 4;

	int showStage = 0;

	public Button[] button = new Button[4];

	public Button[] upDownButton = new Button[2];

	//3,4,4,3,3,3,4
	public string[,] Title = {
		{"Stage-1_1","Stage-1_2","Stage-1_3",null},
		{"Stage-2_1","Stage-2_2","Stage-2_3","Stage-2_4"},
		{"Stage-3_1","Stage-3_2","Stage-3_3","Stage-3_4"},
		{"Stage-4_1","Stage-4_2","Stage-4_3"   ,null},
		{"Stage-5_1","Stage-5_2","Stage-5_3",null},
		{"Stage-6_1","Stage-6_2","Stage-6_3",null},
		{"Stage-7_1","Stage-7_2","Stage-7_3","Stage-7_4"},
	};	// Use this for initialization
	void Start () {

		//스테이지 클리어 적용 
		if (clear_Stage == challengeStage && clear_Section == challengeSection) {
			challengeSection++;

			//if end section clear 
			if (challengeSection > 4 || (challengeSection == 4 && Title [challengeStage - 1, challengeSection - 1] == null)) {
				challengeSection = 1;
				challengeStage += 1;

				//섹션이 없그레이드 되엇을때 이동
				GameObject.FindObjectOfType<TitleRuMove> ().StartRuMoveRoutine (challengeStage - 1, true);
			} else {
				GameObject.FindObjectOfType<TitleRuMove> ().StartRuMoveRoutine (challengeStage, false);
			}

		} else {
			GameObject.FindObjectOfType<TitleRuMove> ().StartRuMoveRoutine (challengeStage, false);
		}


		showStage = challengeStage;

		//end 7-4 clear
		if(showStage > 7){
			showStage = 7;
		}

		UpdateStageSelectButton (showStage);
	}

	public int a;
	public int b;
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.Space)) {
			ClearStageUpdate (a, b);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		*/
	}

	void UpdateStageSelectButton(int ShowStage){

		//if this val is false  ,each Stage button Event is null 
		bool stageEventUpdate = true;

		//over
		if (ShowStage > challengeStage){
			stageEventUpdate = false;
		}

		for (int i = 0; i < 4; i++) {

			//Text Change
			if (Title [ShowStage - 1, i] != null) {
				button[i].GetComponentInChildren<Text>().text = "      "+ShowStage.ToString() + " - " + (i+1).ToString();
			}
			else{
				button[i].GetComponentInChildren<Text>().text = "";
			}

			//Remove Listener
			button [i].onClick.RemoveAllListeners ();


			if (stageEventUpdate) {
				string temp = Title[showStage-1,i];

				button [i].onClick.AddListener (delegate {
					Debug.Log("Stage "+ temp + "Load");
				});
			}

			if (i == challengeSection - 1 && showStage == challengeStage) {
				button[i].GetComponentInChildren<Text>().text += "  new!";
				stageEventUpdate = false;
			}

		}
	}


	public void showStageChange(bool isLeft){

		if (isLeft) {
			if (showStage > 1)
				showStage--;
		}
		if (!isLeft) {			
			if (showStage <7  && showStage < challengeStage)
				showStage++;
		}

		UpdateStageSelectButton (showStage);
	}

	public static void ClearStageUpdate(int stage, int section){
		clear_Stage = stage;
		clear_Section = section;
	}
}
