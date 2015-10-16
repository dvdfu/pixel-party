using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster : MonoBehaviour {

	public List<Block> allBlocks;
	public Block firstBlock;
	private Stage stageScript;


	void Start () {
		stageScript = gameObject.GetComponent<Stage> ();
		allBlocks = new List<Block>();
		//Debug.Log("weeeeeeeee");

	}

	public void initializeBlock(){
		firstBlock = Instantiate (stageScript.block);
		firstBlock.transform.parent = gameObject.transform;
		firstBlock.transform.localPosition = stageScript.CoordToPos(3,3);
		firstBlock.transform.localScale = new Vector3(stageScript.cellSize, stageScript.cellSize, 1);
		allBlocks.Add(firstBlock);
	}

	void End (){
	}

	void Update (){

	}

	public void checkCollissions(List<Block> blocks){

	
	}

	public void checkCollission(Block b){
	}

	public void MoveBlocks(Vector3 newPosition){
		foreach(Block b in allBlocks){
			b.transform.position = newPosition;
		}
	}
		 

}