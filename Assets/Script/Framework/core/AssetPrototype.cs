using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.core
{
    public class AssetPrototype
    {
        private GameObject prototype;
        private List<int> insList;
        public AssetPrototype(GameObject prototype)
        {
            if (prototype == null)
            {
                throw new Exception("can not set null prototype");
            }
            this.prototype = prototype;
            insList = new List<int>();
        }
        public GameObject GetGameObject()
        {
            var go = GameObject.Instantiate(this.prototype);
            insList.Add(go.GetInstanceID());
            return go;
        }
    }
}