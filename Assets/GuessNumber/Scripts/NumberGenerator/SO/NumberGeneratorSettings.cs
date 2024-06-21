using Unity.VisualScripting;
using UnityEngine;

namespace GuessNumber.NumberGenerator
{
    [CreateAssetMenu(fileName = "NumberGeneratorSettings", menuName = "GuessNumber/Number Generator Settings")]
    public class NumberGeneratorSettings : ScriptableObject
    {
        [InspectorLabel("Max range number")]
        [Min(0)]
        [SerializeField] private int maxRangeNumber = 0;
        [InspectorLabel("Min range number")]
        [Min(0)]
        [SerializeField] private int minRangeNumber = 0;

        public int MaxRangeNumber => maxRangeNumber;
        public int MinRangeNumber => minRangeNumber;
    }
}