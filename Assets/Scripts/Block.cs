using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public enum Direction { Up, Down, Left, Right }
	public Direction direction;

	void Start () {
	}
	
	void Update () {
	
	}

	public void SetDirection(Direction dir) {
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		direction = dir;
		switch (dir) {
		case Direction.Up:
			rb.velocity = new Vector2(0, 1);
			break;
		case Direction.Down:
			rb.velocity = new Vector2(0, -1);
			break;
		case Direction.Left:
			rb.velocity = new Vector2(-1, 0);
			break;
		case Direction.Right:
			rb.velocity = new Vector2(1, 0);
			break;
		}
	}
}
