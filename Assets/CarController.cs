// CarController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBrakeForce;
    private bool isBraking;

    // Tambahkan variabel ini untuk countdown
    public bool isCountdownComplete = false;

    // Settings
    [SerializeField] private float motorForce, brakeForce, maxSteerAngle;
    [SerializeField] private float maxSpeed = 50f;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheel Transforms
    [SerializeField] private Transform frontWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private Rigidbody carRigidbody;

    private void Start() {
        carRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (!isCountdownComplete)
        {
            // Jangan lakukan apapun sebelum countdown selesai
            return;
        }

        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor() {
        if (carRigidbody.velocity.magnitude < maxSpeed) {
            rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
            rearRightWheelCollider.motorTorque = verticalInput * motorForce;
        } else {
            rearLeftWheelCollider.motorTorque = 0;
            rearRightWheelCollider.motorTorque = 0;
        }

        currentBrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();
    }

    private void ApplyBraking() {
        frontWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels() {
        UpdateSingleWheel(frontWheelCollider, frontWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        
        // Pembatas posisi untuk menjaga agar roda tidak tumbang
        if (wheelCollider.isGrounded) {
            wheelTransform.position = new Vector3(pos.x, Mathf.Clamp(pos.y, 0, float.MaxValue), pos.z);
        } else {
            wheelTransform.position = pos; // Jika tidak menyentuh tanah, gunakan posisi normal
        }
    }
}
