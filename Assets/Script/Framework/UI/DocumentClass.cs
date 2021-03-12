using System;
using System.Collections;
using System.Collections.Generic;
using Babeltime.Log;
using UnityEditor;
using UnityEngine;

namespace Framework.UI
{
    public class DocumentClass : MonoBehaviour
    {
        [SerializeField]
        private string LuaClass = "";

        private int contextID;

        public int ContextId
        {
            get { return contextID; }
            set { contextID = value; }
        }

//        TODO 目前暂时确定父类一定是Framework.UI.Prefab类，不使用自定义父类，因为检测自定义父类是从Framework.UI.Prefab继承而来，比较麻烦，而且考虑使用状态而不是继承来重用Prefab
//        [SerializeField]
//        private string SuperClass = "";
        // Start is called before the first frame update
        void Start()
        {
            BTLog.Error("documentclass parent:{0}", this.transform.parent.name);
        }
        
        // Update is called once per frame
        void Update()
        {
            
        }
        
        private void OnDestroy()
        {
            
        }
    }
}

