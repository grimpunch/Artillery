using UnityEngine;
using System.Collections;

public class UnderwaterCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(transform.position.y<50)
		{
			this.gameObject.GetComponent<BlurEffect>().enabled = true;
			this.gameObject.GetComponent<Vignetting>().enabled = true;
		}
		else
		{
			this.gameObject.GetComponent<BlurEffect>().enabled = false;
			this.gameObject.GetComponent<Vignetting>().enabled = false;
		}
		
	}
}
