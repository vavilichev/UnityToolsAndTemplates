using System.Numerics;
using R3;

namespace Vavilichev.GameResources
{
    public class ResourcesService
    {
        private readonly ResourcesIntService _resourcesIntService;
        private readonly ResourcesBigIntService _resourcesBigIntService;
        
        public ResourcesService(ResourcesDataInt resourcesInt, ResourcesDataBigInt resourcesBigInt = null)
        {
            _resourcesIntService = new ResourcesIntService(resourcesInt);

            if (resourcesBigInt != null)
            {
                _resourcesBigIntService = new ResourcesBigIntService(resourcesBigInt);
            }
        }

        public bool AddResource(ResourceType resourceType, int amount)
        {
            return _resourcesIntService.AddResource(resourceType, amount);
        }

        public bool SpendResource(ResourceType resourceType, int amount)
        {
            return _resourcesIntService.SpendResource(resourceType, amount);
        }

        public bool IsEnoughResources(ResourceType resourceType, int amount)
        {
            return _resourcesIntService.IsEnoughResources(resourceType, amount);
        }

        public bool AddResource(ResourceType resourceType, BigInteger amount)
        {
            return _resourcesBigIntService.AddResource(resourceType, amount);
        }

        public bool SpendResource(ResourceType resourceType, BigInteger amount)
        {
            return _resourcesBigIntService.SpendResource(resourceType, amount);
        }

        public bool IsEnoughResources(ResourceType resourceType, BigInteger amount)
        {
            return _resourcesBigIntService.IsEnoughResources(resourceType, amount);
        }

        public Observable<ResourcesIntChangedArgs> ObserveResourceInt(ResourceType resourceType)
        {
            return _resourcesIntService.ObserveResourceInt(resourceType);
        }

        public Observable<ResourcesBigIntChangedArgs> ObserveResourcesBigInt(ResourceType resourceType)
        {
            return _resourcesBigIntService.ObserveResourcesBigInt(resourceType);
        }
    }
}