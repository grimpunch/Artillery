using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public Texture2D GREENICON;
	public Texture2D YELLOWICON;
	public Texture2D GREYICON;
	public Texture2D InstructGUI;
	public GUISkin GUISKIN;
	private Rect MainGameWindow = new Rect (Screen.width*0.55f,Screen.height*0.1f,Screen.width*0.40f,Screen.height*0.8f);
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI()
	{
		
	GUI.skin = GUISKIN;
	MainGameWindow = GUI.Window (0, MainGameWindow, MainGameWindowFunction, "Choose a Level");
		
	}
	
	void MainGameWindowFunction(int windowID)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label("Green Level");
		GUILayout.Label("Yellow Level");
		GUILayout.Label("Grey Level");
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
			if(GUILayout.Button(GREENICON))
            {
            	Application.LoadLevel(1);
            }
		
            if(GUILayout.Button(YELLOWICON))
            {
                Application.LoadLevel(2);
            }
			if(GUILayout.Button(GREYICON))
            {
                Application.LoadLevel(3);
            }
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.TextArea("Instructions: " +
			"The goal of the game is to sink your opponent beneath the water by shooting shells and damaging the terrain under them." +
			"Players take turns aiming their artillery at the opponent and shooting out the ground beneath them" +
			"Each level contains a PowerUp which gives BOTH players far more destructive shells.");
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
			//GUIExplanation pic
			GUI.DrawTexture(new Rect(-25,Screen.height*0.31f,Screen.width*0.45f,Screen.height*0.4f),InstructGUI,ScaleMode.ScaleToFit);
		GUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
			if(GUILayout.Button("Quit"))
            {
                Application.Quit();
            }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
