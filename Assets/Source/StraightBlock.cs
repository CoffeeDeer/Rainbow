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

	public override int GetBlockCode ()
	{
		//기본 형태 0 , 3 , 6 , 9
		Queue<int> Q1 = new Queue<int>(new[] { 1, 0, 1, 0 });
		int blockCode = base.MakeBlockCode (Q1);	
		return blockCode;
	}
}
