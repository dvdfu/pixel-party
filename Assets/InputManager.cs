using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	Vector3 mousePosition;
	float xMovement;
	float yMovement;
	Stage stageScript;
	

	// Use this for initialization
	void Start () {
		stageScript = gameObject.GetComponent<Stage>();

		xMovement = 0;
		yMovement = 0;
		mousePosition = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("asdasdasdas");
		
		if(Input.GetMouseButton(0)){
			if(mousePosition == new Vector3()){
				mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
			Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mousePosition;
			Debug.Log(difference);
			
			xMovement += difference.x;
			yMovement += difference.y;

			Vector3 currentPos = stageScript.cluster.transform.localPosition;
			
			if(Mathf.Abs(xMovement) > stageScript.cellSize){

				float leftBoundary = stageScript.CoordToPos(0,0).x;
				float rightBoundary = stageScript.CoordToPos(stageScript.gridSize,0).x;
				
				if(xMovement < 0){
					if(currentPos.x - stageScript.cellSize >= leftBoundary){
						stageScript.cluster.transform.localPosition = new Vector2(currentPos.x - stageScript.cellSize, currentPos.y);
						xMovement += stageScript.cellSize;
					}
				}else{
					if(currentPos.x + stageScript.cellSize <= rightBoundary){
						stageScript.cluster.transform.localPosition = new Vector2(currentPos.x + stageScript.cellSize, currentPos.y);
						xMovement -= stageScript.cellSize;
					}
				}
				
				//xMovement = 0;
			}
			
			if(Mathf.Abs(yMovement) > stageScript.cellSize){

				float topBoundary = stageScript.CoordToPos(0,stageScript.gridSize).y;
				float bottomBoundary = stageScript.CoordToPos(0,0).y;
				
				if(yMovement < 0){
					if( currentPos.y - stageScript.cellSize >= bottomBoundary){
						stageScript.cluster.transform.localPosition = new Vector2(currentPos.x, currentPos.y - stageScript.cellSize);
						yMovement += stageScript.cellSize;
					}
				}else{
					if( currentPos.y + stageScript.cellSize <= topBoundary){
						stageScript.cluster.transform.localPosition = new Vector2(currentPos.x, currentPos.y + stageScript.cellSize);
						yMovement -= stageScript.cellSize;
					}
				}
				
				//yMovement = 0;
				
			}
			
			
			
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			//Debug.Log(mousePosition);
		}
		else{
			mousePosition = new Vector3();
		}
	
	}
}
