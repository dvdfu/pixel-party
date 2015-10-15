using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {
	public GameObject dot;
	public Vector2 gridSize;

	void Start () {
		GameObject d;
		Vector3 pos;
		for (int i = 0; i <= gridSize.x; i++) {
			for (int j = 0; j <= gridSize.y; j++) {
				Vector2 r = gameObject.GetComponent<RectTransform>().sizeDelta;
				pos = new Vector3(r.x * (i/gridSize.x - 0.5f), r.y * (j/gridSize.y - 0.5f), 0);
				d = Instantiate (dot) as GameObject;
				d.transform.parent = gameObject.transform;
				d.transform.localPosition = pos;
			}
		}
	}

	void Update () {
	
	}
}
