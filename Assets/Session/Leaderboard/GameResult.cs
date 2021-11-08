using System;

namespace Leaderboards
{
    [Serializable]
    public struct GameResult
    {
        public int Score;
        public long Date;

        public override string ToString ()
        {
            return Score.ToString("N0")+" "+GetDateTime().ToString("HH:mm dd:MM:y");
        }

        public GameResult (int score, DateTime date)
        {
            Score = score;
            Date = date.ToFileTimeUtc();

        }

        public DateTime GetDateTime ()
        {
            return DateTime.FromFileTimeUtc(Date);

        }
    }
}
