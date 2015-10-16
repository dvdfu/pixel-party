using UnityEngine;
using System.Collections;

public class PixelArt : MonoBehaviour {
	public int width;
	public int height;
	public int originX;
	public int originY;
	public int[,] colors;
	
	void Start () {
		colors = new int[width, height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				colors[i, j] = height % 4;
			}
		}
	}
	
	void Update () {
	}
	
	public int originColor() {
		return colors[originX, originY];
	}
}
