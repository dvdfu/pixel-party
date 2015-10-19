using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
	private Vector3 mousePressed;
	private Vector3 mouseDragged;
	private Stage stageScript;
	private Cluster cluster;
	private int clusterX;
	private int clusterY;

	void Start () {
		cluster = gameObject.GetComponent<Cluster>();
		stageScript = gameObject.GetComponent<Stage> ();
		mouseDragged = Vector3.zero;
		mousePressed = Vector3.zero;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			mousePressed = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			clusterX = cluster.origin.cellX;
			clusterY = cluster.origin.cellY;
		}
		if (Input.GetMouseButton (0)) {
			mouseDragged = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mousePressed;
			float cs = stageScript.cellSize;
			int dragX = (int) (mouseDragged.x / cs);
			int dragY = (int) (mouseDragged.y / cs);
			cluster.MoveOriginTo(clusterX + dragX, clusterY + dragY);
		}
	}
}
