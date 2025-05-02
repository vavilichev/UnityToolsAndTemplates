namespace Vavilichev.GameResources
{
    public struct ResourcesIntChangedArgs
    {
        public readonly ResourceType ResourceType;
        public readonly int OldAmount;
        public readonly int NewAmount;

        public ResourcesIntChangedArgs(ResourceType resourceType, int oldAmount, int newAmount)
        {
            ResourceType = resourceType;
            OldAmount = oldAmount;
            NewAmount = newAmount;
        }
    }
}