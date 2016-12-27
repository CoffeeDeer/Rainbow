using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;



public class BlockManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private BlockCode[,] GetBlockMap(){


		BlockCode[,] arr = new BlockCode[5, 5];
		//arr initialize
		for (int i = 0; i < arr.GetLength (0); i++) {
			for (int j = 0; j < arr.GetLength (1); j++) {
				arr[i,j] = new BlockCode();
			}
		}

		//get blockCode
		Transform map = GameObject.Find ("Map").transform;
	
		foreach(Transform child in map){
			Vector3 localPos = child.transform.localPosition;
			int x = Mathf.RoundToInt (localPos.x);
			int y = Mathf.RoundToInt (localPos.y);

			BlockObject childComp = child.GetComponent<BlockObject> ();
			try{
				arr [x, y] = childComp.GetBlockCode();
			}catch(IndexOutOfRangeException e){
				Debug.Log ("Error " + e.Message);
			}
		}


		for (int i = 0; i < arr.GetLength(0); i++) {
			string temp = "";
			for (int j = 0; j < 5; j++) {
				temp += arr[i,j].ToString() + "\t";
			}
			Debug.Log (temp);
		}

		return arr;
	}

	public int[,] MakeMovableMap(){
		
		GameObject.Find ("Player");
		Vector3 PlayerPos = new Vector3 (0, 0, 0);
		BlockCode[,] blockMap = GetBlockMap ();

		int[,] movableMap = FindMovableMapAlg (blockMap, PlayerPos);

		return movableMap;		
	}

	private int[,] FindMovableMapAlg(BlockCode[,] blockMap,Vector3 PlayerPos){

		//initialize;
		int[,] movableMap = new int[blockMap.GetLength(0),blockMap.GetLength(1)];
		bool[,] visitMap = new bool[blockMap.GetLength(0),blockMap.GetLength(1)];
		Array.Clear (movableMap, 0, movableMap.Length);
		Array.Clear (visitMap, 0, movableMap.Length);

		Queue<KeyValuePair<int,int>> Q1 = new Queue<KeyValuePair<int,int>>();
		 
		int playerXpos = Mathf.RoundToInt (PlayerPos.x);
		int playerYpos = Mathf.RoundToInt (PlayerPos.y);
		Q1.Enqueue(new KeyValuePair<int,int>(playerXpos , playerYpos));
		visitMap [playerXpos, playerYpos] = true;

		while (Q1.Count != 0) {
			KeyValuePair<int,int> temp = Q1.Dequeue ();
			int key = temp.Key;
			int value = temp.Value;
			for (int i = 0; i < 4; i++) {
				if (blockMap [temp.Key, temp.Value].GetCodeByIndex (i) == '1') {
					switch (i) {
					case 0:
						value += 1;
						break;
					case 1:
						key += 1;
						break;
					case 2:
						value -= 1;
						break;
					case 3:
						key -= 1;
						break;
					}
				}
				KeyValuePair<int,int> newKey = new KeyValuePair<int, int>(key,value);

				//newkeyPos in BlockMap? and 
				if (newKey.Key >=0 && newKey.Value >=0 &&
					newKey.Key <blockMap.GetLength (0) && newKey.Value <blockMap.GetLength (1)){
					if (visitMap [newKey.Key, newKey.Value] == true)
						continue;

					if (blockMap [newKey.Key, newKey.Value].GetCodeByIndex ((i + 2) % 4) == '1') {
						visitMap [newKey.Key, newKey.Value] = true;
						Q1.Enqueue (newKey);
					}
				}
			}
		}


		for (int i = 0; i < visitMap.GetLength(0); i++) {
			string temp = "";
			for (int j = 0; j < visitMap.GetLength(1); j++) {
				temp += visitMap[i,j] + "\t";
			}
			Debug.Log (temp);
		}


		return movableMap;
	}

}
