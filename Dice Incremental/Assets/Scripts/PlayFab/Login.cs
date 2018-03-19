using System.Collections;
using System.Collections.Generic;
using System.Text;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;


public class Login : MonoBehaviour
{

	[SerializeField]
	private Transform m_errorTransform;

	[SerializeField]
	private Text m_errorText;
	private string m_username = "";
	private string m_password = "";

	public void UsernameChanged( string newUsername )
	{
		m_username = newUsername;
	}

	public void PasswordChanged( string newPassword )
	{
		m_password = newPassword;
	}

	public void DoLogin()
	{
#if DEVLOGIN
		m_username = "matsgamedev";
		m_password = "testdev";
#endif

		LoginWithPlayFabRequest request = new LoginWithPlayFabRequest
		{
			Username = m_username,
			Password = m_password,
			TitleId = PlayFabSettings.TitleId
		};

		PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSucces, OnLoginFail);
	}

	private void OnLoginSucces( LoginResult result )
	{
		Debug.LogFormat("Logged in succesfully with user: {0}", m_username);
	}

	private void OnLoginFail(PlayFabError error)
	{
		Debug.LogFormat("Login failed with error: {0}", error.GenerateErrorReport());
		ShowError(error);
	}

	public void DoRegister()
	{
		RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
		{
			Username = m_username,
			Password = m_password,
			RequireBothUsernameAndEmail = false,
			TitleId = PlayFabSettings.TitleId
		};

		PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucces, OnRegisterFail);
	}

	private void OnRegisterSucces( RegisterPlayFabUserResult result)
	{
		Debug.LogFormat("Register succesfull with user: {0} and your login status is: {1}", result.Username, PlayFabClientAPI.IsClientLoggedIn());
	}

	private void OnRegisterFail( PlayFabError error )
	{
		Debug.LogFormat("Register failed because of: {0}", error.GenerateErrorReport());
		ShowError(error);
	}

	private void ShowError(PlayFabError error)
	{
		m_errorText.text = GenerateReadableError(error);
		m_errorTransform.gameObject.SetActive(true);
	}

	public void CloseError()
	{
		m_errorTransform.gameObject.SetActive(false);
	}


	private string GenerateReadableError(PlayFabError error)
	{
		StringBuilder tempSb = new StringBuilder();

		if ( error.ErrorDetails == null ) return error.ErrorMessage;

		foreach (KeyValuePair<string, List<string>> pair in error.ErrorDetails)
		foreach (string msg in pair.Value)
			tempSb.Append("\n").Append(pair.Key).Append(": ").Append(msg);
		return tempSb.ToString();
	}
}
