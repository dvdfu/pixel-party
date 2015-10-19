using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster : MonoBehaviour {
	public Tile origin;
	public Tile tile;
	public Tile[,] tiles;

	private Stage stage;
	private int size;
	private Dictionary<Vector3, int> overlay;

	void Start () {
		stage = gameObject.GetComponent<Stage> ();
		size = stage.gridSize;
		Debug.Log ("size: " + size);
		tiles = new Tile[size, size];
		origin = AddTile (size/2, size/2, 0);

		// Draw Overlay .
		overlay = new Dictionary<Vector3, int>();
		for(int i = 2; i < 4; i++){
			for(int j = 2; j< 4; j++){
				overlay.Add(new Vector3(i,j,0), 0);
			}
		}

		DrawOverlay(overlay);
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

	public void AddOverlayTile(int x, int y, int color, float alpha){
		Tile newTile = Instantiate (tile);
		// Get rid of shadow.
		GameObject shadow = newTile.transform.FindChild("Shadow").gameObject;
		shadow.SetActive(false);
		
		newTile.stage = stage;
		newTile.cellX = x;
		newTile.cellY = y;
		newTile.SetColor (color);
		newTile.SetAlpha(alpha);
		newTile.transform.parent = transform;
		newTile.transform.localPosition = stage.CoordToPos (newTile.cellX, newTile.cellY);
		newTile.transform.localScale = new Vector3 (stage.cellSize, stage.cellSize, 1);
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

	public void DrawOverlay(Dictionary<Vector3, int> coordToColor){
		foreach(KeyValuePair<Vector3, int> entry in coordToColor){
			AddOverlayTile((int)entry.Key.x, (int)entry.Key.y, entry.Value, 0.2f);
    	}
	}
}