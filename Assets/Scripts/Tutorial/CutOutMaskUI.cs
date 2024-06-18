
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class CutOutMaskUI : Image
{
    public override Material materialForRendering 
    {
        get
        {
            Material newMaterial = new Material(base.materialForRendering);
            newMaterial.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return newMaterial;
        }
    }
}
