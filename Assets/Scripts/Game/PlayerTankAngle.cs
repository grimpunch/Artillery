using UnityEngine;
using System.Collections;

public class PlayerTankAngle : MonoBehaviour {

    public Vector2 InputDirection;
    public float rotationSpeed;
    public float rotationInputEasing;
	public PlayerTankShoot.PlayerControl player;
	//public GameLogic.GameState gameState;
	//public GameLogic.PlayerTurnState playerTurn;
	public float YMax;
    public float YMin;
    public float YExtent;

    // Use this for initialization
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		if(GameLogic.gameState == GameLogic.GameState.GamePlay)
		{
			switch(player)
			{
				case PlayerTankShoot.PlayerControl.Red:
				{
			         if(GameLogic.playerTurn == GameLogic.PlayerTurnState.Red)
						{
							if(Input.GetAxis("JoyVertical1") == 0 )
							{
							InputDirection.y = Input.GetAxis("Vertical1");
							}
							if(Input.GetAxis("JoyVertical1") != 0)
							{
							InputDirection.y = Input.GetAxis("JoyVertical1");
							}
							
					        if (InputDirection.y > rotationInputEasing && YExtent < YMax)
					        {
					            //Debug.Log("up");
					            YExtent += 0.1f;
					            transform.Rotate(Vector3.left, rotationSpeed, Space.Self);
					        }
					        if (InputDirection.y < -rotationInputEasing &&   YExtent > YMin)
					        {
					            YExtent -= 0.1f;
					            //Debug.Log("down");
					            transform.Rotate(Vector3.right, rotationSpeed, Space.Self);
					        }
						}
							break;
						
				}
				case PlayerTankShoot.PlayerControl.Blue:
				{
					if(GameLogic.playerTurn == GameLogic.PlayerTurnState.Blue)
						{
							if(Input.GetAxis("JoyVertical2") == 0 )
							{
							InputDirection.y = Input.GetAxis("Vertical2");
							}
							if(Input.GetAxis("JoyVertical2") != 0)
							{
							InputDirection.y = Input.GetAxis("JoyVertical2");
							}
					        if (InputDirection.y > rotationInputEasing && YExtent < YMax)
					        {
					            //Debug.Log("up");
					            YExtent += 0.1f;
					            transform.Rotate(Vector3.left, rotationSpeed, Space.Self);
					        }
					        if (InputDirection.y < -rotationInputEasing &&   YExtent > YMin)
					        {
					            YExtent -= 0.1f;
					            //Debug.Log("down");
					            transform.Rotate(Vector3.right, rotationSpeed, Space.Self);
					        }
						}
							break;
				}
	
	    	}
		}
	}
}