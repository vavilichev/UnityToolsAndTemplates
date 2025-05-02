using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Vavilichev.GameResources
{
    public class ResourcesIntService
    {
        private readonly Dictionary<ResourceType, ResourceDataInt> _resourcesIntMap = new();
        private readonly Dictionary<ResourceType, BehaviorSubject<ResourcesIntChangedArgs>> _resourcesIntChangedSubjectMap = new();
        
        public ResourcesIntService(ResourcesDataInt resourcesInt)
        {
            foreach (var resource in resourcesInt.Resources)
            {
                _resourcesIntMap[resource.ResourceType] = resource;
                var defaultValueParams = new ResourcesIntChangedArgs(resource.ResourceType, 0, resource.Amount);
                var createdSubject = new BehaviorSubject<ResourcesIntChangedArgs>(defaultValueParams);
                _resourcesIntChangedSubjectMap[resource.ResourceType] = createdSubject;
            }
        }
        
        public bool AddResource(ResourceType resourceType, int amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Amount must be greater than 0");
            }
            
            if (!_resourcesIntMap.ContainsKey(resourceType))
            {
                Debug.LogWarning($"ResourceType {resourceType} is not registered, creating a new one.");
                var createdResource = new ResourceDataInt { ResourceType = resourceType };
                _resourcesIntMap.Add(resourceType, createdResource);
                
                var createdSubject = new BehaviorSubject<ResourcesIntChangedArgs>(new ResourcesIntChangedArgs(resourceType, 0, amount));
                _resourcesIntChangedSubjectMap[resourceType] = createdSubject;
            }
            
            var resource = _resourcesIntMap[resourceType];
            var oldAmount = resource.Amount;
            resource.Amount += amount;

            var subject = _resourcesIntChangedSubjectMap[resourceType];
            subject.OnNext(new ResourcesIntChangedArgs(resourceType, oldAmount, resource.Amount));
            return true;
        }

        public bool SpendResource(ResourceType resourceType, int amount)
        {
            if (!IsEnoughResources(resourceType, amount))
            {
                return false;
            }

            var resource = _resourcesIntMap[resourceType];
            var oldAmount = resource.Amount;
            resource.Amount -= amount;
            
            var subject = _resourcesIntChangedSubjectMap[resourceType];
            subject.OnNext(new ResourcesIntChangedArgs(resourceType, oldAmount, resource.Amount));
            return true;
        }

        public bool IsEnoughResources(ResourceType resourceType, int amount)
        {
            if (!_resourcesIntMap.ContainsKey(resourceType))
            {
                throw new Exception($"ResourceType is not registered: {resourceType}");
            }
            
            var resource = _resourcesIntMap[resourceType];

            return resource.Amount >= amount;
        }
        
        public Observable<ResourcesIntChangedArgs> ObserveResourceInt(ResourceType resourceType)
        {
            return _resourcesIntChangedSubjectMap[resourceType];
        }
    }
}