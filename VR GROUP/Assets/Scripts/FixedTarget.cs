using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FixedTarget : MonoBehaviour
{
    public int answerIndex; // The answer value this target represents
    public float cooldownTime = 0.5f;

    [Header("Feedback Settings")]
    public float flashDuration = 0.3f;
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;

    [Header("Sound Feedback")]
    public AudioClip correctSound;
    public AudioClip incorrectSound;

    private Renderer targetRenderer;
    private Material originalMaterial;
    private float lastHitTime;
    private bool isFlashing;

    private AudioSource audioSource;

    void Start()
    {
        targetRenderer = GetComponent<Renderer>();
        originalMaterial = targetRenderer.material;

        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Called when projectile hits this target
    public void HandleHit()
    {
        if (Time.time < lastHitTime + cooldownTime || isFlashing) return;

        QuizManager quizManager = FindObjectOfType<QuizManager>();
        if (quizManager != null)
        {
            // Pass our answerIndex to the quiz manager
            bool isCorrect = quizManager.HandleTargetShot(answerIndex);
            lastHitTime = Time.time;

            // Play sound
            if (isCorrect && correctSound != null)
                audioSource.PlayOneShot(correctSound);
            else if (!isCorrect && incorrectSound != null)
                audioSource.PlayOneShot(incorrectSound);

            StartCoroutine(FlashColor(isCorrect));
        }
    }

    IEnumerator FlashColor(bool isCorrect)
    {
        isFlashing = true;

        if (targetRenderer != null)
        {
            Material flashMaterial = new Material(targetRenderer.material);
            targetRenderer.material = flashMaterial;
            targetRenderer.material.color = isCorrect ? correctColor : incorrectColor;

            yield return new WaitForSeconds(flashDuration);

            targetRenderer.material = originalMaterial;
            Destroy(flashMaterial);
        }

        isFlashing = false;
    }
}
