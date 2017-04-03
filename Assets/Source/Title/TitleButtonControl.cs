using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleButtonControl : MonoBehaviour {

	public Button newStartButton;
	public Button continueButton;

	// Use this for initialization
	void Start () {
	
		bool isSaveDataExist = new SaveManager ().checkSaveData ();
		if (isSaveDataExist == false) {
			continueButton.interactable = false;
		}

		newStartButton.onClick.AddListener(delegate{
			newStartButton.interactable = false;
			DialogSceneLoad.UpdataFromStageData(7,0);
			GameObject.FindObjectOfType<SceneChanger>().StageLoad("DialogScene");

			new SaveManager().SavePlayData(new SaveData());
		});


		continueButton.onClick.AddListener(delegate{
			continueButton.interactable = false;
			GameObject.FindObjectOfType<SceneChanger>().StageLoad("MapSelect");
		});

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
