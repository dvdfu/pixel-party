using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster : MonoBehaviour {
	public Tile origin;
	public Tile tile;
	public Tile[,] tiles;
	public Overlay overlay;
	public AudioSource bump;

	private int originOff;
	private Stage stage;
	private int size;

	void Start () {
		stage = gameObject.GetComponent<Stage> ();
		size = stage.gridSize*2;
		tiles = new Tile[size, size];
		originOff = size / 4;
		origin = AddTile (originOff, originOff, 0);
		bump = GetComponent<AudioSource> ();
	
		// Overlay
		overlay = gameObject.GetComponent<Overlay>();
		overlay.overlayCoords = overlay.RedSquareOverlay();
		//overlay.DrawOverlay();
	}

	public Tile AddTile(int x, int y, int color, float alpha = 1.0f, bool isOverlayTile = false) { // takes GRID coordinates
		int clusterX = x;
		int clusterY = y;
		if (origin != null) {
			clusterX += originOff - origin.cellX;
			clusterY += originOff - origin.cellY;
		}
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
		bump.Play ();
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

	public void MoveOverlayTo(int x, int y) { // takes GRID coordinates
		int dx = x - origin.cellX;
		int dy = y - origin.cellY;


		Dictionary<Vector3, int> newOverlay = new Dictionary<Vector3, int>();

		foreach(KeyValuePair<Vector3, int> entry in overlay.overlayCoords){
			newOverlay.Add(new Vector3(entry.Key.x + dx, entry.Key.y + dy, 0), entry.Value);
    	}

		// Kill old overlay.
		overlay.DestroyOverlay();

		overlay.overlayCoords = newOverlay;
		overlay.DrawOverlay();
  }

	public void CheckCollisions(List<Block> blocks){
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				Tile tile = tiles[i, j];
				if (tile != null) {
					int cellX = i + origin.cellX - originOff;
					int cellY = j + origin.cellY - originOff;
					foreach (Block b in blocks) {
						Vector2 coord = stage.PosToCoord(b.transform.localPosition);
						switch(b.direction) {
						case Block.Direction.Up:
							if (cellX == (int)coord.x && cellY-1 == (int)coord.y) {
								AddTile (cellX, cellY-1, b.color);
								b.isDead = true;
							}
						break;
						case Block.Direction.Down:
							if (cellX == (int)coord.x && cellY == (int)coord.y) {
								AddTile (cellX, cellY+1, b.color);
								b.isDead = true;
							}
						break;
						case Block.Direction.Left:
							if (cellX == (int)coord.x && cellY == (int)coord.y) {
								AddTile (cellX+1, cellY, b.color);
								b.isDead = true;
							}
						break;
						case Block.Direction.Right:
							if (cellX-1 == (int)coord.x && cellY == (int)coord.y) {
								AddTile (cellX-1, cellY, b.color);
								b.isDead = true;
							}
						break;
						}
					}
				}
			}
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