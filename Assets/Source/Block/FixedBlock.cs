using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FixedBlock : MonoBehaviour {

	Vector3 clickPos;
	bool MouseFlag;

	float timer;

	// Use this for initialization
	void Start () {
		MouseFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (MouseFlag == true)
			timer += Time.deltaTime;
	}

	public void OnMouseUp()
	{
		Vector3 NowPos = Input.mousePosition;
		float Mag = Vector3.Magnitude (NowPos - clickPos);

		if (timer < 1.0f && Mag < 40) {
			BlockManager blockManager = GameObject.Find ("BlockManager").GetComponent<BlockManager>();
			blockManager.GetMoveGuideMap (this.transform.position);
		}
	}

	public void OnMouseDown()
	{
		clickPos = Input.mousePosition;
		clickPos.z = 0f;

		timer = .0f;
		
		MouseFlag = true;
	}
}
