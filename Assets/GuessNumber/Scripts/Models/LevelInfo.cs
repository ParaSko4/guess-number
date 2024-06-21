using System;

namespace GuessNumber
{
    public class LevelInfo
    {
        public event Action<int> NumberSetted;
        public event Action<bool> LevelStatusChanged;

        public int Number { get; private set; } = 0;
        public int LastGuessNumber { get; private set; } = 0;
        public bool IsLastGuessPlayerNumberLarger => Number < LastGuessNumber;
        public bool AITurn { get; private set; } = false;
        public bool LevelCompleted { get; private set; } = false;

        public void SetNumber(int number)
        {
            Number = number;

            NumberSetted?.Invoke(Number);
        }

        public void SetLastGuessNumber(int lastGuessNumber)
        {
            LastGuessNumber = lastGuessNumber;
        }

        public void SetLevelStatus(bool completed)
        {
            LevelCompleted = completed;

            LevelStatusChanged?.Invoke(completed);
        }

        public void SetAITurnState(bool turn)
        {
            AITurn = turn;
        }
    }
}