using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StraightBlock : BlockObject {
	
	const BlockObject.BlockType type = BlockType.StraightBlock;

	// Use this for initialization
	void Start () {
		base.UpdateBlockDirectionCode();
	}

	StraightBlock() :base(type){
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public override BlockCode  GetBlockCode()
	{
		//기본 형태 0 , 3 , 6 , 9
		BlockCode Q1 = new BlockCode('1','0','1','0');
		BlockCode blockCode = base.MakeBlockCode (Q1);	
		return blockCode;
	}

	public void AAAA (){
		Debug.Log ("CCC)");
	}
}
