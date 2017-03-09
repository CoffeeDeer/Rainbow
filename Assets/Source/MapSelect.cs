﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public class MapSelect : MonoBehaviour {
	//Cleae Stage  씬통신 위해 static  
	private static int clear_Stage =1;
	private static int clear_Section = 2;

	// 시작은 0스이지 4부터 시작 
	static int challengeStage = 1;
	static int challengeSection = 3;

	int showStage = 0;

	public Button[] button = new Button[4];

	public Button[] upDownButton = new Button[2];

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

	void Start () {

		//LoadStageData ();

		//Debug.Log (challengeStage +" "+challengeSection);
		//스테이지 클리어 적용 
		if (clear_Stage == challengeStage && clear_Section == challengeSection) {
			challengeSection++;

			//if end section clear 
			if (challengeSection > 4 || (challengeSection == 4 && Title [challengeStage - 1, challengeSection - 1] == null)) {
				challengeSection = 1;
				challengeStage += 1;

				//SaveStageData ();

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
				StageData stageData = new StageData (showStage - 1, i);

				//리스너 등록
				button [i].onClick.AddListener (delegate {
					if(stageData.section == 0 ){
						//DialogSceneLoad.LoadDialogFromMap(stageData);
						//SceneManager.LoadScene("DialogScene");
					}
					else{
						Debug.Log("sss  "+ temp);	
					}
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

	private void SaveStageData(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/SaveData.dat");
		StageData data = new StageData (challengeStage, challengeSection);

		bf.Serialize (file, data);
		file.Close ();

		Debug.Log ("Save Data");
	}

	private void LoadStageData(){
		if(File.Exists(Application.persistentDataPath+"/SaveData.dat")){
			Debug.Log ("Load Data");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
			StageData data = (StageData)bf.Deserialize(file);

			file.Close();
			challengeStage = data.stage;
			challengeSection = data.section;
		}
	}

	[System.Serializable]
	public struct StageData{
		public int stage;
		public int section;
		public StageData(int Stage,int Section){
			stage=Stage;
			section=Section;
		}
	}
}
