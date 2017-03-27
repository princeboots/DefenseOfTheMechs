﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float maxSpeed = 2f;

	private int pointNumber;
	private float distanceToWaypoint;
	private float pointRadius = 0.02f;
	[SerializeField] private Waypoint nextWaypoint;
	

	void Start () {
		pointNumber = 0;
		nextWaypoint = WaypointManager.levelWaypoints[pointNumber];
	}
	
	void Update () {
		LookAtWaypoint();
		MoveByWaypoints();
		
	}

	void MoveByWaypoints() {
		distanceToWaypoint = Vector2.Distance(transform.position, nextWaypoint.transform.position);
		if (distanceToWaypoint <= pointRadius) {
			if (pointNumber+1 >= WaypointManager.levelWaypoints.Length) {
				Destroy(gameObject);
			} else {
				nextWaypoint = WaypointManager.levelWaypoints[++pointNumber];
			}
		}
		MoveToNextWayPoint();
	}

	void MoveToNextWayPoint() {
		float step = maxSpeed * Time.deltaTime;
		transform.position = Vector2.MoveTowards(transform.position, nextWaypoint.transform.position, step);
	}
	void LookAtWaypoint() {
		transform.up = Vector3.Slerp(transform.up,(nextWaypoint.transform.position - transform.position),Time.fixedDeltaTime*4f);
		transform.rotation *= Quaternion.Euler(0,0,1);
	}
}
