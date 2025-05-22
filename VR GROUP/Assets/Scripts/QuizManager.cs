using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public int correctAnswer; // From 1 to 8
    }

    public Question[] questions;
    public Text questionTextUI;
    public Text timerTextUI; 
    public float questionTimeLimit = 20f;

    private int currentQuestionIndex = 0;
    private float timeRemaining;
    private bool timerRunning = false;

    private void Start()
    {
        StartQuiz();
    }

    public void StartQuiz()
    {
        currentQuestionIndex = 0;
        ShowQuestion();
    }

    public void ShowQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            questionTextUI.text = questions[currentQuestionIndex].questionText;
            timeRemaining = questionTimeLimit;
            timerRunning = true;
        }
        else
        {
            questionTextUI.text = "Quiz Complete!";
            timerTextUI.text = "";
            timerRunning = false;
        }
    }

    private void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;
            timerTextUI.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();

            if (timeRemaining <= 0f)
            {
                ResetQuiz("Time's up!");
            }
        }
    }

    public void CheckAnswer(int numberShot)
    {
        if (!timerRunning) return;

        if (questions[currentQuestionIndex].correctAnswer == numberShot)
        {
            currentQuestionIndex++;
            ShowQuestion();
        }
        else
        {
            ResetQuiz("Wrong answer!");
        }
    }

    private void ResetQuiz(string reason)
    {
        timerRunning = false;
        questionTextUI.text = reason + " Restarting...";
        timerTextUI.text = "";

        StartCoroutine(RestartAfterDelay(2f));
    }

    private IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartQuiz();
    }
}

