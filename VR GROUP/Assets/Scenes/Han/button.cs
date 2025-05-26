using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class button : MonoBehaviour
{
    public string keyValue;                   
    public keypadManager keypadManager;
    public AudioSource buttonSound;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (keypadManager != null)
        {
            keypadManager.PressKey(keyValue);
            buttonSound.Play();
        }
        else
        {
            Debug.LogWarning("KeypadManager not assigned on " + gameObject.name);
        }
    }

    private void OnDestroy()
    {
        // Clean up the listener to avoid memory leaks
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnButtonPressed);
        }
    }
}