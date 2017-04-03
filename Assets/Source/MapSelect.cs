using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public class MapSelect : MonoBehaviour {
	//Cleae Stage  씬통신 위해 static  
	int clear_Stage;
	int clear_Section;

	// 시작은 0스이지 4부터 시작 
	int challengeStage;
	int challengeSection;

	int showStage = 0;

	//3,4,4,3,3,3,4
	public static readonly int[] stageSize = {3,4,4,3,3,3,4};

	public Button[] button = new Button[4];
	public Button[] upDownButton = new Button[2];


	bool[,] dialogSceneSkipArray;

	SaveData savedata;

	void Start () {
		SaveManager saveManager = new SaveManager();

		/*
		saveManager.updateChallengeData (6, 3);
		saveManager.updateClearData (6, 3);
		*/

		savedata = saveManager.LoadPlayData ();

		Debug.Log (savedata.challengeStageData + "   " + savedata.nowClearStageData);
	
		challengeStage = savedata.challengeStageData.stage;
		challengeSection = savedata.challengeStageData.section; 
		clear_Stage = savedata.nowClearStageData.stage;
		clear_Section = savedata.nowClearStageData.section;


		//스테이지 클리어 적용 
		if (clear_Stage == challengeStage && clear_Section == challengeSection) {
			challengeSection++;

			//if end section clear 
			if (challengeStage == -1 || challengeSection >= stageSize[challengeStage] ){//|| (Title [challengeStage, challengeSection] == null)) {
				challengeSection = 0;
				challengeStage += 1;			
				//섹션이 없그레이드 되엇을때 이동
				GameObject.FindObjectOfType<TitleRuMove> ().StartRuMoveRoutine (challengeStage, true);
			} else {
				GameObject.FindObjectOfType<TitleRuMove> ().StartRuMoveRoutine (challengeStage+1, false);
			}

			saveManager.updateChallengeData (challengeStage, challengeSection);

		} else {
			GameObject.FindObjectOfType<TitleRuMove> ().StartRuMoveRoutine (challengeStage+1, false);
		}


		showStage = challengeStage;

		//end 7-4 clear
		if(showStage > 6){
			showStage = 6;
			//epilogue 
			//if(!savedata.clearDataArray[8,0])
			{
				StartCoroutine (endCoroutine ());
			}
		}

		UpdateStageSelectButton (showStage);
	}


	// Update is called once per frame
	void Update () {
	}

	void UpdateStageSelectButton(int ShowStage){



		//if this val is false  ,each Stage button Event is null 
		bool stageEventUpdate = true;

		//over
		if (ShowStage > challengeStage){
			stageEventUpdate = false;
		}
		Debug.Log (showStage);
		for (int i = 0; i < 4; i++) {
			//Text Change
			if (i < stageSize[showStage]) {
				button[i].GetComponentInChildren<Text>().text = "      "+(ShowStage+1).ToString() + " - " + (i+1).ToString();
			}
			else{
				button[i].GetComponentInChildren<Text>().text = "";
				stageEventUpdate = false;
			}

			//Remove Listener
			button [i].onClick.RemoveAllListeners ();


			if (stageEventUpdate) {
				
				SaveData.STAGEDATA stageData = new SaveData.STAGEDATA();
				stageData.SetValue(showStage,i);

				//리스너 등록
				button [i].onClick.AddListener (delegate {
					Debug.Log(savedata.clearDataArray[stageData.stage,stageData.section]);
					if(!savedata.clearDataArray[stageData.stage,stageData.section] && stageData.section == 0){	
						Debug.Log(stageData);
						DialogSceneLoad.UpdataFromStageData(stageData.stage,stageData.section);
						GameObject.FindObjectOfType<SceneChanger>().StageLoad("DialogScene");
					}
					else{
						GameObject.FindObjectOfType<SceneChanger>().StageLoad(stageData);
					}
				});
				 
			}

			if (i == challengeSection && showStage == challengeStage) {
				stageEventUpdate = false;
				button[i].GetComponentInChildren<Text>().text += "  new!";
			}

		}
	}

	public void showStageChange(bool isLeft){

		if (isLeft) {
			if (showStage > 0)
				showStage--;
		}
		if (!isLeft) {			
			if (showStage <6  && showStage < challengeStage)
				showStage++;
		}

		UpdateStageSelectButton (showStage);
	}		

	//jump endDialog
	private IEnumerator endCoroutine(){

		Image clickBlock = GameObject.Find ("ClickBlock").GetComponent<Image>();
		clickBlock.enabled = true;

		yield return new WaitForSeconds (3.0f);

		DialogSceneLoad.UpdataFromStageData (8, 0);
		GameObject.FindObjectOfType<SceneChanger> ().StageLoad ("DialogScene");
		yield return 0;

	}

}
