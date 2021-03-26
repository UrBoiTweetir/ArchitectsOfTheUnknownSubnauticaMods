using UnityEngine;

namespace ArchitectsLibrary.Utility
{
    public static class MaterialUtils
    {
        // this method is totally not yoinked from ECCLibrary :)
        public static void ApplySNShaders(GameObject prefab)
        {
            var renderers = prefab.GetComponentsInChildren<Renderer>(true);
            var newShader = Shader.Find("MarmosetUBER");
            for (int i = 0; i < renderers.Length; i++)
            {
                for (int j = 0; j < renderers[i].materials.Length; j++)
                {
                    Material material = renderers[i].materials[j];
                    Texture specularTexture = material.GetTexture("_SpecGlossMap");
                    Texture emissionTexture = material.GetTexture("_EmissionMap");
                    material.shader = newShader;

                    material.DisableKeyword("_SPECGLOSSMAP");
                    material.DisableKeyword("_NORMALMAP");
                    if (specularTexture != null)
                    {
                        material.SetTexture("_SpecTex", specularTexture);
                        material.SetFloat("_SpecInt", 1);
                        material.SetFloat("_Shininess", 8);
                        material.EnableKeyword("MARMO_SPECMAP");
                        material.SetColor("_SpecColor", new Color(1f, 1f, 1f, 1f));
                        material.SetFloat("_Fresnel", 0.24f);
                        material.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
                    }
                    if (material.IsKeywordEnabled("_EMISSION"))
                    {
                        material.EnableKeyword("MARMO_EMISSION");
                        material.SetFloat("_EnableGlow", 1f);
                        material.SetTexture("_Illum", emissionTexture);
                        material.SetFloat("_GlowStrength", 1);
                        material.SetFloat("_GlowStrengthNight", 1);
                    }

                    if (material.GetTexture("_BumpMap"))
                    {
                        material.EnableKeyword("MARMO_NORMALMAP");
                    }

                    if(material.name.Contains("Cutout"))
                    {
                        material.EnableKeyword("MARMO_ALPHA_CLIP");
                    }
                    if (material.name.Contains("Transparent"))
                    {
                        material.EnableKeyword("_ZWRITE_ON");
                        material.EnableKeyword("WBOIT");
                        material.SetInt("_ZWrite", 0);
                        material.SetInt("_Cutoff", 0);
                        material.SetFloat("_SrcBlend", 1f);
                        material.SetFloat("_DstBlend", 1f);
                        material.SetFloat("_SrcBlend2", 0f);
                        material.SetFloat("_DstBlend2", 10f);
                        material.SetFloat("_AddSrcBlend", 1f);
                        material.SetFloat("_AddDstBlend", 1f);
                        material.SetFloat("_AddSrcBlend2", 0f);
                        material.SetFloat("_AddDstBlend2", 10f);
                        material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack | MaterialGlobalIlluminationFlags.RealtimeEmissive;
                        material.renderQueue = 3101;
                        material.enableInstancing = true;
                    }
                }
            }
        }
    }
}