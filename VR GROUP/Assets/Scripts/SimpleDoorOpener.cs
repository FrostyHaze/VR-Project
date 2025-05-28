using UnityEngine;

public class SimpleDoorOpener : MonoBehaviour
{
    public Vector3 openPosition = new Vector3(-21.3339996f, -2.01999998f, -4.04799986f);
    public Vector3 openRotationEuler = Vector3.zero; // set in inspector
    private Vector3 closedPosition;
    private Quaternion closedRotation;

    void Start()
    {
        closedPosition = transform.position;
        closedRotation = transform.localRotation;
    }

    public void OpenDoor()
    {
        transform.position = openPosition;
        transform.localRotation = Quaternion.Euler(openRotationEuler);
    }
}
