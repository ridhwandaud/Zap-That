using UnityEngine;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using GooglePlayGames.BasicApi;

public class GameManager
{
	private static GameManager sInstance = new GameManager();

	public static GameManager Instance
	{
		get
		{
			return sInstance;
		}
	}

	private bool mAuthenticating = false;

	public void Authenticate()
	{
		if (Authenticated || mAuthenticating)
		{
			Debug.LogWarning("Ignoring repeated call to Authenticate().");
			return;
		}

		// Enable/disable logs on the PlayGamesPlatform
		PlayGamesPlatform.DebugLogEnabled = true;

		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			.EnableSavedGames()
			.Build();
		PlayGamesPlatform.InitializeInstance(config);

		// Activate the Play Games platform. This will make it the default
		// implementation of Social.Active
		PlayGamesPlatform.Activate();

		// Set the default leaderboard for the leaderboards UI
		//((PlayGamesPlatform)Social.Active).SetDefaultLeaderboardForUI(GameIds.LeaderboardId);

		// Sign in to Google Play Games
		mAuthenticating = true;
		Social.localUser.Authenticate((bool success) =>
			{
				mAuthenticating = false;
				if (success)
				{
					// if we signed in successfully, load data from cloud
					Debug.Log("Login successful!");
				}
				else
				{
					// no need to show error message (error messages are shown automatically
					// by plugin)
					Debug.LogWarning("Failed to sign in with Google Play Games.");
				}
			});
	}

	public bool Authenticated
	{
		get
		{
			return Social.Active.localUser.authenticated;
		}
	}

	public bool Authenticating
	{
		get
		{
			return mAuthenticating;
		}
	}

	public void SignOut()
	{
		((PlayGamesPlatform)Social.Active).SignOut();
	}
}
