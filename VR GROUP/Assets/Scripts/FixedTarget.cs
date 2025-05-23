using UnityEngine;

public class FixedTarget : MonoBehaviour
{
    public int answerIndex; // Set this in the Inspector for each target

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            QuizManager quizManager = FindObjectOfType<QuizManager>();
            if (quizManager != null)
            {
                quizManager.HandleTargetShot(answerIndex);
            }
        }
    }
}
