using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadZone = 0.025f;


    private bool isPressed; //checks if button is pressed only once so it doesnt repeatedly call isPressed every frame
    private Vector3 startPos; //determines how far button has moved during button press
    private ConfigurableJoint joint; //gets linear limit of joint
    public GameObject targetOne;
    public GameObject targetTwo;
    public GameObject targetThree;
    Vector3 targetOnePos;
    Vector3 targetTwoPos;
    Vector3 targetThreePos;

    public UnityEvent onPressed, onReleased;

    // Start is called before the first frame update
    void Start()
    {
        targetOnePos = new Vector3(4.177f, 2.421f, -10.505f);
        targetTwoPos = new Vector3(-0.901f, 3.412f, -15.092f);
        targetThreePos = new Vector3(-4.04343843f, 2.421f, -10.5430002f);

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

        Instantiate(targetOne, targetOnePos, Quaternion.identity);
        Instantiate(targetTwo, targetTwoPos, Quaternion.identity);
        Instantiate(targetThree, targetThreePos, Quaternion.identity);



    }
    //checks if button is released
    private void Released()
    {

        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released Button");





    }
}