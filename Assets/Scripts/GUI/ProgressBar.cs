using UnityEngine;
using System.Collections;
 
public class ProgressBar : MonoBehaviour {
	public float barDisplay; //current progress
	public Vector2 pos = new Vector2(20,40);
	public Vector2 size = new Vector2(0.1f, 0.1f);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	public GameObject player;

	private Vector2 final_size;

	void OnGUI() {
		//draw the background:
		//GUI.backgroundColor = new Color(1f, 1f, 1f, 0f);
		GUI.BeginGroup(new Rect(pos.x, pos.y, final_size.x, final_size.y));
		//GUI.Box(new Rect(0,0, final_size.x, final_size.y), emptyTex);
		GUI.DrawTexture(new Rect(0,0, final_size.x, final_size.y), emptyTex, ScaleMode.StretchToFill, true, 0);

		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, final_size.x * barDisplay, final_size.y));
		//GUI.Box(new Rect(0,0, final_size.x, final_size.y), fullTex);
		GUI.DrawTexture(new Rect(0,0, final_size.x, final_size.y), fullTex, ScaleMode.StretchToFill, true, 0);
		GUI.EndGroup();
		GUI.EndGroup();
	}

	void Update() {
		//for this example, the bar display is linked to the current time,
		//however you would set this value based on your desired display
		//eg, the loading progress, the player's health, or whatever.
		//barDisplay = Time.time*0.05f;

		final_size = new Vector2(size.x * Screen.width, size.y * Screen.height);

		barDisplay = player.GetComponent<Entity>().health / player.GetComponent<Entity>().maxHealth;
	}
}