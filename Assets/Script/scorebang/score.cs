using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class score : MonoBehaviour
{
    public ScrollRect ScrollRect;
    public string currentPlayerName; // ��ǰ��ҵ�����

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
        //����������ֻ�ȡ
        currentPlayerName = PlayerPrefs.GetString("PlayerName", "visitor");

        // �����ļ�·��Ϊ��ϷͬĿ¼�µ� scores.json
        filePath = Application.dataPath + "/scores.json";


        if(PlayerPrefs.GetInt("switch",0)==1)//��play������ת
        {
            UpdateScore(currentPlayerName,PlayerPrefs.GetInt("score",0));
        }
        else
        {
            // �л�������㳡��ʱ�������а�����
            LoadScoreData();
        }
    }

    // ����³ɼ������а�
    public void AddScore(string playerName, int score)
    {
        PlayerScore newScore = new PlayerScore();
        newScore.PlayerName = playerName;
        newScore.score = score;

        scoreList.Add(newScore);
        SortScores();
        SaveScoreData();
    }

    // ɾ�����а����ض���ҵĳɼ�
    public void DeleteScore(string playerName)
    {
        PlayerScore scoreToRemove = scoreList.Find(p => p.PlayerName == playerName);
        if (scoreToRemove != null)
        {
            scoreList.Remove(scoreToRemove);
            SaveScoreData();
        }
    }

    // �����ض���ҵĳɼ�
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

    // �Զ��������������շ�����������
    private void SortScores()
    {
        scoreList.Sort((x, y) => y.score.CompareTo(x.score));
    }

    // �������а�����
    private void LoadScoreData()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            scoreList = JsonUtility.FromJson<List<PlayerScore>>(json);
            DisplayLeaderboard();
        }
        else
        {
            Debug.LogWarning("No score data found.");
            // ����һ���µ����а�����ʾ��
            PlayerScore exampleScore = new PlayerScore();
            exampleScore.PlayerName = "visitor";
            exampleScore.score = 0;

            // ��ʾ��������ӵ��б���
            scoreList.Add(exampleScore);

            // �������ݱ��浽�ļ���
            SaveScoreData();
        }
    }

    // �������а�����
    private void SaveScoreData()
    {
        string json = JsonUtility.ToJson(scoreList);
        System.IO.File.WriteAllText(filePath, json);
        DisplayLeaderboard();
    }

    // ʾ����������ʾ���а���Ϣ�� UI Text ��
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
                playerName = "<color=yellow>" + playerName + "</color>"; // ����ǰ��ҵ������Ի�ɫ��ʾ
            }

            leaderboardInfo += $"[{i + 1}] {playerName}: {playerScore}\n";
        }

        Text leaderboardText = ScrollRect.content.GetComponent<Text>();
        leaderboardText.text = leaderboardInfo; // ���� UI Text ���ı�����
    }

    // �������ӡ�ɾ���͸��·����Ĺ���
    // ����³ɼ�
    public void AddPlayerScore(string playerName, int score)
    {
        AddScore(playerName, score);
    }

    // ɾ���ض���ҵĳɼ�
    public void RemovePlayerScore(string playerName)
    {
        DeleteScore(playerName);
    }

    // �����ض���ҵĳɼ�
    public void UpdatePlayerScore(string playerName, int newScore)
    {
        UpdateScore(playerName, newScore);
    }
}
