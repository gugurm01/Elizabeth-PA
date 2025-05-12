using TMPro;
using UnityEngine;

public class MatrixFX : MonoBehaviour
{
    public TextMeshProUGUI uiText;  // Texto com TMP
    public float speed = 50f;       // Velocidade do movimento
    public bool moveUp = true;      // Direção: cima ou baixo
    public int textLength = 16;     // Tamanho da string binária

    private RectTransform rectTransform;
    private float screenHeight;
    private float textHeight;

    void Start()
    {
        rectTransform = uiText.GetComponent<RectTransform>();
        screenHeight = Screen.height;

        // Estimar a altura do texto (ou defina manualmente se quiser)
        textHeight = rectTransform.rect.height;
    }

    void Update()
    {
        // Atualiza o conteúdo com uma nova string binária
        uiText.text = GenerateBinaryString(textLength);

        // Move o texto
        float direction = moveUp ? 1f : -1f;
        rectTransform.anchoredPosition += Vector2.up * direction * speed * Time.deltaTime;

        // Reinicia a posição quando sai da tela para dar efeito de loop
        if (moveUp && rectTransform.anchoredPosition.y > screenHeight + textHeight)
        {
            rectTransform.anchoredPosition = new Vector2(
                rectTransform.anchoredPosition.x,
                -textHeight
            );
        }
        else if (!moveUp && rectTransform.anchoredPosition.y < -textHeight)
        {
            rectTransform.anchoredPosition = new Vector2(
                rectTransform.anchoredPosition.x,
                screenHeight + textHeight
            );
        }
    }

    string GenerateBinaryString(int length)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            sb.Append(Random.value > 0.5f ? '1' : '0');
        }
        return sb.ToString();
    }
}
