using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuessNumber.Keyboard
{
    [RequireComponent(typeof(Button))]
    public class Key : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        public event Action<Key> KeyDown;

        private Button keyButton;

        public int KeyValue { get; private set; }

        private void Awake()
        {
            keyButton = GetComponent<Button>();

            Subscrbibe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        public void Setup(int keyValue)
        {
            KeyValue = keyValue;

            label.text = keyValue.ToString();
        }

        private void Subscrbibe()
        {
            keyButton.onClick.AddListener(OnKeyClicked);
        }

        private void Unsubscribe()
        {
            keyButton.onClick.RemoveListener(OnKeyClicked);
        }

        private void OnKeyClicked()
        {
            KeyDown?.Invoke(this);
        }
    }
}
