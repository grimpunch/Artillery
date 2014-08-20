using UnityEngine;
using System.Collections;

public class PlayerTankRotate : MonoBehaviour {

    public Vector2 InputDirection;
    public float rotationSpeed;
    public float rotationInputEasing;
	
	public PlayerTankShoot.PlayerControl player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameLogic.gameState == GameLogic.GameState.GamePlay)
		{
			switch(player)
			{
				case PlayerTankShoot.PlayerControl.Red:
				{
				if(GameLogic.playerTurn == GameLogic.PlayerTurnState.Red)
						{
					    if(Input.GetAxis("JoyHorizontal1") == 0 )
							{
							InputDirection.x = Input.GetAxis("Horizontal1");
							}
							if(Input.GetAxis("JoyHorizontal1") != 0)
							{
							InputDirection.x = Input.GetAxis("JoyHorizontal1");
							}
					        if (InputDirection.x > rotationInputEasing)
					        {
					            transform.Rotate(Vector3.up, rotationSpeed,Space.Self);
					        }
					        if (InputDirection.x < -rotationInputEasing)
					        {
					            transform.Rotate(Vector3.down , rotationSpeed,Space.Self);
					        }
						}
					break;
				}
				case PlayerTankShoot.PlayerControl.Blue:
				{
				if(GameLogic.playerTurn == GameLogic.PlayerTurnState.Blue)
						{
							if(Input.GetAxis("JoyHorizontal2") == 0 )
							{
							InputDirection.x = Input.GetAxis("Horizontal2");
							}
							if(Input.GetAxis("JoyHorizontal2") != 0)
							{
							InputDirection.x = Input.GetAxis("JoyHorizontal2");
							}
					        if (InputDirection.x > rotationInputEasing)
					        {
					            transform.Rotate(Vector3.up, rotationSpeed,Space.Self);
					        }
					        if (InputDirection.x < -rotationInputEasing)
					        {
					            transform.Rotate(Vector3.down , rotationSpeed,Space.Self);
					        }
						}
					break;
				}
			}
		}
	}
}
