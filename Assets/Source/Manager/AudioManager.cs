using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public AudioSource BgmSource;
	public AudioSource DragSource;
	public AudioSource OneShot;

	//
	public AudioClip Menu_Click;
	//올라가면 회전 
	public AudioClip RotateBlock;
	//클리어
	public AudioClip clear;
	//
	public AudioClip HideBlock;

	// Use this for initialization
	void Start () {
		BgmSource.loop = true;
		BgmSource.Play();	

		DragSource.loop = true;
		DragSource.Stop ();

		OneShot = GameObject.Find ("OneShot").GetComponent<AudioSource> ();
		OneShot.volume = 0.8f;

		Menu_Click = (AudioClip)Resources.Load ("Sound/Menu_click");
		RotateBlock = (AudioClip)Resources.Load ("Sound/button");
		clear = (AudioClip)Resources.Load ("Sound/Clear");
		HideBlock = (AudioClip)Resources.Load ("Sound/Block_Cloak");
	}
	
	// Update is called once per frame
	void Update () {

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

	public void playMenu_Click(){
		OneShot.PlayOneShot (Menu_Click);
	}
	public void playRotateBlock(){
		OneShot.PlayOneShot (RotateBlock);
	}
	public void playclear(){
		OneShot.PlayOneShot (clear);
	}
	public void playHideBlock(){
		OneShot.PlayOneShot (HideBlock);
	}
}
