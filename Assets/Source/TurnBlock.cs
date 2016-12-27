using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnBlock : BlockObject {

	const BlockObject.BlockType type = BlockType.TurnBlock;

	// Use this for initialization
	void Start () {
		base.UpdateBlockDirectionCode();
	}

	TurnBlock() :base(type){

	}

	// Update is called once per frame
	void Update () {

	}

	public override BlockCode GetBlockCode ()
	{
		//기본 형태 0 , 3 , 6 , 9
		BlockCode Q1 = new BlockCode('1','0','0','1');
		BlockCode blockCode = base.MakeBlockCode (Q1);	
		return blockCode;
	}
}
