using UnityEngine;

namespace FLuaUI.core.loader
{
    public interface IAssetsAPI
    {
        Sprite LoadSprite(string path);
        GameObject LoadPrefab(string path);
        Object[] LoadAtlas(string atlas);
    }
}