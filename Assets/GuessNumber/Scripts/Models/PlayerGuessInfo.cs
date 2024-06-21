using System;
using System.Text;

namespace GuessNumber
{
    public class PlayerGuessInfo
    {
        private const int MAX_INT_RANGE = 9;

        public event Action FailedAddNumber;
        public event Action<int> NumberUpdated;

        private StringBuilder numberStringBuilder = new();

        public int SuccessGuessCounter { get; private set; } = 0;
        public int NumberInt => int.Parse(NumberString);
        public string PlayerName {  get; private set; }
        public string NumberString => numberStringBuilder.Length == 0 ? "0" : numberStringBuilder.ToString();

        public PlayerGuessInfo(string playerName)
        {
            PlayerName = playerName;
        }

        public void IncreaseGuessCounter()
        {
            SuccessGuessCounter++;
        }

        public void AddNumber(int number)
        {
            if (numberStringBuilder.Length + 1 > MAX_INT_RANGE
                || number == 0 && numberStringBuilder.Length == 0)
            {
                return;
            }

            numberStringBuilder.Append(number);

            NumberUpdated?.Invoke(NumberInt);
        }

        public void RemoveLastNumber()
        {
            if (numberStringBuilder.Length == 0)
            {
                return;
            }

            numberStringBuilder.Remove(numberStringBuilder.Length - 1, 1);

            NumberUpdated?.Invoke(NumberInt);
        }

        public void Clear()
        {
            numberStringBuilder.Clear();

            NumberUpdated?.Invoke(NumberInt);
        }
    }
}