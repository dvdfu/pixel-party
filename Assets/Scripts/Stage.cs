using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage : MonoBehaviour {
	public GameObject dot;
	public GameObject block;
	public int gridSize;
	public float cellSize;
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
				d.transform.parent = gameObject.transform;
				d.transform.localPosition = pos;

				grid[i,j] = d;
			}
		}
		Debug.Log(rectSize);

		// Instantiate first block and set position.
		GameObject firstBlock = Instantiate (block) as GameObject;
		firstBlock.transform.parent = gameObject.transform;
		firstBlock.transform.localPosition = coordToPos(3,3);
		firstBlock.transform.localScale = new Vector2(cellSize, cellSize);


	}

	void Update () {
	
	}

	public Vector2 coordToPos(int x, int y){
		Vector2 pos = grid[x,y].transform.localPosition;
		Debug.Log(pos);
		pos.x += cellSize / 2;
		pos.y += cellSize / 2;
		Debug.Log(pos);
		return pos;

	}
}
