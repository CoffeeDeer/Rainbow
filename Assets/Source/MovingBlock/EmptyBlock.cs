using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmptyBlock : BlockObject {

	const BlockObject.BlockType type = BlockType.NULL;

	// Use this for initialization
	void Start () {
		base.UpdateBlockDirectionCode();
	}

	EmptyBlock() :base(type){

	}

	// Update is called once per frame
	void Update () {
	}

	public override BlockCode  GetBlockCode()
	{
		//기본 형태 0 , 3 , 6 , 9
		BlockCode Q1 = new BlockCode('0','0','0','0');
		BlockCode blockCode = base.MakeBlockCode (Q1);	
		return blockCode;
	}
}
