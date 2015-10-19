using UnityEngine;
using System.Collections.Generic;

public class Overlay : MonoBehaviour {

	public Tile tile;
	public Stage stage;
	public Dictionary<Vector3, int> overlayCoords;
	public List<Tile> overlayTiles;

	// Use this for initialization
	void Start () {
		stage = gameObject.GetComponent<Stage> ();
		overlayTiles = new List<Tile>();
	}
	
	// Update is called once per frame
	void Update () {
	
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
		Vector3 newPos = stage.CoordToPos (newTile.cellX, newTile.cellY);
		newPos.z += 1;
		newTile.transform.localPosition = newPos;
		newTile.transform.localScale = new Vector3 (stage.cellSize, stage.cellSize, 1);
		overlayTiles.Add(newTile);
	}
	
	public Dictionary<Vector3, int> RedSquareOverlay(){
		int middleGrid = stage.gridSize/2 ;
		int offset = 2;

		Dictionary<Vector3, int> overlay = new Dictionary<Vector3, int>();
		for(int i = middleGrid - offset; i <= middleGrid + offset; i++){
			for(int j = middleGrid - offset; j<= middleGrid + offset; j++){
				overlay.Add(new Vector3(i,j,1), 0);
			}
		}
		return overlay;
	}

	public void DrawOverlay(){
		foreach(KeyValuePair<Vector3, int> entry in overlayCoords){
			AddOverlayTile((int)entry.Key.x, (int)entry.Key.y, entry.Value, 0.2f);
		}
	}

	public void DestroyOverlay(){
		foreach(Tile t in overlayTiles){
			Destroy(t.gameObject);
		}
		
		overlayTiles.Clear();
	}
}
