using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] int points;

    public void SendPointsToScoreManager()
    {
        ScoreManager.Instance.AddScore(points);
    }
}
