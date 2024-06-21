namespace GuessNumber.NumberGenerator
{
    public interface INumberGenerator : IService
    {
        int GenerateNumber();
    }
}