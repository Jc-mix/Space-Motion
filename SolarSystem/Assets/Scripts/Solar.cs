using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solar : MonoBehaviour {

	public Transform sun;
	public Transform Mercury;
	public Transform Venus;
	public Transform Earth;
	public Transform Mars;
	public Transform Jupiter;
	public Transform Saturn;
	public Transform Uranus;
	public Transform Neptune;
	float RandomX, RandomY, RandomZ;

	// Use this for initialization
	void Start () {
		sun.position = Vector3.zero;
		Mercury.position = new Vector3 (0, 1, 1);
		Venus.position = new Vector3 (0, 2, 2);
		Earth.position = new Vector3 (0, 3, 3);
		Mars.position = new Vector3 (0, 4, 4);
		Jupiter.position = new Vector3 (0, 5, 5);
		Saturn.position = new Vector3 (0, 6, 6);
		Uranus.position = new Vector3 (0, 7, 7);
		Neptune.position = new Vector3 (0, 8, 8);
		RandomX = Random.Range (1, 360);
		RandomY = Random.Range (1, 360);
		RandomY = Random.Range (1, 360);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 axis = new Vector3 (RandomX, RandomY, RandomZ);
		Mercury.RotateAround (sun.position, axis, 80 * Time.deltaTime);
		Mercury.Rotate (Vector3.up * 30 * Time.deltaTime);
		Venus.RotateAround (sun.position, Vector3.back, 70 * Time.deltaTime);
		Venus.Rotate (Vector3.up * 30 * Time.deltaTime);
		Earth.RotateAround (sun.position, Vector3.down, 60 * Time.deltaTime);
		Earth.Rotate (Vector3.up * 30 * Time.deltaTime);
		Mars.RotateAround (sun.position, Vector3.down, 50 * Time.deltaTime);
		Mars.Rotate (Vector3.up * 30 * Time.deltaTime);
		Jupiter.RotateAround (sun.position, axis, 40 * Time.deltaTime);
		Jupiter.Rotate (Vector3.up * 30 * Time.deltaTime);
		Saturn.RotateAround (sun.position, Vector3.forward, 30 * Time.deltaTime);
		Saturn.Rotate (Vector3.up * 30 * Time.deltaTime);
		Uranus.RotateAround (sun.position, axis, 20 * Time.deltaTime);
		Uranus.Rotate (Vector3.up * 30 * Time.deltaTime);
		Neptune.RotateAround (sun.position, Vector3.up, 10 * Time.deltaTime);
		Neptune.Rotate (Vector3.up * 30 * Time.deltaTime);
	}
}
