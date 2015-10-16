using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
	public Text label;
	public int remaining;

	void Start () {
		label = GetComponent<Text>();
		label.text = "" + remaining;
	}
	
	void Update () {
	
	}

	public void Deduct() {
		if (remaining > 0) {
			remaining--;
			label.text = "" + remaining;
		}
	}
}
