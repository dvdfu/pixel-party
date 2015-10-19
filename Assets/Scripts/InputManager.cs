using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
	private Vector3 mousePressed;
	private Vector3 mouseDragged;
	private Vector3 clusterAnchor;
	private Stage stageScript;
	private Block cluster;
	private Cluster clusterScript;

	void Start () {
		clusterScript = gameObject.GetComponent<Cluster>();
		stageScript = gameObject.GetComponent<Stage> ();
//		cluster = clusterScript.firstBlock;
		mouseDragged = Vector3.zero;
		mousePressed = Vector3.zero;
	}
	
	void Update () {
//		List<Block> l = clusterScript.CheckCollision(stageScript.blocks);
//		foreach(Block b in l){
//			stageScript.blocks.Remove(b);
//			//b.anchor = b.transform.position;
//    	}
//		if (Input.GetMouseButtonDown (0)) {
//			mousePressed = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//			clusterScript.SaveAnchors();
//			//cluster.anchor = cluster.transform.position;
//		}
//		if (Input.GetMouseButton (0)) {
//			mouseDragged = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mousePressed;
//			float cs = stageScript.cellSize;
//			int dragX = (int) (mouseDragged.x / cs);
//			int dragY = (int) (mouseDragged.y / cs);
//			//cluster.transform.position = clusterAnchor + new Vector3(dragX, dragY, 0)*cs;
//			clusterScript.MoveBlocks(new Vector3(dragX, dragY, 0)*cs);
//		}
	}
}
