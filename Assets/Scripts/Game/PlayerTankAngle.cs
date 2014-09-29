using UnityEngine;
using System.Collections;

public class PlayerTankAngle : MonoBehaviour
{

    private Vector2 InputDirection;
    public float rotationSpeed;
    public float rotationInputEasing;
    private PlayerTankShoot.PlayerControl player;
    //public GameLogic.GameState gameState;
    //public GameLogic.PlayerTurnState playerTurn;
    public float YMax;
    public float YMin;
    public float YExtent;
    private GameLogic gamelogic;
    private string playerVerticalAxis = "";

    // Use this for initialization
    void Start()
    {
        player = gameObject.GetComponent<PlayerTankShoot>().player;

        if(player == PlayerTankShoot.PlayerControl.Red) {
            playerVerticalAxis = "Player1Vertical";
        }
        if(player == PlayerTankShoot.PlayerControl.Blue) {
            playerVerticalAxis = "Player2Vertical";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameLogic.gameState == GameLogic.GameState.GamePlay) {
            if(GameLogic.playerTurnState.ToString() == player.ToString()) {

                InputDirection.y = Input.GetAxis(playerVerticalAxis);
                if(InputDirection.y > rotationInputEasing && YExtent < YMax) {
                    //Debug.Log("up");
                    YExtent += 0.1f;
                    transform.Rotate(Vector3.left, rotationSpeed, Space.Self);
                }
                if(InputDirection.y < -rotationInputEasing && YExtent > YMin) {
                    YExtent -= 0.1f;
                    //Debug.Log("down");
                    transform.Rotate(Vector3.right, rotationSpeed, Space.Self);
                }
            }
        }
	
    }
}