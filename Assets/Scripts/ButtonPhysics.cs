using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPhysics : MonoBehaviour
{
    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadZone = 0.025f;


    private bool isPressed; //checks if button is pressed only once so it doesnt repeatedly call isPressed every frame
    private Vector3 startPos; //determines how far button has moved during button press
    private ConfigurableJoint joint; //gets linear limit of joint
    public GameObject ball;
    Vector3 ballPos;

    public UnityEvent onPressed, onReleased;

    // Start is called before the first frame update
    void Start()
    {
        ballPos = new Vector3(0.0710000023f, 0.640999973f, -6.48699999f);

        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + threshold >= 1) //determines if button is being pressed for the first time / first frame 
            Pressed();

        if (isPressed && GetValue() - threshold <= 0) //determines if button is being released / last frame
            Released();
    }
    //determins precentile value of button press in order to determine whether or not the button is being pressed 
    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone)
            value = 0;


        return Mathf.Clamp(value, -1f, 1f);
    }
    //checks if button is pressed
    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed Button");

        Instantiate(ball, ballPos, Quaternion.identity);
    }
    //checks if button is released
    private void Released()
    {

        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released Button");

        



    }
}

   

