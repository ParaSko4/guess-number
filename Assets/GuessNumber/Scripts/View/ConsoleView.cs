using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace GuessNumber.View
{
    public class ConsoleView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI output;

        [Header("Settings")]
        [SerializeField] private int maxOutputLineCapacity;

        private const string PLAYER_SUCCESS_ANSWEAR = "[{0}]: Guess success! Correct number is {1}";
        private const string USER_FAIL_ANSWEAR = "[{0}]: Need <color=\"red\">{1}</color> than <b>{2}</b>";
        private const string NEED_LARGER_THAN_GUESS_NUMBER = "LARGER";
        private const string NEED_LESS_THAN_GUESS_NUMBER = "LESS";

        private List<int> linesLength = new();
        private StringBuilder outputStringBuilder = new();

        private int lineCount = 0;

        public void SuccessGuess(string userName, string correctNumber)
        {
            var line = string.Format(PLAYER_SUCCESS_ANSWEAR, userName, correctNumber);

            AppendLine(line);
        }

        public void FailGuess(string userName, int number, bool guessNumberLarger)
        {
            var line = string.Format(USER_FAIL_ANSWEAR, userName, guessNumberLarger ?
                NEED_LESS_THAN_GUESS_NUMBER : NEED_LARGER_THAN_GUESS_NUMBER,
                number);

            AppendLine(line);
        }

        public void AppendLine(string line)
        {
            if (lineCount >= maxOutputLineCapacity)
            {
                outputStringBuilder.Remove(0, linesLength[0] + 2);

                lineCount--;
                linesLength.RemoveAt(0);
            }

            outputStringBuilder.AppendLine(line);

            lineCount++;
            linesLength.Add(line.Length);

            output.text = outputStringBuilder.ToString();
        }

        public void ClearConsole()
        {
            lineCount = 0;

            linesLength.Clear();
            outputStringBuilder.Clear();

            output.text = string.Empty;
        }
    }
}