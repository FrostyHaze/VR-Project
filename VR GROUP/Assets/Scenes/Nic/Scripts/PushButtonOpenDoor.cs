using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButtonOpenDoor : MonoBehaviour
{
    public GameObject doorToOpen; // Assign your door here
    public float openAngle = 90f;

    private bool doorOpened = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.trackPosition = false;
        grabInteractable.trackRotation = false;

        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (!doorOpened && doorToOpen != null)
        {
            doorToOpen.transform.Rotate(0f, openAngle, 0f, Space.World);
            doorOpened = true;
        }
    }
}
