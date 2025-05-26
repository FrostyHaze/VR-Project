using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class filmProjectorActivate : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;

    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable expectedFilmTape;

    public GameObject[] objectsToActivate;

    public AudioSource projectorSound; 


    private void OnEnable()
    {
        socketInteractor.selectEntered.AddListener(OnFilmInserted);
        socketInteractor.selectExited.AddListener(OnFilmRemoved);
    }

    private void OnDisable()
    {
        socketInteractor.selectEntered.RemoveListener(OnFilmInserted);
        socketInteractor.selectExited.RemoveListener(OnFilmRemoved);
    }

    private void OnFilmInserted(SelectEnterEventArgs args)
    {
        if (args.interactableObject == expectedFilmTape)
        {
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
                projectorSound.Play();
            }
        }
    }

    private void OnFilmRemoved(SelectExitEventArgs args)
    {
        if (args.interactableObject == expectedFilmTape)
        {
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(false);
                projectorSound.Stop();
            }
        }
    }
}
