using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parabola : MonoBehaviour {

	public float Power = 10;//the power 
	public float Angle = 60;//the angle
	public float Gravity = -10;//g

	private Vector3 MoveSpeedStart;//vector of the start speed
	private Vector3 MoveSpeed;
	private Vector3 GravitySpeed = Vector3.zero;//vector of g
	private float time;
	private Vector3 currentAngle;
	Vector3 start = new Vector3 (0, 0, 0);
	Vector3 target = new Vector3(6, 0, 0);
	Vector3 upd = new Vector3(0, 6, 0);
	Vector3 downd = new Vector3(0, -6, 0);
	float speed = 1;


	// Use this for initialization
	void Start () {
		MoveSpeedStart = Quaternion.Euler (new Vector3 (0, 0, Angle)) * Vector3.right * Power;
	}



	// Update is called once per frame	
	void Update () {
		//compute the GravitySpeed
		GravitySpeed.y = Gravity * (time += Time.fixedDeltaTime);
		//Move
		//transform.position += (MoveSpeedStart + GravitySpeed) * Time.fixedDeltaTime;
		/*transform.Translate(MoveSpeedStart * Time.fixedDeltaTime);
		transform.Translate(GravitySpeed * Time.fixedDeltaTime);*/
		Vector3 change = new Vector3( Time.deltaTime*5, -Time.deltaTime*(speed/10), 0);  
		this.transform.position += change;  
		speed++; 
	}
}