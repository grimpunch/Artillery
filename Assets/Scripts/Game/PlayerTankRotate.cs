using UnityEngine;
using System.Collections;

public class PlayerTankRotate : MonoBehaviour
{

    public Vector2 InputDirection;
    public float rotationSpeed;
    public float rotationInputEasing;
	
    public PlayerTankShoot.PlayerControl player;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
//        if(GameLogic.playerTurn == GameLogic.PlayerTurnState.Blue) {
        InputDirection.x = Input.GetAxis("Player2Horizontal");
            
        if(InputDirection.x > rotationInputEasing) {
            transform.Rotate(Vector3.up, rotationSpeed, Space.Self);
        }
        if(InputDirection.x < -rotationInputEasing) {
            transform.Rotate(Vector3.down, rotationSpeed, Space.Self);
        }
//        }
		
    }
}
