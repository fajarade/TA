﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 5000f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public float maxMotorTorque = 8000f;
    public float currentSpeed;
    public float maxSpeed = 30000f;

    private List<Transform> nodes;
    private int currectNode = 0;

	private void Start () {
		Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++){
            if (pathTransforms[i] != path.transform){
                nodes.Add(pathTransforms[i]);
            }
        }
        //Debug.Log(nodes.Count.ToString());
	}

    private void FixedUpdate() {
        ApplySteer();
        Drive();
        CheckWaypointDistance();
    }

    private void ApplySteer(){
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
	}

    private void Drive() {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed) {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        } else {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    private void CheckWaypointDistance() {
        Debug.Log(Vector3.Distance(transform.position, nodes[currectNode].position).ToString());
        if(Vector3.Distance(transform.position, nodes[currectNode].position) < 100f) {
            //Debug.Log("waca");
            if(currectNode == nodes.Count - 1) {
                //Debug.Log("olala");
                currectNode = 0;
            } else {
                currectNode++;
            }

            //Debug.Log(currectNode.ToString());
        }
    }
}
