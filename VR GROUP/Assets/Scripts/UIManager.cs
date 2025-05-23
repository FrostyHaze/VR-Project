using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject instructionsPanel; 
    public GameObject quizPanel;         
    public QuizManager quizManager;      

    public void StartQuiz()
    {
        instructionsPanel.SetActive(false); // Hide instructions
        quizPanel.SetActive(true);          // Show quiz UI
        quizManager.BeginQuiz();            // Start the quiz logic
    }
}
