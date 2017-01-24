using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BlockObject : MonoBehaviour {

	public enum BlockType{
		NULL = 0,
		StraightBlock = 1,
		PointBlock = 2,
		TurnBlock = 3,
		FixedBlock = 4

	};

	protected readonly BlockType blockType;

	[ShowOnly]public string blockTypeText;
	[ShowOnly]public int blockDirectionCode;
	[ShowOnly]public int isMovableCode;

	protected BlockObject(BlockType type){
		blockType = type;
		blockTypeText = type.ToString ();
	}		

	protected void UpdateBlockDirectionCode(){
		float temp = this.transform.localEulerAngles.z;

		if (temp < 0) {
			while (temp < 0) {
				temp += 360.0f;
			}
		}
		temp = temp % 360;

		int direction = Mathf.RoundToInt(temp/90);
		direction = direction == 4 ? 0 : direction;	
		this.blockDirectionCode = direction;
	}

	public abstract BlockCode GetBlockCode ();

	void OnMouseDown(){
		

	}

	protected BlockCode MakeBlockCode(BlockCode Q1){
		for (int i = 0; i < this.blockDirectionCode; i++) {
			Q1.RotateAsLeft();
		}	

		return Q1;
	}
}
