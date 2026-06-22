using System;
using UnityEngine;

[Serializable]
public record PlayerTextureData
{
    public int ID;
    public Texture Texture;
}


public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private Texture defaultTexture = null;

    [Space]
    [SerializeField] private MeshRenderer rederer = null;
    [SerializeField] private PlayerTextureData[] skinsData = null;
    private Material material = null;
    private int skinIndex = 0;

    private void Awake()
    {
        material = rederer.sharedMaterial;

        InitSkin();
    }

    private void InitSkin()
    {
        skinIndex = MainManager.instance.skinIndex;

        foreach( PlayerTextureData textureData in skinsData )
        {
            if( textureData.ID == skinIndex )
            {
                material.SetTexture( "_MainTex", textureData.Texture );
                return;
            }
        }

        material.SetTexture( "_MainTex", defaultTexture );
    }
}
