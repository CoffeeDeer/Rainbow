using UnityEngine;
using System;


public class BlockCode
{
	private char[] code;

	public BlockCode (){
		code = new char[4] {'0','0','0','0'};
	}

	public BlockCode (char top,char right, char bottom,char left){
		code = new char[4] {top,right,bottom,left};
	}

	public void RotateAsLeft(){
		char temp = code [0];
		code [0] = code [1];
		code [1] = code [2];
		code [2] = code [3];
		code [3] = temp;
	}

	public override string ToString ()
	{
		string temp = "";

		foreach (char cha in code) {
			temp += cha;
		}

		return temp;
	}

	public char GetCodeByIndex(int index){
		if (index > -1 && index < 4)
			return code[index];
		return '0';
	}
		
}


