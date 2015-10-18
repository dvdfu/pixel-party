using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster : MonoBehaviour {

	public List<Block> allBlocks;
	public Block firstBlock;
	private Stage stageScript;


	void Start () {
		stageScript = gameObject.GetComponent<Stage> ();
		if(allBlocks == null){
			//allBlocks = new List<Block>();
		}

		//Debug.Log("weeeeeeeee");

	}

	public void initializeBlock(Stage script){
		firstBlock = Instantiate (script.block);
		firstBlock.transform.parent = gameObject.transform;
		firstBlock.transform.localPosition = script.CoordToPos(3,3);
		firstBlock.transform.localScale = new Vector3(script.cellSize, script.cellSize, 1);
		allBlocks = new List<Block>();
		allBlocks.Add(firstBlock);

	}

	void End (){
	}

	void Update (){

	}

	public void checkCollissions(List<Block> blocks){

	}

	public List<Block> CheckCollision(List<Block> blocks){
		List<Block> newBlocks = new List<Block>();
		foreach(Block b in blocks){
			foreach(Block clusterBlock in allBlocks){
				// If collide from top.
				Vector3 position = b.gameObject.transform.position;
				
				if((Mathf.Abs(position.y - clusterBlock.gameObject.transform.position.y) < stageScript.cellSize) &&
				   (Mathf.Abs(position.x - clusterBlock.gameObject.transform.position.x) < stageScript.cellSize)){
					Debug.Log("YAAAAAAY");
					b.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					b.anchor = b.transform.position;
					newBlocks.Add(b);
       			 }
        
       				 // If collide from bottom.
			}
		
		}


		foreach(Block block in newBlocks){
			block.anchor = block.transform.position;
			allBlocks.Add(block);
		}

		return newBlocks;

	}

	public void saveAnchors(){
		foreach(Block b in allBlocks){
			b.anchor = b.transform.position;
		}
	}


	public void MoveBlocks(Vector3 newPosition){
		foreach(Block b in allBlocks){
			b.transform.position = b.anchor + newPosition;
		}
	}

	public void removeBlock(Block b){

	
	}
		 

}