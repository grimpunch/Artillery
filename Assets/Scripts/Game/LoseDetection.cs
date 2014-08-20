using UnityEngine;
using System.Collections;

public class LoseDetection : MonoBehaviour {
	
	public enum PlayerControl{Red,Blue};
	public PlayerControl player;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch(player)
			{
				case PlayerControl.Red:
				{
					if(transform.position.y < 50)
					{
					GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>().RedLose = true;
					}
					break;
				}
				case PlayerControl.Blue:
				{
					if(transform.position.y < 50)
					{
					GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>().BlueLose = true;
					}
					break;
				}
			default:
				break;
			
			}
	}

	
}
