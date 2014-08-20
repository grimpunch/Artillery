using UnityEngine;
using System.Collections;

public class BlueHeightGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	this.guiTexture.transform.position = new Vector3(0.5f,GameLogic.HeightAboveSeaLevelBlue/220,1);
	}
}
