namespace GuessNumber.NumberGenerator
{
    public abstract class BaseNumberGenerator : INumberGenerator
    {
        protected NumberGeneratorSettings settings;

        public BaseNumberGenerator(NumberGeneratorSettings settings)
        {
            this.settings = settings;
        }

        public abstract int GenerateNumber();
    }
}