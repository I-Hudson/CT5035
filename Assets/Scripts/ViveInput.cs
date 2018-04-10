using UnityEngine;
using Valve.VR;
using System.Collections;

public class ViveInput : MonoBehaviour
{
<<<<<<< HEAD
	public SteamVR_TrackedObject mTrackedObject = null;
	public SteamVR_Controller.Device mDevice;
	public Vector3 vLastPosition;

	public FixedJoint ControllerJoint;

	public GameObject ObjectToHold;
	private Rigidbody ObjectToHoldRigidBody;
	public bool CurrentlyThrowing = true;

	void Awake()
	{
		mTrackedObject = GetComponent<SteamVR_TrackedObject>();
	}

	void Update()
	{
		mDevice = SteamVR_Controller.Input((int)mTrackedObject.index);

		#region Trigger

		//Down
		if (mDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			print("Trigger Down");
		}

		//Up
		if (mDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			print("Trigger Up");
		}

		//Value
		Vector2 triggerValue = mDevice.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger);

		#endregion

		#region Grip

		//Down
		if (mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			print("Grip Down");

		}

		//Up
		if (mDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
			print("Grip Up");
		}

		#endregion

		#region Touchpad

		//Down
		if (mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
		{
			print("Touchpad Down");
		}

		//Up
		if (mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
		{
			print("Touchpad Up");
		}

		//Value
		Vector2 touchValue = mDevice.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

		#endregion


	}

	private void FixedUpdate()
	{
		if (CurrentlyThrowing)
		{
			Transform ObjectPos;
			if (mTrackedObject.origin != null)
			{
				ObjectPos = mTrackedObject.origin;
			}
			else
			{
				ObjectPos = mTrackedObject.transform.parent;
			}


			if (ObjectPos != null)
			{
				ObjectToHoldRigidBody.velocity = ObjectPos.TransformVector(mDevice.velocity);
				ObjectToHoldRigidBody.angularVelocity = ObjectPos.TransformVector(mDevice.angularVelocity);
			}
			else
			{
				ObjectToHoldRigidBody.velocity = mDevice.velocity;
				ObjectToHoldRigidBody.angularVelocity = mDevice.angularVelocity;
			}
			ObjectToHoldRigidBody.maxAngularVelocity = ObjectToHoldRigidBody.angularVelocity.magnitude;

			CurrentlyThrowing = false;
		}
	}

	void OnTriggerStay(Collider other)
	{

		if (other.CompareTag("GripBox"))//If the collider is pickupable
		{
			ObjectToHold = other.gameObject;
		}


		if (ObjectToHold != null && mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))//Grab
		{
			ControllerJoint.connectedBody = ObjectToHold.GetComponent<Rigidbody>();
			CurrentlyThrowing = false;
			ObjectToHoldRigidBody = null;
		}


		else if (ObjectToHold != null && mDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip))//Let go
		{
			ObjectToHoldRigidBody = ControllerJoint.connectedBody;
			ControllerJoint.connectedBody = null;
			CurrentlyThrowing = true;
		}
		else
		{
			ControllerJoint.connectedBody = null;
		}
	}
	void OnTriggerExit(Collider other)
	{
		ObjectToHold = null;
	}
=======
    public SteamVR_TrackedObject mTrackedObject = null;
    public SteamVR_Controller.Device mDevice;
    public Vector3 vLastPosition;

    void Awake()
    {
        mTrackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        mDevice = SteamVR_Controller.Input((int)mTrackedObject.index);

        #region Trigger

        //Down
        if (mDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            print("Trigger Down");
        }

        //Up
        if (mDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            print("Trigger Up");
        }

        //Value
        Vector2 triggerValue = mDevice.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger);

        #endregion

        #region Grip

        //Down
        if (mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            print("Grip Down");

        }

        //Up
        if (mDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            print("Grip Up");
        }

        #endregion

        #region Touchpad

        //Down
        if (mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            print("Touchpad Down");
        }

        //Up
        if (mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            print("Touchpad Up");
        }

        //Value
        Vector2 touchValue = mDevice.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

        #endregion

        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GripBox") && mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            other.transform.parent = this.transform;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (other.CompareTag("GripBox") && mDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            other.transform.parent = GameObject.Find("The room").transform;
            other.GetComponent<Rigidbody>().isKinematic = false;

            
        }

        vLastPosition = other.transform.position;
        //parent
    }
>>>>>>> 2e79da2c257cb35455ea15cb978ba1e98a569d73
}