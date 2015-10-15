using UnityEngine;
using System.Collections;

public class blockManager : MonoBehaviour {
	Stage stageScript ;
	float cellSize ;
	// Use this for initialization
	void Start () {

		stageScript = gameObject.transform.parent.gameObject.GetComponent<Stage>();
		cellSize = stageScript.cellSize;

		// instantiate it 
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
