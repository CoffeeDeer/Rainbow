using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DialogSceneLoad : MonoBehaviour {

	private static SaveData.STAGEDATA fromData;

	public static SaveData.STAGEDATA getFormData{
		get{ 
			return fromData;
		}
	}

	public Sprite[] startImageList;
	public Sprite[] endImageList;

	public SceneChanger sceneChanger;
	public Image viewImage;
	public Text viewText;


	// Use this for initialization
	void Start () {

		SaveManager saveManager = new SaveManager ();
		SaveData data = saveManager.LoadPlayData ();
		Debug.Log ("form is " + fromData);
		if(fromData.section == 0)
			viewImage.sprite = startImageList [fromData.stage];
		else 
			viewImage.sprite = endImageList [fromData.stage];

		viewImage.color = new Color (1.0f, 1.0f, 1.0f);
		data.clearDataArray [fromData.stage, fromData.section] = true;
		saveManager.SavePlayData (data);
		SceneTextLoad ();
			

	}

	// Update is called once per frame
	void Update () {}

	public static void UpdataFromStageData(int Stage, int Section){
		Debug.Log ("dia " + Stage + " " + Section);
		fromData.SetValue (Stage, Section);
	}

	public void textPanelOnOff(GameObject Panel){
		if (Panel.activeInHierarchy) {
			Panel.SetActive (false);
		} else {
			Panel.SetActive (true);
		}
	} 

	void SceneTextLoad(){
		int code = fromData.stage * 10 + fromData.section;

		string dialogText = new DialogXmlReading ().getDialog(code);

		viewText.text = dialogText;

	}
}
