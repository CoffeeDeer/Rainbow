using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CubeControl : MonoBehaviour, IPointerUpHandler,IPointerDownHandler {

	public bool VerticalMove;
	public bool HorizenMove;

	public GameObject parents;
	public UnityEvent DragEvent;

	blockState.State myStatus;
	bool Mouseflag = false;

	Camera myCamera;

	void Start () {
		myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		int temp = 0;

		if (HorizenMove == true)
			temp += 1;
		if (VerticalMove == true)
			temp += 2;

		blockState.Direction direc = (blockState.Direction)temp;
		myStatus = new blockState.NoInputState(direc);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Mouseflag == true) {		

			if(myStatus.GetState()=="MovingState"){
				DragEvent.Invoke();			
			}

			Vector3 minusVector = Input.mousePosition - myCamera.WorldToScreenPoint(transform.position);

			GameObject center = this.gameObject;
			if (parents != null)
				center = parents.gameObject;
						
			myStatus = myStatus.Update(minusVector,center.transform);

		}

	}

	public void OnPointerUp(PointerEventData e)
	{
		if (Mouseflag == false)
			return;

		myStatus = myStatus.MouseUp(transform.position,this.transform);
		Mouseflag = false;	

		GameObject center = this.gameObject;

		if (parents != null) {
			center = parents.gameObject;
		}

		center.GetComponent<ResetPosOnly>().StartResetPosCoroutine();
		center.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;	


	}

	public void OnPointerDown(PointerEventData e)
	{		
		Mouseflag = true;

		GameObject center = this.gameObject;

		if(parents != null) 
			center = parents.gameObject;	

		center.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;	
		
	}


}




