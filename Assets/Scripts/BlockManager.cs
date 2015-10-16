using UnityEngine;
using System.Collections;

public class blockManager : MonoBehaviour {
	private Stage stageScript;
	private float cellSize;

	void Start () {
		stageScript = transform.parent.gameObject.GetComponent<Stage>();
		cellSize = stageScript.cellSize;
	}

	void Update () {
	}
}
