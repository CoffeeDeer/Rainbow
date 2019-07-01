using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	int[,] moveGuideMap;
	Coroutine movingRoutine = null;
	public GameObject PlayerTex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveGuideMapUpdate(int[,] moveguideMap){
		moveGuideMap = moveguideMap;
		if (movingRoutine != null) {
			StopCoroutine (movingRoutine);
		}
		movingRoutine = StartCoroutine (Moving ());
	}


	//플레이어 이동 ㅁ
	IEnumerator Moving(){

		//GameObject blockMan = GameObject.Find ("BlockManager");
		//blockMan.SendMessage("SetNowUse",false,SendMessageOptions.DontRequireReceiver);

		while (true) {
			int xPos = Mathf.RoundToInt (transform.position.x);
			int yPos = Mathf.RoundToInt (transform.position.y);

			//거리의 차 
			Vector3 distancediff = new Vector3 ( (xPos - transform.position.x), (yPos - transform.position.y), .0f);

			//because player is moving has diff_distance
			if (distancediff.magnitude > 0.10f) {
				//reset Position for 3frame
				for (int i = 0; i < 3; i++) {
					Vector3 temp = distancediff / 3;
					transform.Translate (temp, Space.World);
					yield return new WaitForSeconds (0.03f);
				}
			}


			if(moveGuideMap [xPos, yPos] == 0     || moveGuideMap [xPos, yPos] == -1)
			{
				//Debug.Log("arrive");
				break;				 
			}

			//transform.position = new Vector3 (xPos, yPos, transform.position.z);

			float deltatime;
			Vector3 direction = Vector3.zero;

			switch (moveGuideMap [xPos, yPos]) {		
			case 1:
				
				direction = Vector3.down;
				PlayerTex.transform.localEulerAngles = new Vector3(88.40601f,780.903f,-119.62f);
				break;
			case 2:
				direction = Vector3.left;
				PlayerTex.transform.localEulerAngles = new Vector3(1.925f,630.744f,-270.472f);
				break;
			case 3:				
				direction = Vector3.up;
				PlayerTex.transform.localEulerAngles = new Vector3(-86.912f,642.607f,79.714f);
				break;
			case 4:
				direction = Vector3.right;
				PlayerTex.transform.localEulerAngles = new Vector3(-1.364f,809.257f,-90.479f);
				break;
			default:
				break;

			}

			deltatime = 0;
			while (true) {
				//Debug.Log (deltatime);
				if (deltatime > 0.25f)
					break;
				//Debug.Log ("movie");
				transform.Translate (4 * Time.deltaTime * direction, Space.World);	
				deltatime += Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}

			xPos = Mathf.RoundToInt (transform.position.x);
			yPos = Mathf.RoundToInt (transform.position.y);

			transform.position = new Vector3 (xPos, yPos, transform.position.z);
		}

		//nowMoving = false;
		//blockMan.SendMessage("SetNowUse",true,SendMessageOptions.DontRequireReceiver);

		yield return 0;
	}

	void OnTriggerEnter(Collider coll){

		if(coll.transform.CompareTag("TurnCubeInMultiple")){
			coll.GetComponent<FixedBlock> ().IsPlayerOn = true;
		}

		if (coll.transform.CompareTag ("MovableSingle")) {
			CubeControl ctrl = coll.gameObject.GetComponent<CubeControl> ();
			if (ctrl != null) {
				ctrl.enabled = false;
			}
		}

		else if (coll.transform.CompareTag ("MovableMulti")) {
			
			Transform form = coll.transform;
			foreach (Transform k in form) {				
				CubeControl ctrl = k.GetComponent<CubeControl> ();
				if (ctrl != null) {
					ctrl.enabled = false;
					k.gameObject.GetComponent<FixedBlock> ().enabled = true;				
				}
			}
		}
	}

	void OnTriggerExit(Collider coll){

		if(coll.transform.CompareTag("TurnCubeInMultiple")){
			coll.GetComponent<FixedBlock> ().IsPlayerOn = false;
		}

		if (coll.transform.CompareTag ("MovableSingle")) {
			CubeControl ctrl = coll.gameObject.GetComponent<CubeControl> ();
			if (ctrl != null) {
				ctrl.enabled = true;
			}
		}

		else if (coll.transform.CompareTag ("MovableMulti")) {
			Transform form = coll.transform;
			foreach (Transform k in form) {
				CubeControl ctrl = k.GetComponent<CubeControl> ();
				if (ctrl != null) {
					ctrl.enabled = true;
					k.gameObject.GetComponent<FixedBlock> ().enabled = false;	
				}
			}
		}
	}
}
