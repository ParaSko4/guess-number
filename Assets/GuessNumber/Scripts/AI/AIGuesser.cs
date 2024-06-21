using GuessNumber.NumberGenerator;
using Random = UnityEngine.Random;

namespace GuessNumber.AI
{
    public class AIGuesserService : IService
    {
        private readonly PlayerGuessInfo playerGuessInfo;
        private readonly NumberGeneratorSettings numberGeneratorSettings;

        private int maxRange;
        private int minRange;

        public PlayerGuessInfo PlayerGuessInfo => playerGuessInfo;

        public AIGuesserService(NumberGeneratorSettings numberGeneratorSettings)
        {
            this.numberGeneratorSettings = numberGeneratorSettings;

            playerGuessInfo = new(LabelsStrings.AI_NAME);
        }

        public void ResetGuesser()
        {
            maxRange = numberGeneratorSettings.MaxRangeNumber;
            minRange = numberGeneratorSettings.MinRangeNumber;
        }

        public void GuessNumber()
        {
            var guessNumber = Random.Range(minRange, maxRange);

            playerGuessInfo.AddNumber(guessNumber);
        }

        public void CorrectGuessRange(int previousGuessNumber, bool isGuessNumberLarger)
        {
            if (isGuessNumberLarger && maxRange > previousGuessNumber)
            {
                maxRange = previousGuessNumber;
            }
            else if (isGuessNumberLarger == false && minRange <= previousGuessNumber)
            {
                minRange = previousGuessNumber + 1;
            }
        }
    }
}