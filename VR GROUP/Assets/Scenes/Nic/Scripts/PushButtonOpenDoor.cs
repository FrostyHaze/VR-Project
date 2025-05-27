using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class PushButtonOpenDoor : MonoBehaviour
{
    [Header("Door Settings")]
    [Tooltip("The door GameObject to rotate when the button is pressed")]
    public GameObject doorToOpen;
    [Tooltip("Angle in degrees to rotate the door on Y when pressed")]
    public float openAngle = 90f;

    [Header("Audio Settings")]
    [Tooltip("Sound to play when button is pressed")]
    public AudioClip pressSfx;

    private bool doorOpened = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private AudioSource audioSource;

    void Awake()
    {
        // XR Grab setup
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.trackPosition = false;
        grabInteractable.trackRotation = false;
        grabInteractable.selectEntered.AddListener(OnGrab);

        // AudioSource setup
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;    // full 3D spatialization
        audioSource.minDistance = 0.2f;   // tweak to your scene scale
        audioSource.maxDistance = 3f;
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (doorOpened || doorToOpen == null)  
            return;

        // Play the button‐press sound
        if (pressSfx != null)
            audioSource.PlayOneShot(pressSfx);

        // Rotate the door
        doorToOpen.transform.Rotate(0f, openAngle, 0f, Space.World);
        doorOpened = true;
    }
}
