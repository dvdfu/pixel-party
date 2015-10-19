using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster : MonoBehaviour {
	public Tile origin;
	public Tile tile;
	public Tile[,] tiles;

	private Stage stage;
	private int size;
	private Overlay overlay;

	void Start () {
		stage = gameObject.GetComponent<Stage> ();
		size = stage.gridSize;
		Debug.Log ("size: " + size);
		tiles = new Tile[size, size];

		origin = AddTile (size/2, size/2, 0);
	
		// Overlay
		overlay = gameObject.GetComponent<Overlay>();
		overlay.overlayCoords = overlay.RedSquareOverlay();
		overlay.DrawOverlay();



	}

	public Tile AddTile(int x, int y, int color, float alpha = 1.0f, bool isOverlayTile = false) { // takes GRID coordinates
		int clusterX = x;
		int clusterY = y;
		if (origin != null) {
			clusterX += size/2 - origin.cellX;
			clusterY += size/2 - origin.cellY;
		}
		Debug.Log ("Add tile at " + clusterX + " " + clusterY);
		if (tiles [clusterX, clusterY] != null) {
			return null;
		}
		Tile newTile = Instantiate (tile);
		newTile.stage = stage;
		newTile.cellX = x;
		newTile.cellY = y;
		newTile.SetColor (color);
		newTile.SetAlpha(alpha);
		newTile.transform.parent = transform;
		newTile.transform.localPosition = stage.CoordToPos (x, y);
		newTile.transform.localScale = new Vector3 (stage.cellSize, stage.cellSize, 1);
		tiles[clusterX, clusterY] = newTile;
		return newTile;
	}

	public void MoveOriginTo(int x, int y) { // takes GRID coordinates
		int dx = x - origin.cellX;
		int dy = y - origin.cellY;
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				Tile tile = tiles[i, j];
				if (tile != null) {
					tile.cellX += dx;
					tile.cellY += dy;
					tile.transform.localPosition += new Vector3(dx, dy, 0) * stage.cellSize;
				}
			}
		}
	}

	public void CheckCollisions(List<Block> blocks){
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				Tile tile = tiles[i, j];
				if (tile != null) {
					int cellX = i + origin.cellX - size/2;
					int cellY = j + origin.cellY - size/2;
					foreach (Block b in blocks) {
						Vector2 coord = stage.PosToCoord(b.transform.localPosition);
						if (cellX == (int)coord.x && cellY == (int)coord.y) {
							AttachBlock(b, cellX, cellY);
						}
					}
				}
			}
		}
	}

	private void AttachBlock(Block b, int x, int y) { // takes GRID coordinates
		Debug.Log ("Attach block at " + x + " " + y);
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