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
    public float timeLimit = 20f;

    private int currentQuestionIndex = 0;
    private float timer;
    private bool quizActive = false;

    void Start()
    {
        StartQuiz();
    }

    void Update()
    {
        if (!quizActive) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Debug.Log("Time's up!");
            ResetQuiz();
        }
    }
    public void BeginQuiz()
    {
        currentQuestionIndex = 0;
        quizActive = true;
        ShowQuestion();
    }
    void StartQuiz()
    {
        currentQuestionIndex = 0;
        quizActive = true;
        ShowQuestion();
    }

    void ResetQuiz()
    {
        currentQuestionIndex = 0;
        ShowQuestion();
        timer = timeLimit;
    }

    void ShowQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            questionText.text = questions[currentQuestionIndex].text;
            timer = timeLimit;
        }
        else
        {
            questionText.text = "Quiz Complete!";
            quizActive = false;
        }
    }

    public void HandleTargetShot(int selectedAnswer)
    {
        if (!quizActive) return;

        if (selectedAnswer == questions[currentQuestionIndex].correctAnswer)
        {
            currentQuestionIndex++;
            ShowQuestion();
        }
        else
        {
            Debug.Log("Wrong answer, resetting...");
            ResetQuiz();
        }
    }
}
