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
					return new MovingState(Direction.Horizen,false);						

				case Direction.Vertical:
					return new MovingState(Direction.Vertical,false);
			
				case Direction.free:
					{
						if (calcQuadrant (minusVector) % 2 == 0)
							return new MovingState (Direction.Vertical,true);
						else
							return new MovingState (Direction.Horizen,true);
					}

				}			
			}

			//롱클릭 
			else if (passedfTime >= 0.7f && LongClick == false) {
				LongClick = true;
				if(form.gameObject.GetComponent<CubeControl> () != null)
					form.gameObject.GetComponent<CubeControl> ().LongClickEvent.Invoke ();
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
		bool preMoving = false;

		bool FreeMove;

		GameObject audioManger;

		Vector3 DirectionVec = Vector3.zero;
		float arriveRange = 6.0f;
		Direction myDir;

		public MovingState(Direction _Dir,bool isFree)
		{
			myDir = _Dir;
			nowMoving = false;

			FreeMove = isFree;

			audioManger = GameObject.Find ("SoundManager");
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

			preMoving = nowMoving;

			nowMoving = true;
			if (Mathf.Abs (SpecificMag) < arriveRange) {
				nowMoving = false;
				if (preMoving) {
					audioManger.SendMessage ("Block_DragSound_play", false, SendMessageOptions.DontRequireReceiver);
				}
			}

			if (nowMoving) {				
				form.Translate (DirectionVec * Time.deltaTime * 1.5f, Space.World);
				if (!preMoving) {
					audioManger.SendMessage ("Block_DragSound_play", true, SendMessageOptions.DontRequireReceiver);
				}
			}

		
			return this;
		}

		public override State MouseUp(Vector3 vec,Transform form){
			audioManger.SendMessage ("Block_DragSound_play", false, SendMessageOptions.DontRequireReceiver);
			if (FreeMove)
				myDir = Direction.free;

			return new NoInputState(myDir);
		}
	}



}