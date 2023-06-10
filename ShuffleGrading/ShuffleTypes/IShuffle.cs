namespace ShuffleGrading.ShuffleTypes
{
    public interface IShuffle
    {
        string? Name { get; }
        void Shuffle(int[] deck, bool[] origins);
    }
}
