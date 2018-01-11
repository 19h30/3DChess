using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Dùng để load các resource (chỉ 1 lần duy nhất!)
/// </summary>
public class ResourcesCTL
{
    private static ResourcesCTL _instance = null;
    public static ResourcesCTL Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ResourcesCTL();
            return _instance;
        }
    }


    #region Thuộc tính
    private Material _blackCellMaterial = null;
    public Material BlackCellMaterial
    {
        get
        {
            if (_blackCellMaterial == null)
                _blackCellMaterial = Resources.Load<Material>("Materials/BlackCell");
            return _blackCellMaterial;
        }
    }

    private Material _whiteCellMaterial = null;
    public Material WhiteCellMaterial
    {
        get
        {
            if(_whiteCellMaterial == null)
                _whiteCellMaterial = Resources.Load<Material>("Materials/WhiteCell");
            return _whiteCellMaterial;
        }
    }

    #endregion




    private ResourcesCTL()
    {
    }

    
    private Dictionary<string, GameObject> _dict = new Dictionary<string,GameObject>();
    //Lấy gameobject từ trong resource ra!
    public GameObject GetGameObject(string path)
    {
        if(_dict.ContainsKey(path) == false)
            _dict.Add(path, Resources.Load<GameObject>(path));
        return _dict[path];
    }


}
