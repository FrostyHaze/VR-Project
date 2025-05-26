using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class keypadManager : MonoBehaviour
{
    public TextMeshPro displayText;   // Assign in Inspector
    public string correctCode = "428"; // Set your password here
    public AudioSource doorSound;

    [Header("Events")]
    public UnityEvent onCorrectCode;
    public UnityEvent onIncorrectCode;

    private string currentCode = "";

    public void PressKey(string key)
    {
        if (key == "Del")
        {
            if (currentCode.Length > 0)
                currentCode = currentCode.Substring(0, currentCode.Length - 1);
        }
        else if (currentCode.Length < 3)
        {
            currentCode += key;
            if (currentCode.Length == 3)
                CheckCode();
        }

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        displayText.text = currentCode;
    }

    private void CheckCode()
    {
        if (currentCode == correctCode)
        {
            Debug.Log("Correct Code Entered!");
            onCorrectCode.Invoke();
            doorSound.Play();

        }
        else
        {
            Debug.Log("Incorrect Code.");
            onIncorrectCode.Invoke();
            ClearCode();
        }
    }

    private void ClearCode()
    {
        currentCode = "";
        UpdateDisplay();
    }
}