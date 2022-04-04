using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    public static int MaxLeaderBoardSlotSaved = 8;
    public static string SlotNamePrefix = "Slot_Name_";
    public static string SlotScorePrefix = "Slot_Score_";
    public static string SlotRoundPrefix = "Slot_Round_";

    public List<HighScore> highScores;

    public class HighScore
    {
        public string Name;
        public int Score;
        public int Round;

        public void Set(string name, int score, int round)
        {
            Name = name;
            Score = score;
            Round = round;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 10; i++)
        {
            string name = PlayerPrefs.GetString(SlotNamePrefix + i);
            int score = PlayerPrefs.GetInt(SlotScorePrefix + i);
            int round = PlayerPrefs.GetInt(SlotRoundPrefix + i);
            HighScore highScore = new HighScore();
            highScore.Set(name, score, round);
            highScores.Add(highScore);
        }
    }

    public static bool MadeItToTheLeaderBoard(int score)
    {
        int lowestLeaderBoardScore = PlayerPrefs.GetInt(SlotScorePrefix + MaxLeaderBoardSlotSaved);
        return lowestLeaderBoardScore < score;
    }

    public void SaveToLeaderBoard(string name, int score, int round)
    {
        bool inserted = false;
        int counter = 1;
        foreach (HighScore highScore in highScores)
        {
            if (!inserted && highScore.Score < score)
            {
                SaveHighScore(counter, name, score, round);
                inserted = true;
                counter++;
                if (counter > MaxLeaderBoardSlotSaved)
                {
                    break;
                }
                SaveHighScore(counter, highScore.Name, highScore.Score, highScore.Round);
                counter++;
                if (counter > MaxLeaderBoardSlotSaved)
                {
                    break;
                }
            } else
            {
                SaveHighScore(counter, highScore.Name, highScore.Score, highScore.Round);
                counter++;
                if (counter > MaxLeaderBoardSlotSaved)
                {
                    break;
                }
            }
        }
    }

    public void SaveHighScore(int placement, string name, int score, int round)
    {
        PlayerPrefs.SetString(SlotNamePrefix + placement, name);
        PlayerPrefs.SetInt(SlotScorePrefix + placement, score);
        PlayerPrefs.SetInt(SlotRoundPrefix + placement, round);
    }

}
