using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class SaveData {
	public bool[,] clearDataArray;

	public STAGEDATA nowClearStageData;
	public STAGEDATA challengeStageData;

	public SaveData(){
		nowClearStageData.initialize ();
		challengeStageData.initialize();

		clearDataArray = new bool[10, 5];
		Array.Clear (clearDataArray, 0, clearDataArray.Length);
	}

	[System.Serializable]
	public struct STAGEDATA{
		public int stage;
		public int section;

		public void initialize(){
			stage = -1;
			section = -1;
		}

		public void SetValue(int Stage,int Section){
			stage = Stage;
			section = Section;
		}

		public override string ToString ()
		{
			return stage + " " + section;
		}
	}

	public override string ToString ()
	{
		return "clear "+nowClearStageData.ToString () + " / chall " + challengeStageData.ToString ();
	}
		
}
