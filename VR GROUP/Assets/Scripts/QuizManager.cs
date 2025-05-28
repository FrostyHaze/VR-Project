using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Question
{
    public string text;
    public int correctAnswer; // Index of correct target
}

public class QuizManager : MonoBehaviour
{
    public List<Question> questions = new List<Question>();
    public TMP_Text questionText;
    public TMP_Text timerText;
    public float timeLimit = 20f;
    public Color normalTimeColor = Color.white;
    public Color warningTimeColor = Color.red;
    public float warningThreshold = 5f;

    private int currentQuestionIndex = 0;
    private float currentTime;
    private bool quizActive = false;


    public SimpleDoorOpener doorOpener;

    void Start()
    {
        StartQuiz();
    }

    void Update()
    {
        if (!quizActive) return;

        currentTime -= Time.deltaTime;
        UpdateTimerDisplay();

        if (currentTime <= 0f)
        {
            HandleTimeExpired();
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {Mathf.Max(0f, currentTime):0.0}s";
            timerText.color = currentTime < warningThreshold ? warningTimeColor : normalTimeColor;
        }
    }

    public void StartQuiz()
    {
        currentQuestionIndex = 0;
        quizActive = true;
        currentTime = timeLimit;
        ShowQuestion();
    }

    void HandleTimeExpired()
    {
        Debug.Log("Time's up!");

        if (currentQuestionIndex >= questions.Count - 1)
        {
            
            EndQuiz();
        }
        else
        {
            
            currentQuestionIndex = 0;
            currentTime = timeLimit;
            ShowQuestion();
        }
    }
    void ShowQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            questionText.text = questions[currentQuestionIndex].text;
            UpdateTimerDisplay();
        }
        else
        {
            EndQuiz();
        }
    }

    void EndQuiz()
    {
        questionText.text = "Game Complete! Please Head out the Door";
        if (timerText != null) timerText.text = "";
        quizActive = false;

        if (doorOpener != null)
        {
            doorOpener.OpenDoor();
        }
    }

    public bool HandleTargetShot(int selectedAnswer)
    {
        if (!quizActive) return false;

        // If the correct answer is 0, that means the player should NOT shoot any target
        if (questions[currentQuestionIndex].correctAnswer == 0)
        {
            Debug.Log("This question requires no target to be shot. Shooting anything is wrong.");

            // Any target shot = wrong
            currentQuestionIndex = 0;
            currentTime = timeLimit;
            ShowQuestion();
            return false;
        }

        // For normal cases
        bool isCorrect = (selectedAnswer == questions[currentQuestionIndex].correctAnswer);

        if (isCorrect)
        {
            Debug.Log($"Correct answer! (Selected: {selectedAnswer}, Expected: {questions[currentQuestionIndex].correctAnswer})");
            currentQuestionIndex++;
            currentTime = timeLimit;

            if (currentQuestionIndex >= questions.Count)
            {
                EndQuiz();
            }
            else
            {
                ShowQuestion();
            }
        }
        else
        {
            Debug.Log($"Wrong answer! (Selected: {selectedAnswer}, Expected: {questions[currentQuestionIndex].correctAnswer})");
            currentQuestionIndex = 0;
            currentTime = timeLimit;
            ShowQuestion();
        }

        return isCorrect;
    }

   


    public int GetCurrentCorrectAnswerIndex()
    {
        if (currentQuestionIndex < questions.Count)
        {
            return questions[currentQuestionIndex].correctAnswer;
        }
        return -1;
    }
}