using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	int[,] moveGuideMap;
	Coroutine movingRoutine = null;

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

			//because player is moving has distancediff
			if (distancediff.magnitude > 0.10f) {
				//reset for 3frame
				for (int i = 0; i < 3; i++) {
					Vector3 temp = distancediff / 3;
					transform.Translate (temp, Space.World);
					yield return new WaitForSeconds (0.03f);
				}
			}


			if(moveGuideMap [xPos, yPos] == 0     || moveGuideMap [xPos, yPos] == -1)
			{
				Debug.Log("arrive");
				break;				 
			}

			//transform.position = new Vector3 (xPos, yPos, transform.position.z);

			float deltatime;
			Vector3 direction = Vector3.zero;

			switch (moveGuideMap [xPos, yPos]) {		
			case 1:
				direction = Vector3.down;
				//PlayerTex.transform.localEulerAngles = new Vector3(-0.1f,87,270);
				break;
			case 2:
				direction = Vector3.right;
				//PlayerTex.transform.localEulerAngles = new Vector3(0.87f,267,449);
				break;
			case 3:
				direction = Vector3.up;
				//PlayerTex.transform.localEulerAngles = new Vector3(-90,-1.1f,360);
				break;
			case 4:
				direction = Vector3.left;
				//PlayerTex.transform.localEulerAngles = new Vector3(90,178,360);
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
				yield return new WaitForSeconds (Time.deltaTime);
			}

			xPos = Mathf.RoundToInt (transform.position.x);
			yPos = Mathf.RoundToInt (transform.position.y);

			transform.position = new Vector3 (xPos, yPos, transform.position.z);
		}

		//nowMoving = false;
		//blockMan.SendMessage("SetNowUse",true,SendMessageOptions.DontRequireReceiver);

		yield return 0;
	}
}
