using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoardManager : MonoBehaviour
{
    public static int MaxLeaderBoardSlotSaved = 8;
    public static string PlayerName = "PlayerName";
    public static string SlotNamePrefix = "Slot_Name_";
    public static string SlotScorePrefix = "Slot_Score_";
    public static string SlotRoundPrefix = "Slot_Round_";

    public List<HighScore> highScores;

    public List<TMP_Text> names;
    public List<TMP_Text> scores;
    public List<TMP_Text> rounds;

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
        highScores = CreateHighScores();
        int counter = 0;
        foreach (HighScore highScore in highScores)
        {
            names[counter].text = highScore.Name;
            scores[counter].text = "" + highScore.Score;
            rounds[counter].text = "" + highScore.Round;
            counter++;
        }
    }

    public static List<HighScore> CreateHighScores()
    {
        List<HighScore> scores = new List<HighScore>();
        for (int i = 1; i <= MaxLeaderBoardSlotSaved; i++)
        {
            string name = PlayerPrefs.GetString(SlotNamePrefix + i);
            int score = PlayerPrefs.GetInt(SlotScorePrefix + i);
            int round = PlayerPrefs.GetInt(SlotRoundPrefix + i);
            HighScore highScore = new HighScore();
            highScore.Set(name, score, round);
            scores.Add(highScore);
        }
        return scores;
    }

    public static bool MadeItToTheLeaderBoardAndIfSoSaveIt(int score, int round)
    {
        int lowestLeaderBoardScore = PlayerPrefs.GetInt(SlotScorePrefix + MaxLeaderBoardSlotSaved);
        int lowestLeaderBoardRound = PlayerPrefs.GetInt(SlotScorePrefix + MaxLeaderBoardSlotSaved);
        bool madeItToTheLeaderBoard = lowestLeaderBoardScore < score || lowestLeaderBoardScore == score && lowestLeaderBoardRound > round;
        if (madeItToTheLeaderBoard)
        {
            SaveToLeaderBoard(PlayerPrefs.GetString(PlayerName), score, round);
        }
        return madeItToTheLeaderBoard;
    }

    public static void SaveToLeaderBoard(string name, int score, int round)
    {
        List<HighScore> scores = CreateHighScores();
        bool inserted = false;
        int counter = 1;
        foreach (HighScore highScore in scores)
        {
            if (!inserted && (highScore.Score < score || highScore.Score == score && highScore.Round > round))
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

    public static void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString(LeaderBoardManager.PlayerName, playerName);
    }

    public static void SaveHighScore(int placement, string name, int score, int round)
    {
        PlayerPrefs.SetString(SlotNamePrefix + placement, name);
        PlayerPrefs.SetInt(SlotScorePrefix + placement, score);
        PlayerPrefs.SetInt(SlotRoundPrefix + placement, round);
    }

}
