using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CesarCifra : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text originalWordText;
    public TMP_Text feedbackText;
    public Button checkButton;

    [Header("Spawning")]
    public GameObject letterUIPrefab;
    public Transform letterUIContainer;

    public int TEMP_timesToPlay;
    public GameObject TEMP_gameWin;

    private List<string> wordList = new List<string> {
        "code", "cipher", "intel", "decrypt", "war",
        "spies", "message", "signal", "radio", "freedom"
    };

    private string currentWord;
    private string encryptedWord;
    private int caesarShift;

    private List<TMP_InputField> currentInputs = new();

    void Start()
    {
        checkButton.onClick.AddListener(CheckResult);
        StartNewPuzzle();
    }

    void StartNewPuzzle()
    {
        // Limpa elementos anteriores
        foreach (Transform child in letterUIContainer)
        {
            Destroy(child.gameObject);
        }
        currentInputs.Clear();

        // Palavra e shift aleatórios
        currentWord = wordList[Random.Range(0, wordList.Count)].ToLower();
        caesarShift = Random.Range(1, 25);
        encryptedWord = Encrypt(currentWord, caesarShift);

        // Mostra palavra + deslocamento
        originalWordText.text = $"{currentWord.ToUpper()} +{caesarShift}";
        feedbackText.text = "";

        // Cria elementos visuais
        for (int i = 0; i < currentWord.Length; i++)
        {
            GameObject uiElement = Instantiate(letterUIPrefab, letterUIContainer);

            // Assumindo que os filhos do prefab tenham esses nomes:
            TMP_InputField input = uiElement.transform.Find("LetraInput").GetComponent<TMP_InputField>();
            Button plus = uiElement.transform.Find("PlusButton").GetComponent<Button>();
            Button minus = uiElement.transform.Find("MinusButton").GetComponent<Button>();

            int index = i; // necessário para evitar closure incorreta

            plus.onClick.AddListener(() => ShiftLetter(index, +1));
            minus.onClick.AddListener(() => ShiftLetter(index, -1));

            input.text = "A";
            currentInputs.Add(input);
        }
    }

    void ShiftLetter(int index, int shift)
    {
        string current = currentInputs[index].text.ToUpper();
        if (string.IsNullOrEmpty(current)) current = "A";
        char c = current[0];
        char newChar = (char)((((c - 'A') + shift + 26) % 26) + 'A');
        currentInputs[index].text = newChar.ToString();
    }

    void CheckResult()
    {
        string result = "";
        foreach (var input in currentInputs)
        {
            result += input.text.ToLower();
        }

        if (result == encryptedWord)
        {
            feedbackText.text = "Correto! Próximo...";
            feedbackText.color = Color.green;
            TEMP_timesToPlay--;
            if(TEMP_timesToPlay <= 0) 
            {
                TEMP_gameWin.SetActive(true);
                this.enabled = false;
            }
            else
            Invoke(nameof(StartNewPuzzle), 1.5f);
        }
        else
        {
            feedbackText.text = "Tente novamente!";
            feedbackText.color = Color.red;
        }
    }

    string Encrypt(string word, int shift)
    {
        char[] letters = word.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i] = (char)((((letters[i] - 'a') + shift) % 26) + 'a');
        }
        return new string(letters);
    }
}
