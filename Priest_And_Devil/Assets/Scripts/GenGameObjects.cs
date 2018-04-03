using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// generator gameobject in each frame

public class GenGameObjects : MonoBehaviour, ISceneController, IUserAction {

	// Collections to manage gameobject
	private Queue<GameObject> priest_begin;
	private Queue<GameObject> priest_end;

	private Queue<GameObject> devil_begin;
	private Queue<GameObject> devil_end;

	private Queue<GameObject> onTheWay;
	private GameObject ship;
	private enum ship_location {
		begin, end, moving_to_begin, moving_to_end
	}
	private ship_location ship_info;
	// the number of object in each side
	private int num_of_priest_at_the_begin;
	private int num_of_priest_at_the_end;
	private int num_of_devil_at_the_begin;
	private int num_of_devil_at_the_end;
	// position information
	private Vector3 land_begin_position;
	private Vector3 land_end_position;

	private Vector3 priest_begin_position;
	private Vector3 priest_end_position;

	private Vector3 devil_begin_position;
	private Vector3 devil_end_position;

	private Vector3 object_move;
	private Vector3 object_move_at_the_boat;

	private Vector3 on_ship_left_position;
	private Vector3 on_ship_right_positon;

	private Vector3 ship_begin_position;
	private Vector3 ship_end_position;

	public enum State
	{
		win, over, moving, normal
	}
	public State game_state;
	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		setPostion(priest_begin, priest_begin_position, object_move);
		setPostion(devil_begin, devil_begin_position, object_move);
		setPostion(priest_end, priest_end_position, object_move);
		setPostion(devil_end, devil_end_position, object_move);
		setPostion(onTheWay, on_ship_left_position, object_move_at_the_boat);

