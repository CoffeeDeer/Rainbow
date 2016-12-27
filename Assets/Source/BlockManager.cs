using UnityEngine;
using System.Collections;
using System;


public class BlockManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private int[,] GetBlockMap(){
		int[,] arr = new int[5, 5];
		Array.Clear (arr, 0, arr.Length);

		Transform map = GameObject.Find ("Map").transform;
		Debug.Log ("CC");
		foreach(Transform child in map){
			Vector3 localPos = child.transform.localPosition;
			int x = Mathf.RoundToInt (localPos.x);
			int y = Mathf.RoundToInt (localPos.y);

			BlockObject childComp = child.GetComponent<BlockObject> ();
			arr [x, y] = childComp.GetBlockCode();
		}


		for (int i = 0; i < arr.GetLength(0); i++) {
			string temp = "";
			for (int j = 0; j < 5; j++) {
				temp += arr[i,j].ToString("D4") + "\t";
			}
			Debug.Log (temp);
		}

		return arr;
	}

	public int[,] MakeMovableMap(){

		GameObject.Find ("Player");

		int[,] blockMap = GetBlockMap ();

		int[,] movableMap = new int[blockMap.GetLength(0),blockMap.GetLength(1)];
		Array.Clear (movableMap, 0, movableMap.Length);

		int x = blockMap.GetLength (0);
		int y = blockMap.GetLength (1);
	
		return movableMap;		
	}
}
