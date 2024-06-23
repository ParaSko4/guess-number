using GuessNumber.AI;
using GuessNumber.Keyboard;
using GuessNumber.NumberGenerator;
using UnityEngine;

namespace GuessNumber
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private LevelController levelController;
        [SerializeField] private KeyboardController keyboardController;

        [Space]
        [Header("SO")]
        [SerializeField] private NumberGeneratorSettings numberGeneratorSettings;

        private void Start()
        {
            ServiceLocator.Instance.Register<INumberGenerator>(new PositiveNumberGenerator(numberGeneratorSettings));
            ServiceLocator.Instance.Register(new AIGuesserService(numberGeneratorSettings));
            ServiceLocator.Instance.Register(levelController);

            levelController.Initialize();
            keyboardController.Initialize();
        }
    }
}