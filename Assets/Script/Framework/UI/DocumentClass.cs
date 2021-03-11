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
        [SerializeField]
        private string SuperClass = "";
        // Start is called before the first frame update
        void Start()
        {
            BTLog.Error("documentclass parent:{0}", this.transform.parent.name);
        }
        
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

