using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public AudioSource BgmSource;
	public AudioSource DragSource;

	public AudioClip Menu_Click;
	public AudioClip CantMoveThere;


	// Use this for initialization
	void Start () {
		BgmSource.loop = true;
		BgmSource.Play();	

		DragSource.loop = true;
		DragSource.Stop ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Menu_ClickSoundPlay()
	{
		//SfxSource.PlayOneShot (Menu_Click);
	}

	public void Block_DragSound_play(bool value)
	{
		if (value == true) {
			if (DragSource.isPlaying == false) {
				DragSource.Play ();
			}
		} else {
			DragSource.Stop ();
		}
	}
}
