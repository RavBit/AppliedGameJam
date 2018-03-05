using UnityEngine;

[ExecuteInEditMode]
public class GraphicsManager : MonoBehaviour
{
    public Material EffectMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        EffectMaterial.SetFloat("Magnitude", 0.5f);
        if (EffectMaterial != null)
            Graphics.Blit(src, dst, EffectMaterial);
    }
}
