using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster : MonoBehaviour {
	public int originX;
	public int originY;
	public Tile tile;
	public Tile[,] tiles;

	private Stage stage;
	private int size;
	private Overlay overlay;
//
	void Start () {
		stage = gameObject.GetComponent<Stage> ();
		size = stage.gridSize;
		tiles = new Tile[size, size];
		originX = size / 2;
		originY = size / 2;
		AddTile (0, 0, 0);

		// Overlay
		overlay = gameObject.GetComponent<Overlay>();
		overlay.overlayCoords = overlay.RedSquareOverlay();
		overlay.DrawOverlay();



	}

	public void AddTile(int x, int y, int color, float alpha = 1.0f, bool isOverlayTile = false) {
		if (tiles [x, y] != null)
			return;
		Tile newTile = Instantiate (tile);
		newTile.stage = stage;
		newTile.cellX = x;
		newTile.cellY = y;
		newTile.SetColor (color);
		newTile.SetAlpha(alpha);
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
	public void CheckCollisions(List<Block> blocks){
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				Tile tile = tiles[i, j];
				if (tile != null) {
					foreach (Block b in blocks) {
						Vector2 coord = stage.PosToCoord(b.transform.localPosition);
						if (i == (int)coord.x && j == (int)coord.y) {
							AttachBlock(b, i, j);
						}
					}
				}
			}
		}
	}

	private void AttachBlock(Block b, int x, int y) {
		switch (b.direction) {
		case Block.Direction.Up:
			AddTile (x, y-1, b.color);
			break;
		case Block.Direction.Down:
			AddTile (x, y+1, b.color);
			break;
		case Block.Direction.Left:
			AddTile (x+1, y, b.color);
			break;
		case Block.Direction.Right:
			AddTile (x-1, y, b.color);
			break;
		}
	}
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

	public int CheckFinalScore(){
		int score = 0;

		for(int i = 0; i < size; i++){
			for(int j = 0; j < size; j++){
				Tile t = tiles[i,j];

				if(t == null){
					continue;
				}

				Vector3 coord = new Vector3(t.cellX, t.cellY, 0);
				if(overlay.overlayCoords.ContainsKey(coord)){
					int color = overlay.overlayCoords[coord];
					if(t.color == color){
						score += 1;
					}
				}
				
			}


		
		}

		return score;
	}
		
}