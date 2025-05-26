using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    // called when a GameObject collides with the collider
    private void OnTriggerEnter(Collider other)
    {
        // check whether the player collided with the trigger
        if(other.tag == "Player")
        {
            // move to next scene
            SceneManagement.RandomiseScene();
            SceneManager.LoadScene(SceneManagement.current());
        }

    }
}
