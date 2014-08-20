using UnityEngine;
using System.Collections;

public class Destruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public void Destruction()
	{
		Debug.Log("Destroyed buildings");
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
