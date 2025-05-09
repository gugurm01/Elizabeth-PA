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
    [SerializeField] private GameObject loggedInPanel;
    [SerializeField] private GameObject errorPanel;

    [Header("Login UI")]
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;

    [Header("Erro UI")]
    [SerializeField] private Button okButton;

    [Header("Credenciais")]
    [SerializeField] private string correctPassword = "1234";
    [SerializeField] private string displayedUsername = "Administrador";

    private LoginState currentState;

    private void Start()
    {
        SetState(LoginState.LOGIN);

        usernameText.text = displayedUsername;
        passwordInput.contentType = TMP_InputField.ContentType.Password;
        passwordInput.ActivateInputField();

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
        if (passwordInput.text == correctPassword)
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
    }

    private void ShowError()
    {
        loginPanel.SetActive(false);
        errorPanel.SetActive(true);
    }

    private void SetState(LoginState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case LoginState.LOGIN:
                loginPanel.SetActive(true);
                errorPanel.SetActive(false);
                loggedInPanel.SetActive(false);
                break;

            case LoginState.LOGADO:
                loginPanel.SetActive(false);
                errorPanel.SetActive(false);
                loggedInPanel.SetActive(true);
                break;
        }
    }
}
