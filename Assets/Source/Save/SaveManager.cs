using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public class SaveManager{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SavePlayData(SaveData data){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/SaveData.dat");

		bf.Serialize (file, data);
		file.Close ();

		//Debug.Log ("Save Data");
	}

	public SaveData LoadPlayData(){
		//Debug.Log (Application.persistentDataPath);
		if (File.Exists (Application.persistentDataPath + "/SaveData.dat")) {
			
			///Debug.Log ("Load Data");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize (file);

			file.Close ();

			return data;
		} else {
			SaveData data = new SaveData ();
			return data;
		}
	}

	public void updateChallengeData (int Stage, int Section)
	{
		SaveData data = LoadPlayData ();
		if (data == null) {
			Debug.LogError ("NO Save Data");
			return;
		}
		data.challengeStageData.SetValue (Stage,Section);

		SavePlayData (data);
	}

	public void updateClearData(int Stage,int Section){
		SaveData data = LoadPlayData ();
		if (data == null) {
			Debug.LogError ("NO Save Data");
			return;
		}
		data.nowClearStageData.SetValue (Stage,Section);

		SavePlayData (data);
	}

	public bool[,] isDialogSceneNeed(){
		SaveData temp = LoadPlayData ();


		return temp.clearDataArray;
	}

	public bool checkSaveData(){

		bool result = File.Exists (Application.persistentDataPath + "/SaveData.dat");

		return result;
	}
}