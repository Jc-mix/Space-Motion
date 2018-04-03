using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction {
	void reset();
	void drive();

	void priest_to_boat_at_begin();
	void devil_to_boat_at_begin();
	void priest_to_boat_at_end();
	void devil_to_boat_at_end();

	void left_off_boat();
	void right_off_boat();
}