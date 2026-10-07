using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TMP_Text))]
public class TMPUICurve : MonoBehaviour
{
    [Header("Curve Settings")]
    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 0);
    [Range(-50f, 50f)]
    public float curveStrength = 15f;

    TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void LateUpdate()
    {
        if (text == null) return;

        text.ForceMeshUpdate();
        var mesh = text.mesh;
        var vertices = mesh.vertices;

        float minX = text.bounds.min.x;
        float maxX = text.bounds.max.x;
        float width = maxX - minX;

        if (width <= 0) return;

        for (int i = 0; i < vertices.Length; i++)
        {
            float normalizedX = (vertices[i].x - minX) / width;
            vertices[i].y += curve.Evaluate(normalizedX) * curveStrength;
        }

        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }
}
