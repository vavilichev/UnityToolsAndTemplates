using System.Collections.Generic;
using System.Numerics;
using Newtonsoft.Json;
using R3;
using UnityEngine;

namespace Vavilichev.GameResources.Example
{
    public class GameResourcesExample : MonoBehaviour
    {
        private const string GREXAMPLE_KEY = nameof(GREXAMPLE_KEY);
        
        private ResourcesService _resourcesService;
        private ResourcesData _resourcesData;
        
        private void Start()
        {
            var gameResourcesJson = PlayerPrefs.GetString(GREXAMPLE_KEY, null);
            if (string.IsNullOrEmpty(gameResourcesJson))
            {
                var newGameResources = CreateResourcesData();
                var newGameResourcesJson = JsonConvert.SerializeObject(newGameResources, Formatting.Indented);
                gameResourcesJson = newGameResourcesJson;
            }

            _resourcesData = JsonConvert.DeserializeObject<ResourcesData>(gameResourcesJson);
            _resourcesService = new ResourcesService(_resourcesData.ResourcesInt, _resourcesData.ResourcesBigInt);

            _resourcesService.ObserveResourceInt(ResourceType.HardCurrency).Subscribe(args =>
            {
                var oldValue = args.OldAmount;
                var newValue = args.NewAmount;
                var resourceType = args.ResourceType;

                Debug.Log($"Resources changed. Type: {resourceType}, oldValue = {oldValue}, newValue = {newValue}");
            });
            
            _resourcesService.ObserveResourceInt(ResourceType.Souls).Subscribe(args =>
            {
                var oldValue = args.OldAmount;
                var newValue = args.NewAmount;
                var resourceType = args.ResourceType;

                Debug.Log($"Resources changed. Type: {resourceType}, oldValue = {oldValue}, newValue = {newValue}");
            });
            
            _resourcesService.ObserveResourcesBigInt(ResourceType.SoftCurrency).Subscribe(args =>
            {
                var oldValue = args.OldAmount;
                var newValue = args.NewAmount;
                var resourceType = args.ResourceType;

                Debug.Log($"Resources changed. Type: {resourceType}, oldValue = {oldValue}, newValue = {newValue}");
            });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                var randomPower = Random.Range(0, 30);
                var randomValueStr = "1";
                for (int i = 0; i < randomPower; i++)
                {
                    randomValueStr += "0";
                }

                var randomValue = BigInteger.Parse(randomValueStr);
                _resourcesService.AddResource(ResourceType.SoftCurrency, randomValue);
                return;
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                var randomValue = Random.Range(1, 200);
                _resourcesService.AddResource(ResourceType.HardCurrency, randomValue);
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                var randomValue = Random.Range(1, 200);
                _resourcesService.AddResource(ResourceType.Souls, randomValue);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var gameResourcesJson = JsonConvert.SerializeObject(_resourcesData, Formatting.Indented);
                PlayerPrefs.SetString(GREXAMPLE_KEY, gameResourcesJson);
                Debug.Log($"Saved game resources: {gameResourcesJson}");
            }
        }

        private ResourcesData CreateResourcesData()
        {
            var newGameResources = new ResourcesData();
            newGameResources.ResourcesInt = new ResourcesDataInt();
            newGameResources.ResourcesInt.Resources = new List<ResourceDataInt>();
            newGameResources.ResourcesBigInt = new ResourcesDataBigInt();
            newGameResources.ResourcesBigInt.Resources = new List<ResourceDataBigInt>();
                
            var resourceCoins = new ResourceDataBigInt();
            resourceCoins.Amount = new BigInteger();
            resourceCoins.ResourceType = ResourceType.SoftCurrency;
            newGameResources.ResourcesBigInt.Resources.Add(resourceCoins);
                
            var resourceGems = new ResourceDataInt();
            resourceGems.ResourceType = ResourceType.HardCurrency;
            resourceGems.Amount = 0;
            newGameResources.ResourcesInt.Resources.Add(resourceGems);

            var resourceWood = new ResourceDataInt();
            resourceWood.ResourceType = ResourceType.Souls;
            resourceWood.Amount = 0;
            newGameResources.ResourcesInt.Resources.Add(resourceWood);

            return newGameResources;
        }
    }
    
    
}