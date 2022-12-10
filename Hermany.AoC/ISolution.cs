namespace Hermany.AoC
{
    public interface ISolution
    {
        public string P1Assertion { get; }
        public string P2Assertion { get; }
        
        public string P1(string[] input);
        public string P2(string[] input);
    }
}
