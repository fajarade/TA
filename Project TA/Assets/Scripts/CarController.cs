using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CarController : MonoBehaviour {

	public GameObject wheel_FR;
	public GameObject wheel_FL;
	public GameObject wheel_BR;
	public GameObject wheel_BL;

	public WheelCollider FR;
	public WheelCollider FL;
	public WheelCollider BR;
	public WheelCollider BL;

	Rigidbody rb;
	Vector3 temp;
	Vector3 temp1;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		temp=rb.centerOfMass;
		temp.x = 0f;
		temp.y = -0.8f;
		temp.z = 0f;
		rb.centerOfMass = temp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		Move_Car();
		Steer_Wheels();
	}

	void Move_Car(){
		//fungsi untuk bergerak atas bawah (maju mundur)
		BR.motorTorque=5000*Input.GetAxis("Vertical");
		BL.motorTorque=5000*Input.GetAxis("Vertical");

		//fungsi hanya untuk bergerak kiri dan kanan
		FR.steerAngle=30*Input.GetAxis("Horizontal");
		FL.steerAngle=30*Input.GetAxis("Horizontal");
	}

	void Steer_Wheels(){
		//this is Front right wheel swapping of steer Angle to wheels...
		temp1 = wheel_FR.transform.localEulerAngles;
		temp1.y = FR.steerAngle;
		wheel_FR.transform.localEulerAngles = temp1;

		//this is front left swapping of steer Angle...
		temp1 = wheel_FR.transform.localEulerAngles;
		temp1.y = FL.steerAngle;
		wheel_FL.transform.localEulerAngles = temp1;
	}

}