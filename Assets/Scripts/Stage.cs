using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {
	public GameObject dot;
	public float gridSize;
	public float cellSize;

	void Start () {
		float rectSize = gameObject.GetComponent<RectTransform> ().sizeDelta.x;
		cellSize = rectSize / gridSize;

		GameObject d;
		Vector3 pos;
		for (float i = 0; i <= rectSize; i += cellSize) {
			for (float j = 0; j <= rectSize; j += cellSize) {
				pos = new Vector3(i-rectSize/2, j-rectSize/2, 0);
				d = Instantiate (dot) as GameObject;
				d.transform.parent = gameObject.transform;
				d.transform.localPosition = pos;
			}
		}
	}

	void Update () {
	
	}
}
