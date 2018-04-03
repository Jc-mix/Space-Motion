using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
	private IUserAction action;
	private GenGameObjects sc;
	private float button_width;
	private float button_height;
	private float button_y;
	private float move_x;

	// Use this for initialization
	void Start () {
		sc = SSDirector.getInstance().currentSceneController as GenGameObjects;
		action = SSDirector.getInstance ().currentSceneController as IUserAction;
		button_width = 100;
		button_height = 50;
		button_y = 80;
		move_x = 100;
		Debug.Log("UI start");
		Debug.Log(action);
	}

	void OnGUI() {
		// reset button
		if (GUI.Button(new Rect(350, button_y + 200, button_width, button_height), "Reset")) {
			action.reset();
		}
		if (sc.game_state == GenGameObjects.State.over) {
			GUI.Label(new Rect(350, button_y, button_width * 2, button_height * 2), "GameOver!");
			return;
		}
		if (sc.game_state == GenGameObjects.State.win) {
			GUI.Label(new Rect(350, button_y, button_width * 2, button_height * 2), "Win");
			return;
		}
		// priest to boat at the begin
		if (GUI.Button(new Rect(70, button_y, button_width, button_height), "On")) {
			action.priest_to_boat_at_begin();
			Debug.Log("button clicked");
		}
		// devil to boat at the begin
		if (GUI.Button(new Rect(70 + move_x, button_y, button_width, button_height), "On")) {
			action.devil_to_boat_at_begin();
		}
		// priest to boat at the end
		if (GUI.Button(new Rect(540, button_y, button_width, button_height), "On")) {
			action.priest_to_boat_at_end();
		}
		// devil to boat at the end
		if (GUI.Button(new Rect(540 + move_x, button_y, button_width, button_height), "On")) {
			action.devil_to_boat_at_end();
		}
		// left of boat off
		if (GUI.Button(new Rect(320, button_y, button_width, button_height), "Off")) {
			action.left_off_boat();
		}
		// right of boat off
		if (GUI.Button(new Rect(320 + move_x, button_y, button_width, button_height), "Off")) {
			action.right_off_boat();
		}
		// go button
		if (GUI.Button(new Rect(350, button_y - 80, button_width, button_height), "GO!")) {
			action.drive();
		}
	}
}