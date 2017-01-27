using UnityEngine;
using System.Collections;

public class RenderOrder : MonoBehaviour {

	public int renderOreder;
	// Use this for initialization
	void Start () {
		Renderer renderer = GetComponent<Renderer> ();
		renderer.material.renderQueue = renderOreder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
