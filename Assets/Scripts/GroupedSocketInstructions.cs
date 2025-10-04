using DG.Tweening;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GroupedSocketInstructions : MonoBehaviour
{
    [System.Serializable]
    public class Step
    {
        public XRSocketInteractor[] sockets;  // Required sockets for this step
        public AudioClip instructionClip;     // Instruction audio for this step
    }

    [Header("Instructor Settings")]
    public AudioSource audioSource;
    public Animator instructorAnimator;
    public string talkAnim = "Talk";
    public string idleAnim = "Idle";

    [Header("Steps Settings")]
    public Step[] steps;                     // All steps data

    [Header("Final Sound (After All Steps)")]
    public AudioClip finalClip;              // Final audio after all steps

    private int currentStep = 0;
    private int socketsFilled = 0;

    void Start()
    {
        // Add listener to all sockets
        foreach (var step in steps)
        {
            foreach (var socket in step.sockets)
                socket.selectEntered.AddListener(_ => OnSocketFilled(socket));
        }
    }

    void OnSocketFilled(XRSocketInteractor socket)
    {
        // If all steps done, stop here
        if (currentStep >= steps.Length) return;

        var step = steps[currentStep];
        socketsFilled++;

        // Check if all sockets for this step are filled
        if (socketsFilled >= step.sockets.Length)
        {
            socketsFilled = 0;

            PlayInstruction(step.instructionClip);

            currentStep++;

            // Play final sound ONLY when all steps are done
            if (currentStep >= steps.Length && finalClip)
            {
                Invoke(nameof(PlayFinalSound), step.instructionClip ? step.instructionClip.length + 0.5f : 0f);
            }
        }
    }

    // Play instruction voice
    void PlayInstruction(AudioClip clip)
    {
        if (!clip) return;
        audioSource.clip = clip;
        audioSource.Play();
        instructorAnimator.Play(talkAnim);
        Invoke(nameof(StopTalking), clip.length);
    }

    // Return to idle after talking
    void StopTalking()
    {
        instructorAnimator.Play(idleAnim);
    }

    // Play final sound when all steps are done
    void PlayFinalSound()
    {
        audioSource.clip = finalClip;
        audioSource.Play();
        instructorAnimator.Play(talkAnim);
        Invoke(nameof(StopTalking), finalClip.length);
        Debug.Log(" All training steps completed!");
    }
}
