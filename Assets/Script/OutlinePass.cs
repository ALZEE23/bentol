using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlinePass : ScriptableRenderPass
{
    private Material outlineMaterial;

    public OutlinePass(RenderPassEvent evt)
    {
        renderPassEvent = evt;
        Shader outlineShader = Shader.Find("Hidden/OutlineGlow");
        if (outlineShader != null)
            outlineMaterial = new Material(outlineShader);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (outlineMaterial == null) return;

        CommandBuffer cmd = CommandBufferPool.Get("Outline Pass");
        var drawSettings = CreateDrawingSettings(new ShaderTagId("UniversalForward"), ref renderingData, SortingCriteria.None);
        var filterSettings = new FilteringSettings(RenderQueueRange.all, LayerMask.GetMask("OutlineObject"));

        drawSettings.overrideMaterial = outlineMaterial;

        context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filterSettings);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}
