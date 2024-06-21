using GuessNumber.Keyboard;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuessNumber.View
{
    public class KeyboardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI userGuessNumberLabel;
        [SerializeField] private Button enterButton;
        [SerializeField] private Button removeNumberButton;
        [SerializeField] private List<Key> keys;

        private KeyboardController controller;
        private PlayerGuessInfo playerGuessNumber;

        private void OnDestroy()
        {
            Unsubscribe();
        }

        public void Setup(KeyboardController controller)
        {
            this.controller = controller;

            playerGuessNumber = ServiceLocator.Instance.Get<LevelController>().UserGuessNumber;

            userGuessNumberLabel.text = playerGuessNumber.NumberString;

            Subscribe();
        }

        private void Subscribe()
        {
            foreach (var key in keys)
            {
                key.KeyDown += OnKeyDown;
            }

            enterButton.onClick.AddListener(controller.EnterNumber);
            removeNumberButton.onClick.AddListener(controller.RemoveLastNumber);

            playerGuessNumber.NumberUpdated += OnNumberUpdated;
        }

        private void Unsubscribe()
        {
            foreach (var key in keys)
            {
                key.KeyDown -= OnKeyDown;
            }

            enterButton.onClick.RemoveListener(controller.EnterNumber);
            removeNumberButton.onClick.RemoveListener(controller.RemoveLastNumber);

            playerGuessNumber.NumberUpdated -= OnNumberUpdated;
        }

        private void OnKeyDown(Key key)
        {
            controller.AddNumber(key.KeyValue);
        }

        private void OnNumberUpdated(int number)
        {
            userGuessNumberLabel.text = number.ToString();
        }
    }
}