using Cinemachine;
using System.Collections;
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    [Header("Camera & UI")]
    [SerializeField] private CinemachineVirtualCamera computerCamera;
    [SerializeField] private CanvasGroup computerCanvas;

    [Header("Timings")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float delayBeforeFade = 0.5f;

    [SerializeField] ParticleSystem particle;

    private bool isInUse = false;
    private Coroutine fadeCoroutine;

    public void Interact()
    {
        if (isInUse) return;

        EnterComputer();
        if (particle != null)
            particle.Stop();
    }

    private void EnterComputer()
    {
        isInUse = true;
        Debug.Log("Entrou no computador.");
        ComputerManager.Instance.EnterComputer(this, computerCamera);
        FadeCanvas(true);
    }

    private void ExitComputer()
    {
        isInUse = false;
        Debug.Log("Saiu do computador.");
        ComputerManager.Instance.ExitComputer();
        particle.Play();
        FadeCanvas(false);
    }

    public void ExitComputerExternally()
    {
        isInUse = false;
        Debug.Log("Saiu do computador (externo).");
        FadeCanvas(false);
    }

    private void FadeCanvas(bool fadeIn)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasRoutine(fadeIn));
    }

    private IEnumerator FadeCanvasRoutine(bool fadeIn)
    {
        if (fadeIn)
        {
            yield return new WaitForSeconds(delayBeforeFade);
            computerCanvas.gameObject.SetActive(true);
            computerCanvas.blocksRaycasts = true;
            computerCanvas.interactable = true;
        }

        float start = fadeIn ? 0f : 1f;
        float end = fadeIn ? 1f : 0f;
        float time = 0f;

        while (time < fadeDuration)
        {
            computerCanvas.alpha = Mathf.Lerp(start, end, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        computerCanvas.alpha = end;

        if (!fadeIn)
        {
            computerCanvas.blocksRaycasts = false;
            computerCanvas.interactable = false;
            computerCanvas.gameObject.SetActive(false);
        }
    }

    public void OnExitComputerButton()
    {
        if (isInUse)
        {
            ExitComputer();
        }
    }
}
