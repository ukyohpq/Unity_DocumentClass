using Babeltime.Log;
using LuaInterface;
using UnityEngine;
using Framework.core;
using Framework.core.Components;
using UnityEngine.UI;

namespace Framework.LuaUI.Components
{
    public class DocumentClass : GameObjectLuaBinder
    {
        [SerializeField]
        private string LuaClass = "";
        
        [NoToLua]
        public void SetLuaTable(LuaTable value)
        {
            BindLuaTable(value);
            var luaState = value.GetLuaState();
#if UNITY_EDITOR
            var oldTop = luaState.LuaGetTop();
#endif
            luaState.LuaGetRef(value.GetReference());
            BindLuaClass(luaState);
#if UNITY_EDITOR
            var curTop = luaState.LuaGetTop();
            if (curTop != oldTop)
            {
                BTLog.Error("stack is unbalanced oldTop:{0} curTop:{1}", oldTop, curTop);
            }
#endif
        }

        public string GetLuaClassName()
        {
            return LuaClass;
        }
        
//        该函数在调用前，必须保证当前lua栈顶有一个lua的Prefab对象。也就是说，栈不为空!
        private void BindLuaClass(LuaState luaState)
        {
            if (luaState.LuaIsNil(-1))
            {
                BTLog.Error("该函数在调用前必须保证lua栈顶上有一个lua的Prefab对象");
                return;
            }
            BindFieldsOnTrans(transform, luaState, luaState.LuaGetTop());
//            完成绑定之后，广播complete事件
            luaState.LuaGetField(-1, "DispatchMessage");
            if (luaState.LuaIsNil(-1))
            {
                luaState.LuaPop(1);
                BTLog.Warning("Prefab Lua must has Method:DispatchMessage");
                return;
            }
            luaState.LuaInsert(-2);
            luaState.Push("INIT");
//            BTLog.Error("BindLuaClass INIT:{0}", this.name);
            luaState.LuaSafeCall(2, 0, 0, 0);
        }

        private void BindFieldsOnTrans(Transform trans, LuaState luaState, int topIdx)
        {
            BTLog.Debug("BindFieldsOnTrans trans:{0} top:{1}", trans.name, topIdx);
            var numChildren = trans.childCount;
            for (int i = 0; i < numChildren; i++)
            {
                var child = trans.GetChild(i);
                var childName = child.name;
                if (childName == "") continue;
                var suffix = Utils.GetSuffixOfGoName(childName);
//                如果不是合法后缀，则直接进入下一级，检测子go有没有需要绑定的
                if (!Utils.IsValidSuffix(suffix))
                {
                    BindFieldsOnTrans(child, luaState, topIdx);
                    continue;
                }

                GameObjectLuaBinder childBinder;
                LuaTable lt = null;
//                对Doc进行特殊处理，这个不能直接绑定cs组件，需要创建一个lua对象，然后进行绑定
                switch (suffix)
                {
                    case ComponentSuffix.Doc:
                        var childDoc = child.GetComponent<DocumentClass>();
                        luaState.LuaGetField(topIdx, childName);
                        lt = luaState.ToVariant(-1) as LuaTable;
                        childDoc.BindLuaTable(lt);
                        childDoc.BindLuaClass(luaState);
                        childDoc.Container = childDoc.transform;
                        break;
                    case ComponentSuffix.Button:
                        var childBtn = child.GetComponent<LuaButton>();
                        if (childBtn == null)
                        {
                            childBtn = child.gameObject.AddComponent<LuaButton>();
                        }
                        luaState.LuaGetField(topIdx, childName);
                        lt = luaState.ToVariant(-1) as LuaTable;
                        childBtn.BindLuaTable(lt);
                        luaState.LuaPop(1);
                        break;
                    case ComponentSuffix.Text:
                        childBinder = child.GetComponent<GameObjectLuaBinder>();
                        if (childBinder == null)
                        {
                            childBinder = child.gameObject.AddComponent<GameObjectLuaBinder>();
                        }
                        luaState.LuaGetField(topIdx, childName);
                        lt = luaState.ToVariant(-1) as LuaTable;
                        childBinder.BindLuaTable(lt);
                        luaState.LuaGetField(-1, "text");
                        var t = luaState.ToVariant(-1) as string;
                        childBinder.GetComponent<Text>().text = t;
                        luaState.LuaPop(2);
                        break;
                    case ComponentSuffix.ScrollView:
                        childBinder = child.GetComponent<GameObjectLuaBinder>();
                        if (childBinder == null)
                        {
                            childBinder = child.gameObject.AddComponent<GameObjectLuaBinder>();
                        }
                        luaState.LuaGetField(topIdx, childName);
                        lt = luaState.ToVariant(-1) as LuaTable;
                        childBinder.BindLuaTable(lt);
                        childBinder.Container = childBinder.transform.Find("Viewport/Content");
                        luaState.LuaPop(1);
                        break;
                    case ComponentSuffix.Image:
                        childBinder = child.GetComponent<GameObjectLuaBinder>();
                        if (childBinder == null)
                        {
                            childBinder = child.gameObject.AddComponent<GameObjectLuaBinder>();
                        }
                        luaState.LuaGetField(topIdx, childName);
                        lt = luaState.ToVariant(-1) as LuaTable;
                        childBinder.BindLuaTable(lt);
                        luaState.LuaGetField(-1, "imageStr");
                        var imageStr = luaState.LuaToString(-1);
                        luaState.LuaPop(2);
                        if (imageStr == "")
                        {
                            break;
                        }
                        var strParams = imageStr.Split('|');
                        switch (strParams.Length)
                        {
                            case 1:
                                LoaderManager.LoadImage(lt, strParams[0]);
                                break;
                            case 2:
                                LoaderManager.LoadAtlasImage(lt, strParams[0], strParams[1]);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        childBinder = child.GetComponent<GameObjectLuaBinder>();
                        if (childBinder == null)
                        {
                            childBinder = child.gameObject.AddComponent<GameObjectLuaBinder>();
                        }
                        luaState.LuaGetField(topIdx, childName);
                        lt = luaState.ToVariant(-1) as LuaTable;
                        childBinder.BindLuaTable(lt);
                        luaState.LuaPop(1);
                        break;
                }

                BTLog.Debug("bind {0}. name:{1} childName:{2}", suffix, trans.name, childName);

            }
        }
        
//        TODO 目前暂时确定父类一定是Framework.UI.Prefab类，不使用自定义父类，因为检测自定义父类是从Framework.UI.Prefab继承而来，比较麻烦，而且考虑使用状态而不是继承来重用Prefab
//        [SerializeField]
//        private string SuperClass = "";
        // Start is called before the first frame update
        void Start()
        {
//            TODO 这里用于非lua中通过New创建出来的，直接拖拽到场景中的prefab，其luatable需要自己创建并绑定。但考虑到这可能是一种不存在的需求，故以后可能会删除
            if (!hasLuaObj())
            {
                CreatePrefabAndBindLuaClass(MainGame.Ins.LuaState);
            }
//            BTLog.Error("documentclass contextID:{0}", contextID);
        }

//        通过在cs端创建lua的Prefab对象进行绑定
        public override void CreatePrefabAndBindLuaClass(LuaState luaState)
        {
            base.CreatePrefabAndBindLuaClass(luaState);
            PushLuaInstance(luaState, Utils.MakeClassName(LuaClass));
            BindLuaClass(luaState);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        protected void OnDestroy()
        {
            base.OnDestroy();
        }
        
        
    }
}

