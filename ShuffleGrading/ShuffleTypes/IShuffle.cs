namespace ShuffleGrading.ShuffleTypes
{
    internal interface IShuffle
    {
        string Name { get; }
        void Shuffle(int[] deck, bool[] origins);
    }
}
