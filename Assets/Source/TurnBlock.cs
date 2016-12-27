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

	public override int GetBlockCode ()
	{
		//기본 형태 0 , 3 , 6 , 9
		Queue<int> Q1 = new Queue<int>(new[] { 1, 0, 0, 1 });
		int blockCode = base.MakeBlockCode (Q1);	
		return blockCode;
	}
}
