using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

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

        Debug.Log("1");
        if(PlayerPrefs.GetInt("switch",0)==1)//��play������ת
        {
            UpdateScore(currentPlayerName,PlayerPrefs.GetInt("score",0));
            Debug.Log("1-1");
        }
        else
        {
            // �л�������㳡��ʱ�������а�����
            LoadScoreData();
            Debug.Log("1-2");
        }
    }

    // ����³ɼ������а�
    public void AddScore(string playerName, int score)
    {
        PlayerScore existingScore = scoreList.Find(p => p.PlayerName == playerName);

        if (existingScore != null)
        {
            // ����ҵ�����ͬ�ǳƵ���ң������Ϊ���ߵķ���
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
            // ���û���ҵ���ͬ�ǳƵ���ң�������³ɼ�
            PlayerScore newScore = new PlayerScore();
            newScore.PlayerName = playerName;
            newScore.score = score;
            scoreList.Add(newScore);
            Debug.Log("Added new score for player " + playerName + ": " + score);
        }

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
            Debug.Log("2-1");
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
            Debug.Log("2-2");
        }
    }

    // �������а�����
    private void SaveScoreData()
    {
        string json = JsonUtility.ToJson(scoreList);
        System.IO.File.WriteAllText(filePath, json);
        DisplayLeaderboard();
    }

    public Text teshu;

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
                teshu.text = $"[{i + 1}] {playerName}: {playerScore}\n";
            }

            leaderboardInfo += $"[{i + 1}] {playerName}: {playerScore}\n";
            
        }

        Text leaderboardText = ScrollRect.content.GetComponent<Text>();
        //Text leaderboardText = ScrollRect.content.GetComponent<Text>();
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
            bannar.transform.GetChild(0).GetComponent<Text>().text = "����������!";
            return 0;
        }
    }

    public void Guanbi()
    {
        bannar.SetActive(false);
    }

}
