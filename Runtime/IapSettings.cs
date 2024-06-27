using System;
using System.Collections.Generic;
using UnityEngine;

namespace VirtueSky.Iap
{
    public class IapSettings : ScriptableObject
    {
        private static IapSettings instance;
        public static IapSettings Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = Resources.Load<IapSettings>(typeof(IapSettings).Name);
                if (instance == null) throw new Exception($"Scriptable setting for {typeof(IapSettings)} must be create before run!");
                return instance;
            }
        }
        [SerializeField] private bool runtimeAutoInit = true;
        [SerializeField] private List<IapDataProduct> iapDataProducts = new List<IapDataProduct>();
        [SerializeField] private bool isValidatePurchase = true;
#if UNITY_EDITOR
        [SerializeField, TextArea] private string googlePlayStoreKey;
        public string GooglePlayStoreKey => googlePlayStoreKey;
#endif
        public bool RuntimeAutoInit => runtimeAutoInit;
        public List<IapDataProduct> IapDataProducts => iapDataProducts;
        public bool IsValidatePurchase => isValidatePurchase;

        public IapDataProduct GetIapProduct(string id)
        {
            foreach (var product in IapDataProducts)
            {
                if (product.Id == id) return product;
            }

            return null;
        }
    }
}