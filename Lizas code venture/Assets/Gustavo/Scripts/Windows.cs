using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Windows : MonoBehaviour
{
    private enum LoginState
    {
        LOGIN,
        LOGADO
    }

    [Header("Painéis")]
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject loginPanelParent;
    [SerializeField] private GameObject loggedInPanel;
    [SerializeField] private GameObject errorPanel;

    [Header("Login UI")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] CanvasGroup canvasGroup;

    [Header("Erro UI")]
    [SerializeField] private Button okButton;

    [Header("Credenciais")]
    [SerializeField] private string correctPassword;
    [SerializeField] private string correctUsername;

    private LoginState currentState;

    private void Start()
    {
        SetState(LoginState.LOGIN);

        usernameInput.contentType = TMP_InputField.ContentType.Standard;
        passwordInput.contentType = TMP_InputField.ContentType.Password;

        usernameInput.text = "";
        passwordInput.text = "";

        usernameInput.ActivateInputField();

        loginButton.onClick.AddListener(HandleLogin);
        okButton.onClick.AddListener(ReturnToLogin);
    }

    private void Update()
    {
        if (currentState == LoginState.LOGIN &&
            loginPanel.activeSelf &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            HandleLogin();
        }
    }

    private void HandleLogin()
    {
        if (passwordInput.text == correctPassword && usernameInput.text == correctUsername)
        {
            SetState(LoginState.LOGADO);
        }
        else
        {
            ShowError();
        }
    }

    private void ReturnToLogin()
    {
        errorPanel.SetActive(false);
        loginPanel.SetActive(true);
        passwordInput.text = "";
        passwordInput.ActivateInputField();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    private void ShowError()
    {
        loginPanel.SetActive(false);
        errorPanel.SetActive(true);

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    private void SetState(LoginState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case LoginState.LOGIN:
                loginPanelParent.SetActive(true);
                loggedInPanel.SetActive(false);
                break;

            case LoginState.LOGADO:
                loggedInPanel.SetActive(true);
                loginPanelParent.SetActive(false);
                break;
        }
    }
}
