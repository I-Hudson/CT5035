using UnityEngine;
using Valve.VR;
using System.Collections;

public class ViveInput : MonoBehaviour
{
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
}