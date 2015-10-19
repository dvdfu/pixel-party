using UnityEngine;
using System.Collections.Generic;

public class Overlay : MonoBehaviour {

	public Tile tile;
	public Stage stage;
	public Dictionary<Vector3, int> overlayCoords;

	// Use this for initialization
	void Start () {
		stage = gameObject.GetComponent<Stage> ();
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
		newTile.transform.localPosition = stage.CoordToPos (newTile.cellX, newTile.cellY);
		newTile.transform.localScale = new Vector3 (stage.cellSize, stage.cellSize, 1);
		
	}
	
	public Dictionary<Vector3, int> RedSquareOverlay(){

		Dictionary<Vector3, int> overlay = new Dictionary<Vector3, int>();
		for(int i = 0; i < 2; i++){
			for(int j = 0; j< 2; j++){
				overlay.Add(new Vector3(i,j,0), 0);
			}
		}

		return overlay;

	}

	public void DrawOverlay(){
		foreach(KeyValuePair<Vector3, int> entry in overlayCoords){
			AddOverlayTile((int)entry.Key.x, (int)entry.Key.y, entry.Value, 0.2f);
		}
	}
}
