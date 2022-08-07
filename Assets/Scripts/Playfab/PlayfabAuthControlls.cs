using System;
using System.Collections;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayfabAuthControlls: MonoBehaviour
{

	[SerializeField] private TMP_InputField usernameInput;
	[SerializeField] private TMP_InputField passwordInput;
	[SerializeField] private TMP_InputField nameInput;
	[SerializeField] private TMP_Text infoText;

	[SerializeField] private GameObject playAsGuestButton;

	private string userDisplayName;

	private const string _PlayFabRememberMeIdKey = "MaraudersBayPlayFabId";

	/// Generated GUID for guest players
	private string RememberMeId
	{
		get
		{
			return PlayerPrefs.GetString(_PlayFabRememberMeIdKey, null);
		}
		set
		{
			var guid = value ?? Guid.NewGuid().ToString();
			PlayerPrefs.SetString(_PlayFabRememberMeIdKey, guid);
		}
	}

	private void ProceedToGame()
	{
		SceneManager.LoadScene(GameScenes.GamePlay);
	}

	private void ShowError(string message)
    {
		infoText.text = message;
		infoText.gameObject.SetActive(true);
		// hide guest button as it occupies same space
		playAsGuestButton.gameObject.SetActive(false);
		StartCoroutine(HideError());
	}

	IEnumerator HideError()
    {
		yield return new WaitForSeconds(5.0f);
		infoText.gameObject.SetActive(false);
		playAsGuestButton.gameObject.SetActive(true);
	}

	public void RegisterPlayer()
	{
		Debug.Log("[Auth].RegisterPlayer");


		if(usernameInput.text == null || usernameInput.text.Length == 0)
        {
			ShowError("Please enter a username to signup");
			return;
        }

		if (passwordInput.text == null || passwordInput.text.Length == 0)
		{
			ShowError("Please enter a password to signup");
			return;
		}

		if (nameInput.text == null || nameInput.text.Length == 0)
		{
			ShowError("Please enter a name to signup");
			return;
		}

		RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
		{
			Username = usernameInput.text.ToLower(),
			Password = passwordInput.text,
			RequireBothUsernameAndEmail = false,
			DisplayName = nameInput.text
		};
		PlayFabClientAPI.RegisterPlayFabUser(request, onRegisterSuccess, onRegisterError);
	}

	public void LoginPlayer()
	{
		Debug.Log("[Auth].LoginPlayer");
		if (usernameInput.text == null || usernameInput.text.Length == 0)
		{
			ShowError("Please enter a username to log in");
			return;
		}

		if (passwordInput.text == null || passwordInput.text.Length == 0)
		{
			ShowError("Please enter a password to log in");
			return;
		}
		LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
		{
			Username = usernameInput.text.ToLower(),
			Password = passwordInput.text,
		};

		PlayFabClientAPI.LoginWithPlayFab(request, OnPlayFabLoginSuccess, OnLoginError);
	}

	public void LoginGuestPlayer()
	{
		Debug.Log("[Auth].LoginGuestPlayer");

		if (string.IsNullOrEmpty(RememberMeId))
		{
			RememberMeId = Guid.NewGuid().ToString();
		}

		LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
		{
			CustomId = RememberMeId,
			CreateAccount = true,
		};

		PlayFabClientAPI.LoginWithCustomID(request, OnPlayFabLoginSuccessGuest, OnLoginError);
	}

	private void OnLoginError(PlayFabError response)
	{
		Debug.Log(response.ToString());
		ShowError(response.ErrorMessage);
	}

	private void OnPlayFabLoginSuccess(LoginResult response)
	{
		Debug.Log("[Auth].OnPlayFabLoginSuccess");
		infoText.text = "Logged in! Loading your data...";
		GetUserData();
	}


	private void OnPlayFabLoginSuccessGuest(LoginResult response)
	{
		Debug.Log("[Auth].OnPlayFabLoginSuccess");
		GetUserData();
		GameMenuText.greetingText = "Signup to save your gold and inventory.";
	}

	private void onRegisterError(PlayFabError error)
	{
		Debug.LogError("Register error:" + error.ErrorMessage);
		infoText.text = error.ErrorMessage;
	}

	private void onRegisterSuccess(RegisterPlayFabUserResult result)
	{
		infoText.text = "Registered!";
		GameMenuText.greetingText = "Welcome to the Marauder's bay, <name>.";
		GetUserData();
	}

	private void GetUserData()
	{
		Debug.Log("[Auth].GetUserData");
		GetAccountInfoRequest request = new GetAccountInfoRequest();
		PlayFabClientAPI.GetAccountInfo(request, OnGetUserDataSuccess, OnGetUserDataError);
	}

	private void OnGetUserDataSuccess(GetAccountInfoResult result)
	{
		userDisplayName = result.AccountInfo.TitleInfo.DisplayName;
		GameMenuText.greetingText = GameMenuText.greetingText.Replace("<name>", userDisplayName);
		ProceedToGame();
	}

	private void OnGetUserDataError(PlayFabError error)
	{
		Debug.Log(error.ErrorMessage);
	}

}