using Leap;
using UnityEngine;

public class HandInput : MonoBehaviour
{
    [SerializeField] private LeapProvider lp;
    public Quaternion rightHandRotation { get; private set; }
    public Vector3 rightHandPosition { get; private set; }
    public Quaternion leftHandRotation { get; private set; }
    public Vector3 leftHandPosition { get; private set; }
    public bool isGrabbingLeft;
    public bool isGrabbingRight { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        lp = Hands.Provider;

    }

    // Update is called once per frame
    void Update()
    {
        var rightHand = lp.GetHand(Chirality.Right);
        if (rightHand != null)
        {
            rightHandRotation = rightHand.Rotation;
            rightHandPosition = rightHand.PalmPosition;
            isGrabbingRight = rightHand.GrabStrength > 0.65f;
        }

        var leftHand = lp.GetHand(Chirality.Left);
        if (leftHand != null)
        {
            leftHandRotation = leftHand.Rotation;
            leftHandPosition = leftHand.PalmPosition;
            isGrabbingLeft = leftHand.GrabStrength > 0.65f;
        }
    }
}
