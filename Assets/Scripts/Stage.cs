using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage : MonoBehaviour {
	public GameObject dot;
	public GameObject dots;
	public Block block;
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

		for (int i = 0; i < 5; i++) {
			SpawnBlock ();
		}
	}

	void Update () {
	
	}

	public void SpawnBlock() {
		int x, y;
		Block.Direction dir;
		if (Random.value > 0.5f) { // vertical trajectory
			x = Random.Range(0, gridSize-1);
			if (Random.value > 0.5f) {
				y = -1;
				dir = Block.Direction.Up;
			} else {
				y = gridSize;
				dir = Block.Direction.Down;
			}
		} else { // horizontal trajectory
			y = Random.Range(0, gridSize-1);
			if (Random.value > 0.5f) {
				x = -1;
				dir = Block.Direction.Right;
			} else {
				x = gridSize;
				dir = Block.Direction.Left;
			}
		}
		AddBlock (x, y, dir);
	}

	public void AddBlock(int x, int y, Block.Direction dir){
		Block newBlock = Instantiate (block);
		newBlock.SetDirection(dir);
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
