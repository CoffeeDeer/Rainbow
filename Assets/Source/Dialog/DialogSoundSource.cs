using UnityEngine;
using System.Collections;

public class DialogSoundSource : MonoBehaviour {

	public AudioSource BGMsource;

	public AudioClip s1;
	public AudioClip s2;
	public AudioClip s3;
	public AudioClip s4;
	public AudioClip Prologue;
	public AudioClip Epilogue;

	// Use this for initialization
	void Start () {
		SaveData.STAGEDATA temp = DialogSceneLoad.getFormData;
		switch(temp.stage){
		case 0:
		case 1:
			BGMsource.clip = s1;
			break;
		case 2:
		case 3:
			BGMsource.clip = s2;
			break;
		case 4:
		case 5:
			BGMsource.clip = s3;
			break;
		case 6:
			BGMsource.clip = s4;
			break;
		case 7:
			BGMsource.clip = Prologue;
			break;
		case 8:
			BGMsource.clip = Epilogue;
			break;
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
