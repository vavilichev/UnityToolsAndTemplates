using System.Numerics;

namespace Vavilichev.GameResources
{
    public struct ResourcesBigIntChangedArgs
    {
        public readonly ResourceType ResourceType;
        public readonly BigInteger OldAmount;
        public readonly BigInteger NewAmount;

        public ResourcesBigIntChangedArgs(ResourceType resourceType, BigInteger oldAmount, BigInteger newAmount)
        {
            ResourceType = resourceType;
            OldAmount = oldAmount;
            NewAmount = newAmount;
        }
    }
}