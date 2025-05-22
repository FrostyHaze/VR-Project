using UnityEngine;

public class UnlockableDoor : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(1f, 0f, 0f); // How far to slide (e.g. right by 1 unit)
    public float openDuration = 1f; // Time to slide open

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;
    private float openTimer = 0f;

    private void Start()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + openOffset;
    }

    private void Update()
    {
        if (isOpening)
        {
            openTimer += Time.deltaTime;
            float t = Mathf.Clamp01(openTimer / openDuration);
            transform.localPosition = Vector3.Lerp(closedPosition, openPosition, t);
        }
    }

    public void UnlockDoor()
    {
        if (!isOpening)
        {
            isOpening = true;
            openTimer = 0f;
            Debug.Log("Door is sliding open!");
        }
    }
}