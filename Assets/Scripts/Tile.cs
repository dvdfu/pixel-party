using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public int color;
	public int cellX;
	public int cellY;
	public Stage stage;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void SetColor(int color) {
		this.color = color;
		GetComponent<SpriteRenderer> ().color = stage.colors[color];
	}
}