		if (ship_info == ship_location.moving_to_end) {
			float step = 3 * Time.deltaTime;
			ship.transform.position = Vector3.MoveTowards(ship.transform.position, ship_end_position, step);
			if (ship.transform.position == ship_end_position){
				ship_info = ship_location.end;
				game_state = State.normal;  
			}
		} else if (ship_info == ship_location.moving_to_begin) {
			float step = 3 * Time.deltaTime;
			ship.transform.position = Vector3.MoveTowards(ship.transform.position, ship_begin_position, step);
			if (ship.transform.position == ship_begin_position) {
				ship_info = ship_location.begin;
				game_state = State.normal;
			}
		} else {
			check();
		}
	}

	void Awake() {
		land_begin_position.Set(-6, 0, 0);
		land_end_position.Set(6, 0, 0);

		priest_begin_position.Set(-8, 1, 0);
		priest_end_position.Set(4, 1, 0);

		devil_begin_position.Set(-5.3f, 1, 0);
		devil_end_position.Set(6.5f, 1, 0);

		object_move.Set(1, 0, 0);
		object_move_at_the_boat.Set(0.5f, 0, 0);
		on_ship_left_position.Set(-0.3f, 1.3f, 0);
		on_ship_right_positon.Set(0.3f, 1.5f, 0);

		ship_begin_position.Set(-1.7f, 0, 0);
		ship_end_position.Set(1.7f, 0, 0);

		priest_begin = new Queue<GameObject>();
		priest_end = new Queue<GameObject>();
		devil_begin = new Queue<GameObject>();
		devil_end = new Queue<GameObject>();
		onTheWay = new Queue<GameObject>();

		game_state = State.normal;

		ship_info = ship_location.begin;
		SSDirector director = SSDirector.getInstance();
		director.currentSceneController = this;
		director.currentSceneController.LoadResources();
	}

	public void LoadResources() {
		// generate gameobject from prefabs
		GameObject land_left = Instantiate(Resources.Load("Prefabs/land") as GameObject, land_begin_position, Quaternion.identity);
		GameObject land_right = Instantiate(Resources.Load("Prefabs/land") as GameObject, land_end_position, Quaternion.identity);
		ship = Instantiate(Resources.Load("Prefabs/ship") as GameObject, ship_begin_position, Quaternion.identity);
		for (int i = 0; i < 3; ++i) {
			devil_begin.Enqueue(Instantiate(Resources.Load("Prefabs/devil"), devil_begin_position + i * object_move, Quaternion.identity) as GameObject);
			priest_begin.Enqueue(Instantiate(Resources.Load("Prefabs/priest"), priest_begin_position + i * object_move, Quaternion.identity) as GameObject);
		}
		Debug.Log("in LoadResources");
		Debug.Log(priest_begin.Count);
	}
	// set object postion on each update()
	// implement it later
	void setPostion(Queue<GameObject> obj_queue, Vector3 first, Vector3 move) {
		GameObject[] temp = obj_queue.ToArray();
		for (int i = 0; i < temp.Length; ++i) {
			temp[i].transform.localPosition = first + i * move;
		}
	}
	// get on the boat
	public void priest_to_boat_at_begin() {
		if (priest_begin.Count > 0 && onTheWay.Count < 2 && ship_info == ship_location.begin) {
			GameObject temp = priest_begin.Dequeue();
			temp.transform.parent = ship.transform;
			onTheWay.Enqueue(temp);
		}
	}
	public void devil_to_boat_at_begin() {
		if (devil_begin.Count > 0 && onTheWay.Count < 2 && ship_info == ship_location.begin) {
			GameObject temp = devil_begin.Dequeue();
			temp.transform.parent = ship.transform;
			onTheWay.Enqueue(temp);
		}
	}
	public void priest_to_boat_at_end() {
		if (priest_end.Count > 0 && onTheWay.Count < 2 && ship_info == ship_location.end) {
			GameObject temp = priest_end.Dequeue();
			temp.transform.parent = ship.transform;
			onTheWay.Enqueue(temp);
		}
	}
	public void devil_to_boat_at_end() {
		if (devil_end.Count > 0 && onTheWay.Count < 2 && ship_info == ship_location.end) {
			GameObject temp = devil_end.Dequeue();
			temp.transform.parent = ship.transform;
			onTheWay.Enqueue(temp);
		}
	}
	// get off the boat
	public void left_off_boat() {
		if (onTheWay.Count > 0) {
			GameObject temp = onTheWay.Dequeue();
			getOffTheBoat(temp);
		}
	}

	public void right_off_boat() {
		if (onTheWay.Count > 1) {
			GameObject temp_1 = onTheWay.Dequeue();
			GameObject temp_2 = onTheWay.Dequeue();
			onTheWay.Enqueue(temp_1);
			getOffTheBoat(temp_2);
		}
	}
	public void drive() {
		if (onTheWay.Count > 0) {
			if (ship_info == ship_location.begin) {
				ship_info = ship_location.moving_to_end;
			} else if (ship_info == ship_location.end) {
				ship_info = ship_location.moving_to_begin;
			}
			game_state = State.moving;
		}
	}
	// private void getOnTheBoat(GameObject obj) {

	// }
	private void getOffTheBoat(GameObject obj) {
		obj.transform.parent = null;
		if (ship_info == ship_location.begin) {
			if (obj.tag == "devil") {
				devil_begin.Enqueue(obj);
			} else {
				priest_begin.Enqueue(obj);
			}
		} else {
			if (obj.tag == "devil" ) {
				devil_end.Enqueue(obj);
			} else {
				priest_end.Enqueue(obj);
			}
		}
	}

	void count_num_of_object() {
		num_of_devil_at_the_begin = devil_begin.Count;
		num_of_devil_at_the_end = devil_end.Count;
		num_of_priest_at_the_begin = priest_begin.Count;
		num_of_priest_at_the_end = priest_end.Count;
		GameObject[] temp = onTheWay.ToArray();
		foreach (GameObject item in onTheWay)
		{
			if (item.tag == "priest") {
				if (ship_info == ship_location.begin) {
					num_of_priest_at_the_begin++;
				} else {
					num_of_priest_at_the_end++;
				}
			} else {
				if (ship_info == ship_location.begin) {
					num_of_devil_at_the_begin++;
				} else {
					num_of_devil_at_the_end++;
				}
			}
		}
	}
	private void check() {
		count_num_of_object();
		// check at the begin
		if (num_of_devil_at_the_begin > num_of_priest_at_the_begin && num_of_priest_at_the_begin != 0) {
			game_state = State.over;
		} else if (num_of_devil_at_the_end > num_of_priest_at_the_end && num_of_priest_at_the_end != 0) {
			game_state = State.over;
		} else if (num_of_devil_at_the_end + num_of_priest_at_the_end == 6) {
			game_state = State.win;
		}
	}

	public void reset() {
		Application.LoadLevel(Application.loadedLevelName);  
		game_state = State.normal;
	}
}