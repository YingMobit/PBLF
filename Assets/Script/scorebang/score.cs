using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

public class score : MonoBehaviour
{
    public ScrollRect ScrollRect;
    public string currentPlayerName; // 当前玩家的名称

    private List<PlayerScore> scoreList = new List<PlayerScore>();
    private string filePath;

    [System.Serializable]
    public class PlayerScore
    {
        public string PlayerName;
        public int score;
    }


    private void Awake()
    {
        //本次玩家名字获取
        currentPlayerName = PlayerPrefs.GetString("PlayerName", "visitor");

        // 设置文件路径为游戏同目录下的 scores.json
        filePath = Application.dataPath + "/scores.json";

        Debug.Log("1");
        if(PlayerPrefs.GetInt("switch",0)==1)//从play场景跳转
        {
            UpdateScore(currentPlayerName,PlayerPrefs.GetInt("score",0));
            Debug.Log("1-1");
        }
        else
        {
            // 切换到结结算场景时加载排行榜数据
            LoadScoreData();
            Debug.Log("1-2");
        }
    }

    // 添加新成绩到排行榜
    public void AddScore(string playerName, int score)
    {
        PlayerScore existingScore = scoreList.Find(p => p.PlayerName == playerName);

        if (existingScore != null)
        {
            // 如果找到了相同昵称的玩家，则更新为更高的分数
            if (score > existingScore.score)
            {
                existingScore.score = score;
                Debug.Log("Updated score for player " + playerName + ". New score: " + existingScore.score);
            }
            else
            {
                Debug.Log("Score not updated for player " + playerName + ". Existing score is higher.");
            }
        }
        else
        {
            // 如果没有找到相同昵称的玩家，则添加新成绩
            PlayerScore newScore = new PlayerScore();
            newScore.PlayerName = playerName;
            newScore.score = score;
            scoreList.Add(newScore);
            Debug.Log("Added new score for player " + playerName + ": " + score);
        }

        SortScores();
        SaveScoreData();
    }



    // 删除排行榜中特定玩家的成绩
    public void DeleteScore(string playerName)
    {
        PlayerScore scoreToRemove = scoreList.Find(p => p.PlayerName == playerName);
        if (scoreToRemove != null)
        {
            scoreList.Remove(scoreToRemove);
            SaveScoreData();
        }
    }

    // 更新特定玩家的成绩
    public void UpdateScore(string playerName, int newScore)
    {
        PlayerScore scoreToUpdate = scoreList.Find(p => p.PlayerName == playerName);
        if (scoreToUpdate != null)
        {
            scoreToUpdate.score = newScore;
            SortScores();
            SaveScoreData();
        }
        else
        {
            AddScore(playerName, newScore);
        }
    }

    // 自定义排序函数：按照分数降序排列
    private void SortScores()
    {
        scoreList.Sort((x, y) => y.score.CompareTo(x.score));
    }

    // 加载排行榜数据
    private void LoadScoreData()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            scoreList = JsonUtility.FromJson<List<PlayerScore>>(json);
            DisplayLeaderboard();
            Debug.Log("2-1");
        }
        else
        {
            Debug.LogWarning("No score data found.");
            // 创建一个新的排行榜数据示例
            PlayerScore exampleScore = new PlayerScore();
            exampleScore.PlayerName = "visitor";
            exampleScore.score = 0;

            // 将示例数据添加到列表中
            scoreList.Add(exampleScore);

            // 将新数据保存到文件中
            SaveScoreData();
            Debug.Log("2-2");
        }
    }

    // 保存排行榜数据
    private void SaveScoreData()
    {
        string json = JsonUtility.ToJson(scoreList);
        System.IO.File.WriteAllText(filePath, json);
        DisplayLeaderboard();
    }

    public Text teshu;

    // 示例方法来显示排行榜信息在 UI Text 中
    private void DisplayLeaderboard()
    {
        string leaderboardInfo = "";
        for (int i = 0; i < scoreList.Count; i++)
        {
            string playerName = scoreList[i].PlayerName;
            string playerScore = scoreList[i].score.ToString();

            currentPlayerName = PlayerPrefs.GetString("player", "");

            if (playerName == currentPlayerName)
            {
                playerName = "<color=yellow>" + playerName + "</color>"; // 将当前玩家的名称以黄色显示
                teshu.text = $"[{i + 1}] {playerName}: {playerScore}\n";
            }

            leaderboardInfo += $"[{i + 1}] {playerName}: {playerScore}\n";
            
        }

        Text leaderboardText = ScrollRect.content.GetComponent<Text>();
        //Text leaderboardText = ScrollRect.content.GetComponent<Text>();
        leaderboardText.text = leaderboardInfo; // 更新 UI Text 的文本内容
    }

    // 补充的添加、删除和更新分数的功能
    // 添加新成绩
    public void AddPlayerScore(string playerName, int score)
    {
        AddScore(playerName, score);
    }

    // 删除特定玩家的成绩
    public void RemovePlayerScore(string playerName)
    {
        DeleteScore(playerName);
    }

    // 更新特定玩家的成绩
    public void UpdatePlayerScore(string playerName, int newScore)
    {
        UpdateScore(playerName, newScore);
    }

    public InputField nameInputField;
    public InputField scoreInputField;
    public void Addscre()
    {
        PlayerPrefs.SetString("player", nameInputField.text);
        int score = ConvertToInt(scoreInputField.text);
        AddScore(nameInputField.text, score);
    }

    public GameObject bannar;

    public int ConvertToInt(string textValue)
    {
        int convertedNumber;

        if (int.TryParse(textValue, out convertedNumber))
        {
            Debug.Log("Converted number: " + convertedNumber);
            return convertedNumber;
        }
        else
        {
            Debug.Log("Failed to convert text to integer!");
            bannar.SetActive(true);
            bannar.transform.GetChild(0).GetComponent<Text>().text = "请输入数字!";
            return 0;
        }
    }

    public void Guanbi()
    {
        bannar.SetActive(false);
    }

}
