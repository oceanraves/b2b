using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFlash : MonoBehaviour
{
    private Renderer rendererToEdit;
    private List<Material> ogMats = new List<Material>();

    private bool _isRed = false;

    Color red = Color.red;

    Material allMat;
    void Start()
    {
        rendererToEdit = gameObject.GetComponent<Renderer>();

        rendererToEdit.material.EnableKeyword("_NORMALMAP");
        rendererToEdit.material.EnableKeyword("_METALLICGLOSSMAP");
        rendererToEdit.material.EnableKeyword("_PARALLAXMAP");       

        GeatOgColors();
    }
    private void GeatOgColors()
    {
        foreach (Material mat in rendererToEdit.materials)
        {
            mat.color = red;
            mat.SetFloat("_Smoothness", 0f);
            mat.SetFloat("_Metallic", 0f);
            mat.mainTexture = null;
            mat.SetTexture("_BumpMap", null);
            mat.SetTexture("_MetallicGlossMap", null);
            mat.SetTexture("_ParllaxMap", null);
        }

        _isRed = true;
    }
    private void Update()
    {
        if (_isRed)
        {
            foreach (Material mat in rendererToEdit.materials)
            {
                mat.EnableKeyword("_EMISSION");
                float emission = Mathf.PingPong(Time.time, 0.5f);
                Color finalColor = red * Mathf.LinearToGammaSpace(emission);
                mat.SetColor("_EmissionColor", finalColor);
            }
        }
    }


    /*
    private static void AssignPlayerMaterialPreset(GameObject obj, int playerIndex, PlayerGraphicsPreset preset)
    {
        SkinnedMeshRenderer[] renderers = obj.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            renderer.material = preset.playerModelMaterial;
        }

        MeshRenderer[] firstChildRenderers = obj.GetComponentsInChildren<MeshRenderer>(true);

        foreach (var renderer in firstChildRenderers)
        {
            renderer.material.EnableKeyword("_EMISSION");
            renderer.material.SetColor("_EmissionColor", preset.attackColor);
        }

        playerGraphicsPresetReferences.Add(playerIndex, preset.playerModelMaterial.name);
    }

    public Color GetPlayerColor(int playerIndex)
    {
        return graphicsPresetsReferences[playerGraphicsPresetReferences[playerIndex]].attackColor;
    }
    */
}
