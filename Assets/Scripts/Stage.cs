using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage : MonoBehaviour {
	public Score score;
	public GameObject dot;
	public GameObject dots;
	public Block block;
	public int gridSize;
	public float cellSize;
	public float blockSpeed = 0.5f;
	public float blockSpawnTime = 500.0f;
	public Color[] colors = new Color[4];

	private float timer = 0;
	private float rectSize;
	private GameObject[,] grid;
	public List<Block> blocks;
	private List<Block> deadBlocks;
	private Cluster cluster;

	void Start () {
		grid = new GameObject[gridSize + 1, gridSize + 1];
		blocks = new List<Block> ();
		deadBlocks = new List<Block> ();
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
		
		cluster = gameObject.GetComponent<Cluster>();
  	}

	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			timer += blockSpawnTime;
			SpawnBlock ();
		}

		foreach (Block b in blocks) {
			Vector2 coords = PosToCoord(b.transform.localPosition);
			b.coords.text = coords.x + " " + coords.y;
			switch (b.direction) {
				case Block.Direction.Up:
				if (coords.y > gridSize) deadBlocks.Add (b);
				break;
				case Block.Direction.Down:
				if (coords.y < -1) deadBlocks.Add (b);
				break;
				case Block.Direction.Left:
				if (coords.x < -1) deadBlocks.Add (b);
				break;
				case Block.Direction.Right:
				if (coords.x > gridSize) deadBlocks.Add (b);
				break;
			}
		}

		cluster.CheckCollisions (blocks);

		foreach (Block b in deadBlocks) {
			blocks.Remove(b);
			Destroy(b.gameObject);
		}
		deadBlocks.Clear();
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
		blocks.Add (AddBlock (x, y, dir, Random.Range (0, 4)));
	}

	public Block AddBlock(int x, int y, Block.Direction dir, int col){
		score.Deduct ();
		Block newBlock = Instantiate (block);
		newBlock.stage = this;
		newBlock.SetDirection(dir);
		newBlock.SetColor (col);
		newBlock.transform.parent = transform;
		newBlock.transform.localPosition = CoordToPos (x, y);
		newBlock.transform.localScale = new Vector3 (cellSize, cellSize, 1);
		return newBlock;
	}

	public Vector2 CoordToPos(int x, int y){
		Vector2 pos = new Vector2 (x * cellSize - rectSize / 2, y * cellSize - rectSize / 2);
		pos.x += cellSize / 2;
		pos.y += cellSize / 2;
		return pos;
	}

	public Vector2 PosToCoord(Vector3 pos){
		float x = pos.x - cellSize / 2;
		float y = pos.y - cellSize / 2;
		x = (x + rectSize / 2) / cellSize;
		y = (y + rectSize / 2) / cellSize;
		return new Vector2 ((int) x, (int) y);
	}
}
