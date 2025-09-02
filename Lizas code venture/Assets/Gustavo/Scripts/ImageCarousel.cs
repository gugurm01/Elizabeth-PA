using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCarousel : MonoBehaviour
{
    [Header("Configurações")]
    public List<Sprite> imagens;
    public Image imageUI;
    public float tempoEntreImagens = 3f;
    public float tempoFade = 1f;
    public float movimentoAmplitude = 10f;
    public float movimentoVelocidade = 1f;

    private int indiceAtual = 0;
    private RectTransform imageTransform;

    void Start()
    {
        if (imagens.Count == 0 || imageUI == null)
        {
            Debug.LogWarning("Configura as imagens e a Image UI.");
            enabled = false;
            return;
        }

        imageTransform = imageUI.GetComponent<RectTransform>();
        imageUI.sprite = imagens[indiceAtual];
        imageUI.color = new Color(1, 1, 1, 1);

        StartCoroutine(TrocarImagens());
    }

    void Update()
    {
        if (imageTransform != null)
        {
            float offsetY = Mathf.Sin(Time.time * movimentoVelocidade) * movimentoAmplitude;
            imageTransform.anchoredPosition = new Vector2(0, offsetY);
        }
    }

    IEnumerator TrocarImagens()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoEntreImagens);

            int proximoIndice = (indiceAtual + 1) % imagens.Count;
            Sprite proximaImagem = imagens[proximoIndice];

            yield return StartCoroutine(FazerFade(proximaImagem));

            indiceAtual = proximoIndice;
        }
    }

    IEnumerator FazerFade(Sprite novaImagem)
    {
        // Fade out
        for (float t = 0; t < tempoFade; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, t / tempoFade);
            imageUI.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        imageUI.sprite = novaImagem;

        // Fade in
        for (float t = 0; t < tempoFade; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, t / tempoFade);
            imageUI.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        imageUI.color = Color.white;
    }
}
