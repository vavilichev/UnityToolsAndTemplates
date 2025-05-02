using System;
using System.Collections.Generic;
using System.Numerics;
using R3;
using UnityEngine;

namespace Vavilichev.GameResources
{
    public class ResourcesBigIntService
    {
        private readonly Dictionary<ResourceType, ResourceDataBigInt> _resourcesBigIntMap = new();
        private readonly Dictionary<ResourceType, BehaviorSubject<ResourcesBigIntChangedArgs>> _resourcesBigIntChangedSubjectMap = new();

        public ResourcesBigIntService(ResourcesDataBigInt resourcesBigInt)
        {
            foreach (var resource in resourcesBigInt.Resources)
            {
                _resourcesBigIntMap[resource.ResourceType] = resource;
                var defaultValueParams = new ResourcesBigIntChangedArgs(resource.ResourceType, 0, resource.Amount);
                var createdSubject = new BehaviorSubject<ResourcesBigIntChangedArgs>(defaultValueParams);
                _resourcesBigIntChangedSubjectMap[resource.ResourceType] = createdSubject;
            }
        }

        public bool AddResource(ResourceType resourceType, BigInteger amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Amount must be greater than 0");
            }
            
            if (!_resourcesBigIntMap.ContainsKey(resourceType))
            {
                Debug.LogWarning($"ResourceType {resourceType} is not registered, creating a new one.");
                var createdResource = new ResourceDataBigInt() { ResourceType = resourceType };
                _resourcesBigIntMap.Add(resourceType, createdResource);
                
                var createdSubject = new BehaviorSubject<ResourcesBigIntChangedArgs>(new ResourcesBigIntChangedArgs(resourceType, 0, amount));
                _resourcesBigIntChangedSubjectMap[resourceType] = createdSubject;
            }
            
            var resource = _resourcesBigIntMap[resourceType];
            var oldAmount = resource.Amount;
            resource.Amount += amount;

            var subject = _resourcesBigIntChangedSubjectMap[resourceType];
            subject.OnNext(new ResourcesBigIntChangedArgs(resourceType, oldAmount, resource.Amount));
            return true;
        }

        public bool SpendResource(ResourceType resourceType, BigInteger amount)
        {
            if (!IsEnoughResources(resourceType, amount))
            {
                return false;
            }

            var resource = _resourcesBigIntMap[resourceType];
            var oldAmount = resource.Amount;
            resource.Amount -= amount;
            
            var subject = _resourcesBigIntChangedSubjectMap[resourceType];
            subject.OnNext(new ResourcesBigIntChangedArgs(resourceType, oldAmount, resource.Amount));
            return true;
        }

        public bool IsEnoughResources(ResourceType resourceType, BigInteger amount)
        {
            if (!_resourcesBigIntMap.ContainsKey(resourceType))
            {
                throw new Exception($"ResourceType is not registered: {resourceType}");
            }
            
            var resource = _resourcesBigIntMap[resourceType];

            return resource.Amount >= amount;
        }
        
        public Observable<ResourcesBigIntChangedArgs> ObserveResourcesBigInt(ResourceType resourceType)
        {
            return _resourcesBigIntChangedSubjectMap[resourceType];
        }
    }
}