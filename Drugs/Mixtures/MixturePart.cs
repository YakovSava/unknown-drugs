namespace UnknownDrugs.Drugs
{
    public readonly struct MixturePart
    {
        public MixturePart(string id, float share)
        {
            Id = id;
            Share = share;
        }

        public string Id { get; }

        public float Share { get; }
    }
}
