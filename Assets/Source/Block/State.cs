using UnityEngine;
using System.Collections;

namespace blockState{

	public enum Direction{
		Horizen =1,
		Vertical =2,
		free = 3
	}

	public abstract class State {
		abstract public State Update(Vector3 minusVector,Transform form); 
		abstract public State MouseUp(Vector3 vec,Transform form);
		virtual public string GetState(){
			return this.GetType ().Name;
		}

		virtual protected int calcQuadrant(Vector3 minusVector){

			float rad = Mathf.Atan2 (minusVector.x, minusVector.y);
			rad = Mathf.Rad2Deg * rad;

			if (rad < 0)
				rad = 360 + rad;

			int quadrant = 0;

			for(int i = 0 ; i< 5; i++){
				//if(rad >= (i*90)-45 && rad < (i*90)+45 )
				if(rad >= (i*90) && rad < (i*90)+90)
				{
					quadrant = i;		
					break;
				}
			}	
			quadrant = (quadrant == 4 ? 0 : quadrant);	

			return quadrant;
		}
		//리셋 함수가 추가될수 있음 
	}


	public class NoInputState : blockState.State{
		float passedfTime;
		bool LongClick;
		Direction myDir;
		GameObject blockMan;

		public NoInputState(Direction _myDir){
			myDir = _myDir;
			passedfTime = .0f;
			LongClick = false;
		}

		public override State Update(Vector3 minusVector,Transform form)
		{		

			passedfTime += Time.deltaTime;



			float Mag = Vector3.Magnitude (minusVector);
		


			if (Mag > 40) {

				switch(myDir){
				case Direction.Horizen:
					return new MovingState(Direction.Horizen);						

				case Direction.Vertical:
					return new MovingState(Direction.Vertical);
			
				case Direction.free:
					if(calcQuadrant(minusVector)%2 == 0)
						return new MovingState(Direction.Vertical);
					else
						return new MovingState(Direction.Horizen);

				}

			
			}

			//롱클릭 
			else if (passedfTime >= 3 && LongClick == false) {
				LongClick = true;
				//Debug.Log("longClick");
			}
			return this;
		}


		public override State MouseUp(Vector3 vec,Transform form){

			//숏클릭
			if (passedfTime < 1) {
				BlockManager blockManager = GameObject.Find ("BlockManager").GetComponent<BlockManager>();
				blockManager.GetMoveGuideMap (form.position);
			}

			return new NoInputState(myDir);
		}
	}

	public class MovingState : State{	

		bool nowMoving;
		Vector3 DirectionVec = Vector3.zero;
		float arriveRange = 6.0f;
		Direction myDir;

		public MovingState(Direction _Dir)
		{
			myDir = _Dir;
			nowMoving = true;
		}

		public override State Update(Vector3 minusVector,Transform form)
		{
			float SpecificMag = 0;

			if (myDir == Direction.Vertical && calcQuadrant(minusVector)%2 == 0) {
				SpecificMag = minusVector.y;
				if (SpecificMag >= 0)
					DirectionVec = Vector3.up;
				else
					DirectionVec = Vector3.down;
			} else if (myDir == Direction.Horizen && calcQuadrant(minusVector)%2 == 1){
				SpecificMag = minusVector.x;
				if(SpecificMag >= 0 )
					DirectionVec = Vector3.right;
				else
					DirectionVec = Vector3.left;
			}

			nowMoving = true;
			if (Mathf.Abs (SpecificMag) < arriveRange) {
				nowMoving = false;
			}

			if (nowMoving) {	

				form.Translate (DirectionVec * Time.deltaTime * 1.5f, Space.World);		


			}
			return this;
		}

		public override State MouseUp(Vector3 vec,Transform form){
			return new NoInputState(myDir);
		}
	}



}