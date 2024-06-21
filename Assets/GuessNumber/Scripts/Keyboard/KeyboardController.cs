using GuessNumber.View;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GuessNumber.Keyboard
{
    public class KeyboardController : MonoBehaviour
    {
        [SerializeField] private KeyboardView keyboardView;

        private LevelController levelController;
        private PlayerGuessInfo playerGuessNumber;

        private List<Key> keys;
        private List<int> keysNumbers;

        public void Initialize()
        {
            Setup();
            RandomKeyboardNumbers();
        }

        public void AddNumber(int number)
        {
            playerGuessNumber.AddNumber(number);
        }

        public void EnterNumber()
        {
            if (levelController.LevelInfo.AITurn)
            {
                return;
            }

            if (levelController.LevelInfo.LevelCompleted)
            {
                levelController.SetupLevel();
                return;
            }

            RandomKeyboardNumbers();
            levelController.TryGuessNumber();
        }

        public void RemoveLastNumber()
        {
            playerGuessNumber.RemoveLastNumber();
        }

        private void Setup()
        {
            keys = GetComponentsInChildren<Key>().ToList();

            levelController = ServiceLocator.Instance.Get<LevelController>();
            playerGuessNumber = levelController.UserGuessNumber;

            keysNumbers = Enumerable.Range(0, keys.Count).ToList();

            keyboardView.Setup(this);
        }

        private void RandomKeyboardNumbers()
        {
            var copyNumberList = new List<int>(keysNumbers);

            for (int i = 0; i < keys.Count; i++)
            {
                var randomIndex = Random.Range(0, copyNumberList.Count);

                keys[i].Setup(copyNumberList[randomIndex]);

                copyNumberList.RemoveAt(randomIndex);
            }
        }
    }
}