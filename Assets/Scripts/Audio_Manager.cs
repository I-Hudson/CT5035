using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour {

    Vector3 mouseRef = new Vector3();
    public AudioSource Mic1;
    public AudioSource Mic2;
    public AudioSource Mic3;
    public AudioSource Mic4;
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawLine(transform.position, transform.forward, Color.red);

        float knobAngle = Vector3.Angle(Vector3.up, transform.forward);
        print(knobAngle);

        if (knobAngle > 0 && knobAngle < 45)
        {
            Mic1.mute = false;
        }
        else
        {
            Mic1.mute = true;
        }

        if (knobAngle > 45 && knobAngle < 90)
        {
            Mic2.mute = false;
        }
        else
        {
            Mic2.mute = true;
        }

        if (knobAngle > 90 && knobAngle < 135)
        {
            Mic3.mute = false;
        }
        else
        {
            Mic3.mute = true;
        }

        if (knobAngle > 135 && knobAngle < 180)
        {
            Mic4.mute = false;
        }
        else
        {
            Mic4.mute = true;
        }
    }

    void OnMouseDown ()
    {
        mouseRef = Input.mousePosition;
    }

    void OnMouseDrag ()
    {
        Vector3 offset = (Input.mousePosition - mouseRef);
        transform.Rotate(new Vector3(0, offset.x * 0.005f, 0));
    }
}
