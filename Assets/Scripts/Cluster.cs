using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster : MonoBehaviour {
//	public List<Block> allBlocks;
//	public Block origin;
	public int originX;
	public int originY;
	public Tile tile;
	public Tile[,] tiles;
	private Stage stage;
//
	void Start () {
		stage = gameObject.GetComponent<Stage> ();
		int size = stage.gridSize + 1;
		tiles = new Tile[size, size];
		originX = size / 2;
		originY = size / 2;
		AddTile (originX, originY, 0);

		// Draw Overlay .
		Dictionary<Vector3, Color> overlay = new Dictionary<Vector3, Color>();
		for(int i = 2; i < 4; i++){
			for(int j = 2; j< 4; j++){
				overlay.Add(new Vector3(i,j,0), Color.blue);
			}
		}

		DrawOverlay(overlay);
	}

	public void AddTile(int x, int y, int color) {
		Tile newTile = Instantiate (tile);
		newTile.stage = stage;
		newTile.cellX = x;
		newTile.cellY = y;
		newTile.SetColor (color);
		newTile.transform.parent = transform;
		newTile.transform.localPosition = stage.CoordToPos (newTile.cellX, newTile.cellY);
		newTile.transform.localScale = new Vector3 (stage.cellSize, stage.cellSize, 1);
		tiles[x, y] = newTile;
	}

//	public void InitializeBlock(Stage script){
//		firstBlock = Instantiate (script.block);
//		firstBlock.transform.parent = gameObject.transform;
//		firstBlock.transform.localPosition = script.CoordToPos(3,3);
//		firstBlock.transform.localScale = new Vector3(script.cellSize, script.cellSize, 1);
//		allBlocks = new List<Block>();
//		allBlocks.Add(firstBlock);
//	}
//
//	void End (){
//	}
//
//	void Update (){
//	}
//
//	public void CheckCollisions(List<Block> blocks){
//	}
//
//	public List<Block> CheckCollision(List<Block> blocks){
//		List<Block> newBlocks = new List<Block>();
//		foreach(Block b in blocks){
//			foreach(Block clusterBlock in allBlocks){
//				// If collide from top.
//				Vector3 position = b.gameObject.transform.position;
//				
//				if((Mathf.Abs(position.y - clusterBlock.gameObject.transform.position.y) < stageScript.cellSize) &&
//				   (Mathf.Abs(position.x - clusterBlock.gameObject.transform.position.x) < stageScript.cellSize)){
//					b.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
//					//b.anchor = stageScript.PosToCoord (b.transform.position);
//					Vector2 coord = stageScript.PosToCoord(b.transform.localPosition);
//					b.anchor = stageScript.CoordToPos((int)coord.x, (int)coord.y);
//					newBlocks.Add(b);
//       			 }
//			}
//		}
//
//		foreach(Block block in newBlocks){
//			//block.anchor = block.transform.position;
//			allBlocks.Add(block);
//		}
//		return newBlocks;
//	}
//
//	public void SaveAnchors(){
//		foreach(Block b in allBlocks){
//			b.anchor = b.transform.position;
//		}
//	}
//
//	public void MoveBlocks(Vector3 newPosition){
//		foreach(Block b in allBlocks){
//			b.transform.position = b.anchor + newPosition;
//		}
//	}
//
//	public void RemoveBlock(Block b){
//	}

	public void DrawOverlay(Dictionary<Vector3, Color> coordToColor){
		foreach(KeyValuePair<Vector3, Color> entry in coordToColor){
			AddTile((int)entry.Key.x, (int)entry.Key.y, 0);
    	}
	}


	
}