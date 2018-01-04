using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// todo : 추가기능 시계방향으로 만들기.
/// </summary>

public class PolygonOld : Image, IMeshModifier
{
    public List<PolygonVertexInfo> vertexInfoList = new List<PolygonVertexInfo>();
    [Range(0f, 360f)]
    public float offset = 0f;

    public float[] animatortest;

    public bool innerPolygon = false;
    [Header("innerPolygon 옵션에서 제로점과 가까울시 width의 예외처리가 되어있지 않음. ")]
    [Range(0f, 1f)]
    public float innerWidth = 1f;
    public bool vertexColorFlag = false;

    [System.Serializable]
    public struct PolygonVertexInfo
    {
        public Color color;
        [Range(0f, 1f)]
        public float length;

        public PolygonVertexInfo(float length)
        {
            color = Color.white;
            this.length = 1f;
        }
    }

    public void ModifyMesh(VertexHelper vh)
    {
        EditMesh(vh);
    }
    public void ModifyMesh(Mesh mesh)
    {
        using (var vh = new VertexHelper(mesh))
        {
            EditMesh(vh);
            vh.FillMesh(mesh);
        }
    }

    void EditMesh(VertexHelper vh)
    {
        vh.Clear();
        int count = vertexInfoList.Count;
        if (count < 3)
            return;//3개서부터 보임 

        for (int n = 0; n < vertexInfoList.Count; n++)
        {
            float angleUnit = 2f * Mathf.PI / vertexInfoList.Count;
            var uv = new Vector3(Mathf.Cos(angleUnit * n + offset) * 0.5f + 0.5f, Mathf.Sin(angleUnit * n + offset) * 0.5f + 0.5f);

            vh.AddVert(GetRadiusPosition(vertexInfoList[n], n), CheckVertexColor(vertexInfoList[n].color), uv);
        }

        if (!innerPolygon)
        {
            int[] v = new int[3] { 0, 1, count - 1 };
            int n = 0;
            while (n < count - 2)
            {
                if (n % 2 == 1) vh.AddTriangle(v[0], v[1], v[2]);
                else vh.AddTriangle(v[0], v[2], v[1]);

                int change = (v[n % 3] == 0) ? 2 : (count - 2 - n) * (n % 2 == 1 ? 1 : -1);
                v[n % 3] += change;
                n++;
            }
        }
        else
        {
            for (int n = 0; n < count; n++)
            {
                float angleUnit = 2f * Mathf.PI / vertexInfoList.Count;
                var uv = new Vector3(Mathf.Cos(angleUnit * n + offset) * 0.5f * (1f - innerWidth) + 0.5f, Mathf.Sin(angleUnit * n + offset) * 0.5f * (1f - innerWidth) + 0.5f);

                vh.AddVert(GetRadiusPosition(vertexInfoList[n], n, 1f - innerWidth), CheckVertexColor(vertexInfoList[n].color), uv);
            }
            for (int n = 0; n < count; n++)
            {
                vh.AddTriangle(n, count + (1 + n) % count, (n + 1) % count);
                vh.AddTriangle(n, n + count, count + (1 + n) % count);
            }
        }
    }
    Vector3 GetRadiusPosition(PolygonVertexInfo info, int index, float scale = 1f)
    {
        if (vertexInfoList.Count < 3)
            return Vector3.zero;

        float width = rectTransform.rect.width / 2 * info.length;
        float height = rectTransform.rect.height / 2 * info.length;

        float angleUnit = 2f * Mathf.PI / vertexInfoList.Count;
        float offsetToAngle = offset / 360 * Mathf.PI * 2;

        Vector3 result = new Vector3(width * Mathf.Cos(angleUnit * index + offsetToAngle), height * Mathf.Sin(angleUnit * index + offsetToAngle));
        return result * scale;
    }

    Color CheckVertexColor(Color vertexColor)
    {
        if (vertexColorFlag)
        {
            return vertexColor;
        }
        else
        {
            return color;
        }
    }
}