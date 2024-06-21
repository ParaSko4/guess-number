using UnityEngine;

namespace GuessNumber.NumberGenerator
{
    public class PositiveNumberGenerator : BaseNumberGenerator
    {
        public PositiveNumberGenerator(NumberGeneratorSettings settings) : base(settings) { }

        public override int GenerateNumber()
        {
            return Random.Range(settings.MinRangeNumber, settings.MaxRangeNumber);
        }
    }
}