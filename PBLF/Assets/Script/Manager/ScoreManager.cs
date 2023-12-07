using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public UnityEvent<int> ScoreChanged;

    public void ChangeScore(int value)
    {
        score += value;
        ScoreChanged?.Invoke(score);
    }
}
