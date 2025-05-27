using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable), typeof(Rigidbody), typeof(AudioSource))]
public class CabinetDoorInteraction : MonoBehaviour
{
    [Header("Door Settings")]
    [Tooltip("The Transform of the door leaf to rotate")]
    [SerializeField] private Transform doorTransform;
    [Tooltip("Local Y-angle to open to (e.g. 90 or 270)")]
    [SerializeField] private float openAngle = 270f;
    [Tooltip("Speed of the opening animation")]
    [SerializeField] private float animationSpeed = 5f;
    [Tooltip("Cooldown (s) before any door can be reopened")]
    [SerializeField] private float cooldownSeconds = 30f;

    [Header("Audio Settings")]
    [Tooltip("Door‐opening sound clip (3D)")]
    [SerializeField] private AudioClip openSfx;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable doorGrab;
    private AudioSource audioSource;
    private bool hasRotated = false;
    private bool isAnimating = false;

    // Static for a global cooldown across all doors
    private static float lastOpenTime = -Mathf.Infinity;

    void Awake()
    {
        // Grab and Rigidbody setup
        doorGrab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        doorGrab.trackPosition = false;
        doorGrab.trackRotation = false;
        GetComponent<Rigidbody>().isKinematic = true;

        // Subscribe to grab event
        doorGrab.selectEntered.AddListener(OnDoorGrabbed);

        // AudioSource setup
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;      // fully 3D
        audioSource.minDistance = 0.5f;
        audioSource.maxDistance = 5f;
    }

    void OnDestroy()
    {
        doorGrab.selectEntered.RemoveListener(OnDoorGrabbed);
    }

    void Update()
    {
        if (!isAnimating) return;

        // Smoothly rotate Y toward openAngle
        Vector3 current = doorTransform.localEulerAngles;
        float y = Mathf.LerpAngle(current.y, openAngle, Time.deltaTime * animationSpeed);
        doorTransform.localEulerAngles = new Vector3(current.x, y, current.z);

        // Stop when close enough
        if (Mathf.Abs(Mathf.DeltaAngle(y, openAngle)) < 0.1f)
            isAnimating = false;
    }

    private void OnDoorGrabbed(SelectEnterEventArgs args)
    {
        // Already opened or still on cooldown?
        if (hasRotated) return;
        if (Time.time < lastOpenTime + cooldownSeconds) return;

        // Play the door‐open SFX
        if (openSfx != null)
            audioSource.PlayOneShot(openSfx);

        // Begin opening animation
        hasRotated = true;
        isAnimating = true;

        // Start global cooldown
        lastOpenTime = Time.time;

        // Prevent further grabs
        doorGrab.enabled = false;
    }
}
