using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhysics : MonoBehaviour
{
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    private Transform _followTarget;
    private Rigidbody _body;




    void Start()
    {
        //physics movement
        _followTarget = followObject.transform;
        _body = GetComponent<Rigidbody>();
        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.interpolation = RigidbodyInterpolation.Interpolate;
        _body.mass = 20f;

        //teleport hands to controllers on start
        _body.position = _followTarget.position;
        _body.rotation = _followTarget.rotation;


    }

    // Update is called once per frame
    void Update()
    {
        PhysicsMove(); 



    }


    private void PhysicsMove()
    {
        //position
        var positionWithOffset = _followTarget.position + positionOffset;

        var distance = Vector3.Distance(positionWithOffset, transform.position);
        _body.velocity = (positionWithOffset - transform.position).normalized * followSpeed * distance;

        //rotation | "q" stands for Quaternion
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);

        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotationSpeed);
    }


}



