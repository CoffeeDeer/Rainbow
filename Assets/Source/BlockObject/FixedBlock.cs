using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FixedBlock : MonoBehaviour,IPointerUpHandler,IPointerDownHandler {
	[HideInInspector] public bool IsPlayerOn;
	Vector3 clickPos;
	bool MouseFlag;

	float timer;

	// Use this for initialization
	void Start () {
		IsPlayerOn = false;
		MouseFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (MouseFlag == true)
			timer += Time.deltaTime;

		if (timer > 0.7f) {
			MouseFlag = false;
			timer = .0f;
			CubeControl temp = this.GetComponent<CubeControl> ();
			if (temp != null && IsPlayerOn == false) {
				temp.LongClickEvent.Invoke ();
			}
		}
	}

	public void OnPointerUp(PointerEventData e)
	{
		Vector3 NowPos = Input.mousePosition;
		float Mag = Vector3.Magnitude (NowPos - clickPos);

		if (timer < 0.7f && Mag < 40 && MouseFlag ==true) {
			BlockManager blockManager = GameObject.Find ("BlockManager").GetComponent<BlockManager>();
			blockManager.GetMoveGuideMap (this.transform.position);
		}

		timer = .0f;
		MouseFlag = false;

	}

	public void OnPointerDown(PointerEventData e)
	{
		clickPos = Input.mousePosition;
		clickPos.z = 0f;

		timer = .0f;
		
		MouseFlag = true;
	}
}
