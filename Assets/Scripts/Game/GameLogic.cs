using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	
		//GameType : 1v1, 1vAI etc
		public enum GameType
		{
				Head2Head,
				HumanVsCPU}
		;
		public GameType gametype;
		//GameState : Pre-Game settings , Game Play , Post Game wrap up.
		public enum GameState
		{
				PreGame,
				GamePlay,
				PostGame}
		;
		public static GameState gameState;
		//PlayerTurnState : Whose turn is it? Red or Blue.
		public enum PlayerTurnState
		{
				Red,
				Blue}
		;
		public static PlayerTurnState playerTurn;
	
		//MenuGUIControl
		public GUISkin GUISKIN;
		public GUISkin GUISKIN2;
		private Rect PreGameWindow = new Rect (0, 0, 275, 200);
		private Rect PostGameWindow = new Rect ((Screen.width / 2) - 50, (Screen.height / 2) - 100, 100, 200);
	
		//Players
		public GameObject RedArty;
		public GameObject BlueArty;
	
		//PlayerCannons
		public GameObject RedArtyCannon;
		public GameObject BlueArtyCannon;
	
	
		//SpawnLocations
		public GameObject[] RedSpawns;
		public GameObject[] BlueSpawns;
		public GameObject CBSpawn;
		public GameObject CB;
	
		//WinCounters
		public int BlueWins = 0;
		public int RedWins = 0;
	
		//Water
		public GameObject Water;
	
		//Turn
		public GameObject TurnProgressBar;
		public float TurnTime;
		public float TurnTimeSET;
		public bool Paused = false;
		public static bool ShotFiredThisRound = false;
		public static bool ExplosionEnded = false;
	
		//Current turn
		public int currentTurn;
	
		//Win/Lose Booleans
		public bool RedLose = false;
		public bool RedWin = false;
		public bool BlueLose = false;
		public bool BlueWin = false;
		//Height Variables for players:
		public static float HeightAboveSeaLevelRed;
		public static float HeightAboveSeaLevelBlue;
	
		//GUIControl
		public GameObject NeutralGUI;
		public GameObject RedHeightGUI;
		public GameObject BlueHeightGUI;
		public GameObject RedTurnTextGUI;
		public GameObject BlueTurnTextGUI;
		public GameObject RedWinsGUI;
		public GameObject BlueWinsGUI;
	
		public string RedName;
		public string BlueName;
	
		//DEBUG ONLY Game State Changers for testing in inpector view
		public GameState debuggamestateSetter;
		public PlayerTurnState debugplayerTurnSetter;
	
		// Use this for initialization
		void Start ()
		{
				RedArty = GameObject.FindGameObjectWithTag ("RedArty");
				BlueArty = GameObject.FindGameObjectWithTag ("BlueArty");
		
				Water = GameObject.FindGameObjectWithTag ("Water");

				TurnProgressBar.GetComponent<BuildCircleMesh> ().enabled = false;
				NeutralGUI = GameObject.FindGameObjectWithTag ("NeutralGUI");
				RedHeightGUI = GameObject.FindGameObjectWithTag ("RedHeightGUI");
				BlueHeightGUI = GameObject.FindGameObjectWithTag ("BlueHeightGUI");
				RedTurnTextGUI = GameObject.FindGameObjectWithTag ("RedTurnGUI");
				BlueTurnTextGUI = GameObject.FindGameObjectWithTag ("BlueTurnGUI");
				RedWinsGUI = GameObject.FindGameObjectWithTag ("REDWINSGUI");
				BlueWinsGUI = GameObject.FindGameObjectWithTag ("BLUEWINSGUI");
				CB = GameObject.FindGameObjectWithTag ("CentreBuilding");
		
				BlueTurnTextGUI.guiText.enabled = false;
				RedTurnTextGUI.guiText.enabled = false;
		
				RedWinsGUI.guiTexture.enabled = false;
				BlueWinsGUI.guiTexture.enabled = false;
		
				RedArty.transform.position = RedSpawns [Random.Range (0, 3)].transform.position;    
				BlueArty.transform.position = BlueSpawns [Random.Range (0, 3)].transform.position;
				RedArty.transform.rotation = RedSpawns [Random.Range (0, 3)].transform.rotation;    
				BlueArty.transform.rotation = BlueSpawns [Random.Range (0, 3)].transform.rotation;
				currentTurn = 0;
				//////Choose a player randomly to go first://///
				if (currentTurn == 0) {
						if (Random.Range (0, 10) > 5) {
								playerTurn = PlayerTurnState.Blue;
						} else {
								playerTurn = PlayerTurnState.Red;
						}
						currentTurn = 1;
				}
				TurnTime = TurnTimeSET;
				ExplosionEnded = false;
				ShotFiredThisRound = false;


				////////////////////////////////////////////
		}
		public void ExplosionEndedSet ()
		{
				ExplosionEnded = true;
		}
		void OnGUI ()
		{
		
				switch (gameState) {
				case GameState.PreGame:
						{
								GUI.skin = GUISKIN;
								PreGameWindow = GUI.Window (0, PreGameWindow, PreGameWindowFunction, "Game Setup");
								break;
						}
				case GameState.GamePlay:
						{
								GUI.Label (new Rect ((Screen.width / 2 - 22.5f), ((Screen.height * 0.1f) + 22.5f), 50, 30), "Turn:" + currentTurn);
								break;
						}
				case GameState.PostGame:
						{
								GUI.skin = GUISKIN2;
								PostGameWindow = GUI.Window (1, PostGameWindow, PostGameWindowFunction, "Game Over, Yeah!");
								break;
						}
				}
		}
	
		void PreGameWindowFunction (int windowID)
		{
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Red Player's Name", GUILayout.Width (120));
				RedName = GUILayout.TextField (RedName, 15, GUILayout.Width (120));
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Blue Player's Name", GUILayout.Width (120));
				BlueName = GUILayout.TextField (BlueName, 15, GUILayout.Width (120));
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Randomise Start Positions")) {
						RedArty.transform.position = RedSpawns [Random.Range (0, 3)].transform.position;    
						BlueArty.transform.position = BlueSpawns [Random.Range (0, 3)].transform.position;
						RedArty.transform.rotation = RedSpawns [Random.Range (0, 3)].transform.rotation;    
						BlueArty.transform.rotation = BlueSpawns [Random.Range (0, 3)].transform.rotation;
				}
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Start Game")) {
						StartGame ();
				}
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Quit To Main Menu")) {
						Application.LoadLevel (0);
				}
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);    
		}
	
		void PostGameWindowFunction (int windowID)
		{
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Red Wins:", GUILayout.Width (50));
				GUILayout.Label ("" + RedWins, GUILayout.Width (50));
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Blue Wins:", GUILayout.Width (50));
				GUILayout.Label ("" + BlueWins, GUILayout.Width (50));
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Rematch")) {
						NewGame ();
				}
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Quit")) {
						NewGame ();
						Application.LoadLevel (0);
				}
				GUILayout.EndHorizontal ();
		}
	
		void StartGame ()
		{
				if (RedName == "") {
						RedTurnTextGUI.guiText.text = "Red's Turn";
				} else {
						RedTurnTextGUI.guiText.text = "" + RedName + "'s Turn";
				}
				if (BlueName == "") {
						BlueTurnTextGUI.guiText.text = "Blue's Turn";
				} else {
						BlueTurnTextGUI.guiText.text = "" + BlueName + "'s Turn";
				}
		
				GameLogic.gameState = GameState.GamePlay;
				debuggamestateSetter = GameLogic.gameState;
				RedWinsGUI.guiTexture.enabled = false;
				BlueWinsGUI.guiTexture.enabled = false;
		}
		void NewGame ()
		{
				GameLogic.gameState = GameState.PreGame;
				debuggamestateSetter = GameLogic.gameState;
				//TurnProgressBar.GetComponent<BuildCircleMesh>().startAngle = 0;
				TurnProgressBar.GetComponent<BuildCircleMesh> ().enabled = false;
				RedArty.transform.position = RedSpawns [Random.Range (0, 3)].transform.position;    
				BlueArty.transform.position = BlueSpawns [Random.Range (0, 3)].transform.position;
				RedArty.transform.rotation = RedSpawns [Random.Range (0, 3)].transform.rotation;    
				BlueArty.transform.rotation = BlueSpawns [Random.Range (0, 3)].transform.rotation;
				RedLose = false;
				RedWin = false;
				BlueLose = false;
				BlueWin = false;
				RedWinsGUI.guiTexture.enabled = false;
				BlueWinsGUI.guiTexture.enabled = false;
				GameObject.FindGameObjectWithTag ("Splat").SendMessage ("Reset");
				GameObject.FindGameObjectWithTag ("World").GetComponent ("MakeCrater").SendMessage ("Reset");
				currentTurn = 0;
				//Choose a player randomly to go first://///
				if (currentTurn == 0) {
						if (Random.Range (0, 10) > 5) {
								playerTurn = PlayerTurnState.Blue;
						} else {
								playerTurn = PlayerTurnState.Red;
						}
						currentTurn = 1;
				}
				TurnTime = TurnTimeSET;
				ExplosionEnded = false;
				ShotFiredThisRound = false;
				RedArtyCannon.SendMessage ("downgrade");
				BlueArtyCannon.SendMessage ("downgrade");
				CB.gameObject.SendMessage ("Destruction");
				Instantiate (CB, CBSpawn.transform.position, CBSpawn.transform.rotation);
				////////////////////////////////////////////
		
		}
	
		public void upgrade ()
		{
				//GUI AND SOUND TO INDICATE UPGRADE NEEDED.
				RedArtyCannon.SendMessage ("upgrade");
				BlueArtyCannon.SendMessage ("upgrade");
		}
	
		// Update is called once per frame
		void Update ()
		{
				//debug setters ////////////////////
				/*gameState = debuggamestateSetter;
		playerTurn = debugplayerTurnSetter;*/
				////////////////////////////////////
		
				switch (gameState) {
				case GameState.PreGame:
						{
								TurnProgressBar.GetComponent<BuildCircleMesh> ().renderer.enabled = false;
								TurnProgressBar.GetComponent<BuildCircleMesh> ().enabled = false;
								RedTurnTextGUI.guiText.enabled = false;
								BlueTurnTextGUI.guiText.enabled = false;
								RedHeightGUI.gameObject.active = false;
								BlueHeightGUI.gameObject.active = false;
								break;
						}
				case GameState.GamePlay:
						{
			
				
				
								//TurnManager//////////////////////////////
				
								if (ExplosionEnded == true && ShotFiredThisRound == true) {
										if (playerTurn == PlayerTurnState.Red) {
												playerTurn = PlayerTurnState.Blue;
										} else if (playerTurn == PlayerTurnState.Blue) {
												playerTurn = PlayerTurnState.Red;
										}
										TurnTime = TurnTimeSET;
										currentTurn ++;
										ShotFiredThisRound = false;
										ExplosionEnded = false;
								} else if (TurnTime > 0 && ShotFiredThisRound == false) {
										TurnTime -= Time.deltaTime;
								} else if (TurnTime <= 0 && ShotFiredThisRound == false) {
										if (playerTurn == PlayerTurnState.Red) {
												playerTurn = PlayerTurnState.Blue;
										}
										if (playerTurn == PlayerTurnState.Blue) {
												playerTurn = PlayerTurnState.Red;
										}
										TurnTime = TurnTimeSET;
										currentTurn ++;
										ShotFiredThisRound = false;
										ExplosionEnded = false;
								}
								///////////////////////////////////////////
				
								//HAS SOMEONE LOST? NEXT STATE AHOY!//////
								if (RedLose == true || BlueLose == true) {
										if (RedLose == true) {
												BlueWin = true;
												BlueWins++;
										} else if (BlueLose == true) {
												RedWin = true;
												RedWins++;
										}
										RedLose = false;
										BlueLose = false;
										gameState = GameState.PostGame;
								}
				
								//////////////////////////////////////////
				
			
			
								//GUIHEIGHT//
								HeightAboveSeaLevelRed = RedArty.transform.position.y - Water.transform.position.y;
								HeightAboveSeaLevelBlue = BlueArty.transform.position.y - Water.transform.position.y;
								RedHeightGUI.gameObject.active = true;
								BlueHeightGUI.gameObject.active = true;
								/////////////
				
				
								//GUITurnIndicator//
								if (playerTurn == PlayerTurnState.Red) {
										TurnProgressBar.GetComponent<BuildCircleMesh> ().renderer.material.color = Color.red;
										RedTurnTextGUI.guiText.enabled = true;
										BlueTurnTextGUI.guiText.enabled = false;
								}
								;
								if (playerTurn == PlayerTurnState.Blue) {
										TurnProgressBar.GetComponent<BuildCircleMesh> ().renderer.material.color = Color.blue;
										RedTurnTextGUI.guiText.enabled = false;
										BlueTurnTextGUI.guiText.enabled = true;
								}
								;
								//GUITURNTIMER//////
								TurnProgressBar.GetComponent<BuildCircleMesh> ().enabled = true;
								////////////////////
				
				
								if (Input.GetKeyUp ("escape")) {
										if (Paused == false) {
												Time.timeScale = 0;
												Paused = true;
										} else if (Paused == true) {
												Time.timeScale = 1;
												Paused = false;
										}
										if (Paused == true) {
												NeutralGUI.camera.depth = 500;
					
					
										}
										if (Paused == false) {
												NeutralGUI.camera.depth = 10;
					
					
										}
										//pause
					
								}
								if (Paused == false) {
										TurnProgressBar.GetComponent<BuildCircleMesh> ().endAngle = (TurnTime * 6);
					
								}
								break;
						}
				case GameState.PostGame:
						{
								TurnProgressBar.GetComponent<BuildCircleMesh> ().renderer.enabled = false;
								TurnProgressBar.GetComponent<BuildCircleMesh> ().enabled = false;
								RedTurnTextGUI.guiText.enabled = false;
								BlueTurnTextGUI.guiText.enabled = false;
								RedHeightGUI.gameObject.active = false;
								BlueHeightGUI.gameObject.active = false;
								if (BlueWin == true) {
										RedWinsGUI.guiTexture.enabled = false;
										BlueWinsGUI.guiTexture.enabled = true;
								} else if (RedWin == true) {
										RedWinsGUI.guiTexture.enabled = true;
										BlueWinsGUI.guiTexture.enabled = false;
								}
			
			
								break;
						}
				}
		}
}
