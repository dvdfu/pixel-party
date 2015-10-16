using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage : MonoBehaviour {
	public GameObject dot;
	public GameObject dots;
	public GameObject block;
	public int gridSize;
	public float cellSize;
	public float blockSpeed = 0.5f;

	private float rectSize;
	private GameObject[,] grid;

	void Start () {
		grid = new GameObject[gridSize + 1, gridSize + 1];

		rectSize = gameObject.GetComponent<RectTransform> ().sizeDelta.x;
		cellSize = rectSize / gridSize;

		GameObject d;
		Vector3 pos;
		for (int i = 0; i <= gridSize; i++) {
			for (int j = 0; j <= gridSize; j++) {
				pos = new Vector3(i*cellSize-rectSize/2, j*cellSize-rectSize/2, 0);
				d = Instantiate (dot) as GameObject;
				d.transform.parent = dots.transform;
				d.transform.localPosition = pos;
				grid[i,j] = d;
			}
		}
		SpawnBlock ();
		SpawnBlock ();
		SpawnBlock ();
	}

	void Update () {
	
	}

	public void SpawnBlock() {
		int x, y;
		if (Random.value > 0.5f) { // vertical trajectory
			x = Random.Range(-1, gridSize);
			y = Random.value > 0.5f ? -1 : gridSize;
		} else { // horizontal trajectory
			x = Random.value > 0.5f ? -1 : gridSize;
			y = Random.Range(-1, gridSize);
		}
		AddBlock (x, y);
	}

	public void AddBlock(int x, int y){
		GameObject newBlock = Instantiate (block) as GameObject;
		newBlock.transform.parent = transform;
		newBlock.transform.localPosition = CoordToPos (x, y);
		newBlock.transform.localScale = new Vector2 (cellSize, cellSize);
	}

	public Vector2 CoordToPos(int x, int y){
		Vector2 pos = new Vector2 (x * cellSize - rectSize / 2, y * cellSize - rectSize / 2);
		pos.x += cellSize / 2;
		pos.y += cellSize / 2;
		return pos;
	}
}
