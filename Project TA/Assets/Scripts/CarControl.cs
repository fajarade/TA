using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControl : MonoBehaviour {

	float MotorF = -600;
	float BrakeF = 60000;
	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform [] tiremeshes = new Transform[4];
	public Transform centerOfMassBody;
	public GameObject SteerWheel;
	public Rigidbody car;
	bool Tran = true;
	public Text GearShift;
	public float accelerate;
	bool onetime = false;

	// Use this for initialization
	void Start () 
	{
		//GearShift.text = "Gear Shift : D";
		//car.centerOfMass = centerOfMassBody.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateWheelRotation();
		UpdateGear();
		UpdateSteerAngle();
	}

	void FixedUpdate()
	{
		accelerate = Input.GetAxis("Vertical") + Input.GetAxis("Vertical2");
		//print(wheelColliders[0].motorTorque);
		UpdateWheelAngle();
		
		if(Tran == true)
		{
			wheelColliders[0].brakeTorque = 0;
			wheelColliders[1].brakeTorque = 0;
			wheelColliders[2].brakeTorque = 0;
			wheelColliders[3].brakeTorque = 0;
			//print(Input.GetAxis("Vertical"));
			if(accelerate >= 0)
			{
				ForwardAccelerate();
			}
			else if(accelerate < 0)
			{
				Brake();
			}
		}

		else if (Tran == false)
		{
			BackwardAccelerate();
			if(accelerate >= 0)
			{
				wheelColliders[0].brakeTorque = 0;
				wheelColliders[1].brakeTorque = 0;
				wheelColliders[2].brakeTorque = 0;
				wheelColliders[3].brakeTorque = 0;
			}
			else if (accelerate < 0)
			{
				Brake();
			}
		}
	}

	void UpdateWheelRotation()
	{
		for(int i = 0;i < 4; i++)
		{
			Quaternion quat;
			Vector3 pos;
			wheelColliders[i].GetWorldPose(out pos, out quat);
			tiremeshes[i].position = pos;
			tiremeshes[i].rotation = quat;
		}
	}

	void UpdateGear()
	{
		bool maju = Input.GetButton("maju");
		bool mundur = Input.GetButton("mundur");
		if(maju)
		{
			Tran = true;
			GearShift.text = "GearShift : D";
		}

		else if(mundur)
		{
			Tran = false;
			GearShift.text = "GearShift : R";
		}
	}

	void UpdateSteerAngle()
	{
		float steer = Input.GetAxis("Horizontal") * -90;
		float posy = car.transform.eulerAngles.y;
		float posx = car.transform.eulerAngles.x;
		SteerWheel.transform.eulerAngles = new Vector3(posx, posy, steer);
	}

	void UpdateWheelAngle()
	{
		float steer = Input.GetAxis("Horizontal");
		float finalAngle = steer * -50f;
		wheelColliders[0].steerAngle = finalAngle;
		wheelColliders[1].steerAngle = finalAngle;
	}

	void ForwardAccelerate(){
		for (int i = 0; i < 4; i++)
		{
			wheelColliders[i].motorTorque = accelerate * MotorF;
		}
	}

	void Brake()
	{
		for (int i = 0; i < 4; i++) 
		{
			wheelColliders[i].brakeTorque = -accelerate * BrakeF;
		}

	}

	void BackwardAccelerate()
	{
		for (int i = 0; i < 4; i++) 
		{
			wheelColliders[i].motorTorque = -accelerate * MotorF;
		}
	}
}
