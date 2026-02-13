using System.Collections.Generic;

[System.Serializable]
public class ScoreRecord
{
    public float playTimeSeconds;   // Время в игре (в секундах)
    public int bulletSpent;         // Кол-во потраченных снарядов
    public int score;               // Количество очков
    public string dateTime;         // Дата и время (ISO формат)

    public string checksum;         // Контрольная сумма
}

[System.Serializable]
public class ScoreData
{
    public ScoreRecord bestScore;               // Лучший за всё время
    public int scoreAmount = 3;                 // Количество сохраненных результатов
    public List<ScoreRecord> recentScores;      // Последние scoreAmount результата

    public ScoreData()
    {
        recentScores = new List<ScoreRecord>();
    }
}