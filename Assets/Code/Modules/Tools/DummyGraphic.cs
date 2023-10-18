using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class DummyGraphic : MaskableGraphic
{
    public override void SetMaterialDirty() {}
    public override void SetVerticesDirty() {}
    protected override void OnPopulateMesh(VertexHelper vh) { vh.Clear(); }
}