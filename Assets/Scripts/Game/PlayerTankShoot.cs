using UnityEngine;
using System.Collections;

public class PlayerTankShoot : MonoBehaviour
{

    public float Velocity;
    public float VelocityMax;
    public bool  increasing = false;
    public Vector3 ShootDirection;
    public GameObject Bombproj;
    //public GameObject SmallBomb;
    public GameObject Drum;
    public GameObject DrumPosition;
    public GameObject DrumKickbackPosition;
	
    public GameObject Muzzle;
    public float Inaccuracy;
    //public GameObject ProgressBar;
    //public GUITexture
    public enum PlayerControl
    {
        Red,
        Blue}
    ;
    public PlayerControl player;
	
	
	

    // Use this for initialization
    void Start()
    {
        Velocity = 0;
				
//				switch (player) {
//				case PlayerControl.Red:
//						ProgressBar = GameObject.FindGameObjectWithTag ("RedProgress");
//			//DrumPosition
//			//DrumKickbackPosition
//						break;
//				case PlayerControl.Blue:
//						ProgressBar = GameObject.FindGameObjectWithTag ("BlueProgress");
//			//DrumPosition 
//			//DrumKickbackPosition
//						break;
//				}
        //ProgressBar.GetComponent<BuildCircleMesh>().endAngle = 0;
        //ProgressBar.GetComponent<AudioSource>().pitch = 0;
		
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameLogic.gameState == GameLogic.GameState.GamePlay && GameLogic.ShotFiredThisRound == false)
        //{//HACK To test firing
        if((Input.GetButtonDown("Player2Fire")) && increasing != true) {
            increasing = true;
//            ProgressBar.GetComponent<AudioSource>().Play();
								
        }
        if(increasing == true && Time.timeScale > 0) {
            if(Velocity < VelocityMax) {
                Velocity++;
            }
//            ProgressBar.GetComponent<BuildCircleMesh>().endAngle = (Velocity / 0.279f);
//            if(ProgressBar.GetComponent<AudioSource>().pitch < 4) {
//                ProgressBar.GetComponent<AudioSource>().pitch += 0.04f;
//            }
								
            if(Velocity == VelocityMax) {
                Velocity = VelocityMax;
                increasing = false;
                //ProgressBar.GetComponent<BuildCircleMesh>().endAngle = 360;
            }
        }
        if(Input.GetButtonUp("Player2Fire")) {
            increasing = false;
            //GameLogic.ShotFiredThisRound = true;
            //ProgressBar.GetComponent<BuildCircleMesh>().endAngle = 0;
            Debug.DrawLine(Muzzle.transform.position, Muzzle.transform.position + Muzzle.transform.forward, Color.red);
            GameObject projectile;
            Vector3 muzzlevelocity = Muzzle.transform.forward;
					            
            if(Inaccuracy != 0) {
                Vector2 rand = Random.insideUnitCircle;
                muzzlevelocity += new Vector3(rand.x, rand.y, 0) * Inaccuracy;
            } 
            muzzlevelocity = muzzlevelocity.normalized * Velocity;
            projectile = Instantiate(Bombproj, Muzzle.transform.position, Quaternion.identity) as GameObject;
            projectile.GetComponent("Bomb").SendMessage("SetVelocity", muzzlevelocity);
            Velocity = 0;
            //ProgressBar.GetComponent<AudioSource>().pitch = 0;
            //ProgressBar.GetComponent<AudioSource>().Stop();
            Drum.transform.position = DrumKickbackPosition.transform.position;
        }
								
        //}
    }
}
