using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	private Vector3 mousePressed;
	private Vector3 mouseDragged;
	private Vector3 clusterAnchor;
	private Stage stageScript;
	private Block cluster;

	void Start () {
		stageScript = gameObject.GetComponent<Stage> ();
		cluster = stageScript.cluster;
		mouseDragged = Vector3.zero;
		mousePressed = Vector3.zero;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			mousePressed = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			clusterAnchor = cluster.transform.position;
		}
		if (Input.GetMouseButton (0)) {
			mouseDragged = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mousePressed;
			float cs = stageScript.cellSize;
			int dragX = (int) (mouseDragged.x / cs);
			int dragY = (int) (mouseDragged.y / cs);
			cluster.transform.position = clusterAnchor + new Vector3(dragX, dragY, 0)*cs;
		}
	}
}
