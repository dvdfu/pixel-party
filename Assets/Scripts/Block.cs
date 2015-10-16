using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public enum Direction { Up, Down, Left, Right }
	public Direction direction;
	public Cluster clusterScript;

	void Start () {
		clusterScript = gameObject.transform.parent.GetComponent<Cluster> ();
	}
	
	void Update () {
	}

	public void SetColor(Color col) {
		GetComponent<SpriteRenderer> ().color = col;
	}

	public void SetDirection(Direction dir) {
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		direction = dir;
		switch (dir) {
		case Direction.Up:
			rb.velocity = new Vector2(0, 2);
			break;
		case Direction.Down:
			rb.velocity = new Vector2(0, -2);
			break;
		case Direction.Left:
			rb.velocity = new Vector2(-2, 0);
			break;
		case Direction.Right:
			rb.velocity = new Vector2(2, 0);
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		/* combine them.
		Debug.Log("weeeeeeeeeeeeee");
		GameObject otherBlock = other.gameObject;
		//Block currentBlock = gameObject.GetComponent<Block>();

		otherBlock.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		clusterScript.allBlocks.Add(otherBlock.GetComponent<Block>());
		*/
	}
}
