using UnityEngine;
using System.Collections;


public class PlayerTankRotate : MonoBehaviour
{
    public Vector2 InputDirection;
    public float rotationSpeed;
    public float rotationInputEasing;
	
    public PlayerTankShoot.PlayerControl player;
    private GameLogic gamelogic;
    private string playerHorizontalAxis = "";

    // Use this for initialization
    void Start()
    {
        gamelogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        if(player == PlayerTankShoot.PlayerControl.Red) {
            playerHorizontalAxis = "Player1Horizontal";
        }
        if(player == PlayerTankShoot.PlayerControl.Blue) {
            playerHorizontalAxis = "Player2Horizontal";
        }
    }
	
    // Update is called once per frame
    void Update()
    {
        if(GameLogic.gameState == GameLogic.GameState.GamePlay) {
            if(gamelogic.playerTurnState.ToString() == player.ToString()) {

                InputDirection.x = Input.GetAxis(playerHorizontalAxis);
            
                if(InputDirection.x > rotationInputEasing) {
                    transform.Rotate(Vector3.up, rotationSpeed, Space.Self);
                }
                if(InputDirection.x < -rotationInputEasing) {
                    transform.Rotate(Vector3.down, rotationSpeed, Space.Self);
                }
            }
        }
    }
}
