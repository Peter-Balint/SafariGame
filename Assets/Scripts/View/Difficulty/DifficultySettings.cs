using Safari.Model;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "Game/Difficulty Settings")]
public class DifficultySettings : ScriptableObject
{
	/// <summary>
	/// The difficulty levels have individual tresholds, they can be adjusted through this scriptible object.
	/// </summary>
	[System.Serializable]
	public class DifficultyData
	{
		public GameDifficulty difficulty;
		public int startingMoney;
		public int moneyWinThreshold;
		public int animalWinThreshold;
		public int visitorWinThreshold;
		public double winMinutesRequired;

	}

	public DifficultyData[] difficultyLevels;

	public DifficultyData GetSettings(GameDifficulty difficulty)
	{
		foreach (var data in difficultyLevels)
		{
			if (data.difficulty == difficulty)
				return data;
		}

		Debug.LogWarning("No settings found for difficulty: " + difficulty);
		return null;
	}
}
