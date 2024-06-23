using Cysharp.Threading.Tasks;
using GuessNumber.AI;
using GuessNumber.NumberGenerator;
using GuessNumber.View;
using System;
using UnityEngine;

namespace GuessNumber
{
    public class LevelController : MonoBehaviour, IService
    {
        [SerializeField] private ConsoleView consoleView;

        [Header("Settings")]
        [SerializeField] private float secondsDelayBeforeAIAnswear;

        private LevelInfo levelInfo = new();
        private PlayerGuessInfo userGuessInfo;
        private AIGuesserService aiGuesserService;
        private INumberGenerator numberGenerator;

        public LevelInfo LevelInfo => levelInfo;
        public PlayerGuessInfo UserGuessNumber => userGuessInfo;

        public void Initialize()
        {
            userGuessInfo = new(LabelsStrings.PLAYER_NAME);

            aiGuesserService = ServiceLocator.Instance.Get<AIGuesserService>();
            numberGenerator = ServiceLocator.Instance.Get<INumberGenerator>();

            SetupLevel();
        }

        public void SetupLevel()
        {
            levelInfo.SetLevelStatus(false);
            levelInfo.SetNumber(numberGenerator.GenerateNumber());
            
            aiGuesserService.ResetGuesser();

            consoleView.ClearConsole();
            consoleView.AppendLine(LabelsStrings.START_LEVEL_TEXT);
        }

        public void TryGuessNumber()
        {
            PlayerTryGuessNumber(userGuessInfo);

            if (levelInfo.LevelCompleted)
            {
                return;
            }

            TryAIGuessNumber();
        }

        private async void TryAIGuessNumber()
        {
            levelInfo.SetAITurnState(true);

            await UniTask.Delay(TimeSpan.FromSeconds(secondsDelayBeforeAIAnswear));

            aiGuesserService.GuessNumber();

            PlayerTryGuessNumber(aiGuesserService.PlayerGuessInfo);

            levelInfo.SetAITurnState(false);
        }

        private void PlayerTryGuessNumber(PlayerGuessInfo playerGuessInfo)
        {
            if (playerGuessInfo.NumberInt == levelInfo.Number)
            {
                LevelComplete(playerGuessInfo);
                return;
            }

            levelInfo.SetLastGuessNumber(playerGuessInfo.NumberInt);
            aiGuesserService.CorrectGuessRange(levelInfo.LastGuessNumber, levelInfo.IsLastGuessPlayerNumberLarger);

            consoleView.FailGuess(playerGuessInfo.PlayerName, playerGuessInfo.NumberInt, levelInfo.IsLastGuessPlayerNumberLarger);
            playerGuessInfo.Clear();
        }

        private void LevelComplete(PlayerGuessInfo playerGuessInfo)
        {
            levelInfo.SetLevelStatus(true);
            playerGuessInfo.IncreaseGuessCounter();

            WriteCompleteLevelConsoleMessage(playerGuessInfo);

            playerGuessInfo.Clear();
        }

        private void WriteCompleteLevelConsoleMessage(PlayerGuessInfo winner)
        {
            consoleView.SuccessGuess(winner.PlayerName, winner.NumberString);

            var statsLine = string.Format(LabelsStrings.STATISTICS_OF_PLAYER_TEMPLATE,
                userGuessInfo.PlayerName, userGuessInfo.SuccessGuessCounter,
                aiGuesserService.PlayerGuessInfo.PlayerName, aiGuesserService.PlayerGuessInfo.SuccessGuessCounter);

            consoleView.AppendLine(statsLine);
            consoleView.AppendLine(LabelsStrings.COMPLETE_LEVEL_TEXT);
        }
    }
}