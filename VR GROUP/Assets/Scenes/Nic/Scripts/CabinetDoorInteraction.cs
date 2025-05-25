// VR Unity: Cabinet Door rotates once then locks, global 30s cooldown for all lockers

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable), typeof(Rigidbody))]
public class CabinetDoorInteraction : MonoBehaviour
{
    [SerializeField] private Transform doorTransform;
    [SerializeField] private float openAngle = 270f;
    [SerializeField] private float animationSpeed = 5f;
    [SerializeField] private float cooldownSeconds = 30f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable doorGrab;
    private bool hasRotated = false;
    private bool isAnimating = false;

    // Static variables to enforce global cooldown across all lockers
    private static float lastOpenTime = -Mathf.Infinity;
 
    void Awake()
    {
        doorGrab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Prevent positional movement and rotation by controller
        doorGrab.trackPosition = false;
        doorGrab.trackRotation = false;

        // Disable physics movement
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        // Subscribe to grab event
        doorGrab.selectEntered.AddListener(OnDoorGrabbed);
    }

    void OnDestroy()
    {
        doorGrab.selectEntered.RemoveListener(OnDoorGrabbed);
    }

    void Update()
    {
        if (isAnimating)
        {
            float target = openAngle;
            Vector3 current = doorTransform.localEulerAngles;
            float y = Mathf.LerpAngle(current.y, target, Time.deltaTime * animationSpeed);
            doorTransform.localEulerAngles = new Vector3(current.x, y, current.z);

            if (Mathf.Abs(Mathf.DeltaAngle(y, target)) < 0.1f)
                isAnimating = false;
        }
    }

    private void OnDoorGrabbed(SelectEnterEventArgs args)
    {
        // Check individual usage and global cooldown
        if (hasRotated) return;
        if (Time.time < lastOpenTime + cooldownSeconds) return;

        // Start animation to openAngle
        hasRotated = true;
        isAnimating = true;

        // Set global cooldown
        lastOpenTime = Time.time;

        // Disable further grabs for this door
        doorGrab.enabled = false;
    }
}