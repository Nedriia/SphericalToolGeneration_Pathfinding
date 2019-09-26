using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iso : MonoBehaviour
{

    // 점역할을 할 오브젝트
    public GameObject prefab;

    // 메테리얼
    public Material material;

    private void Awake()
    {
        StartCoroutine(Creat());

        StartCoroutine(DatCreate());
    }

    /// <summary>
    /// 점 Coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator DatCreate()
    {
        if (prefab != null)
        {
            GameObject objP = new GameObject();

            float radius = 2;

            Vector3[] vertList = GetCartesianCoordinate(radius);

            for (int i = 0; i < vertList.Length; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab, objP.transform);
                obj.transform.localPosition = vertList[i];
                yield return 0;
            }
            prefab.SetActive(false);
        }
    }

    /// <summary>
    /// 오브젝트 생성 Coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator Creat()
    {
        // 오브젝트 생성
        GameObject obj = new GameObject("obj Cartesian Coordinate");
        // 오브젝트 종속
        obj.transform.parent = this.transform;
        // 메쉬 필터 추가
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        // 메쉬 랜더러 추가
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();
        // 메쉬 생성
        Mesh m = PolygonMesh(2);
        // 메쉬필터에 메쉬 설정
        mf.sharedMesh = m;
        // 메쉬 경계면 크기 계산
        m.RecalculateBounds();

        yield return 0;
    }

    /// <summary>
    /// vertices 생성
    /// </summary>
    /// <param name=”radius”>반지름</param>
    /// <returns>vertices 배열</returns>
    Vector3[] GetCartesianCoordinate(float radius)
    {
        // 참조
        // https://en.wikipedia.org/wiki/Truncated_icosahedron
        // http://wiki.unity3d.com/index.php/ProceduralPrimitives#C.23_-_IcoSphere
        //

        // radius
        float R = radius;
        // Golden Ratio
        float G = (1f + Mathf.Sqrt(5f)) / 2f;

        // Truncated_icosahedron 공식 변수를 이용해 간소화
        float G3 = 3 * G;
        float G2 = 2 * G;
        float GP2 = 2 + G;
        float G2P1 = 1 + 2 * G;

        List<Vector3> vertList = new List<Vector3>();

        // 임시 변수명 중복 사용을 위해 중괄호
        {
            List<Vector3> list = new List<Vector3>();

            // Truncated_icosahedron 공식 사용부분
            list.Add(new Vector3(+0f, -1f, +G3).normalized * radius);
            list.Add(new Vector3(+0f, +1f, +G3).normalized * radius);
            list.Add(new Vector3(+0f, -1f, -G3).normalized * radius);
            list.Add(new Vector3(+0f, +1f, -G3).normalized * radius);

            // IcoSphere 참조한 부분
            for (int i = 0; i < list.Count; i++)
            {
                vertList.Add(new Vector3(list[i].x, list[i].y, list[i].z));
                vertList.Add(new Vector3(list[i].z, list[i].x, list[i].y));
                vertList.Add(new Vector3(list[i].y, list[i].z, list[i].x));
            }
        }
        // 임시 변수명 중복 사용을 위해 중괄호
        {
            List<Vector3> list = new List<Vector3>();

            // Truncated_icosahedron 공식 사용부분
            list.Add(new Vector3(-1f, +GP2, +G2).normalized * radius);
            list.Add(new Vector3(-1f, -GP2, +G2).normalized * radius);
            list.Add(new Vector3(+1f, +GP2, +G2).normalized * radius);
            list.Add(new Vector3(+1f, -GP2, +G2).normalized * radius);

            list.Add(new Vector3(-1f, +GP2, -G2).normalized * radius);
            list.Add(new Vector3(-1f, -GP2, -G2).normalized * radius);
            list.Add(new Vector3(+1f, +GP2, -G2).normalized * radius);
            list.Add(new Vector3(+1f, -GP2, -G2).normalized * radius);

            // IcoSphere 참조한 부분
            for (int i = 0; i < list.Count; i++)
            {
                vertList.Add(new Vector3(list[i].x, list[i].y, list[i].z));
                vertList.Add(new Vector3(list[i].z, list[i].x, list[i].y));
                vertList.Add(new Vector3(list[i].y, list[i].z, list[i].x));
            }
        }
        // 임시 변수명 중복 사용을 위해 중괄호
        {
            List<Vector3> list = new List<Vector3>();

            // Truncated_icosahedron 공식 사용부분
            list.Add(new Vector3(-2f, +(1 + G2), +G).normalized * radius);
            list.Add(new Vector3(-2f, -(1 + G2), +G).normalized * radius);
            list.Add(new Vector3(+2f, +(1 + G2), +G).normalized * radius);
            list.Add(new Vector3(+2f, -(1 + G2), +G).normalized * radius);

            list.Add(new Vector3(-2f, +(1 + G2), -G).normalized * radius);
            list.Add(new Vector3(-2f, -(1 + G2), -G).normalized * radius);
            list.Add(new Vector3(+2f, +(1 + G2), -G).normalized * radius);
            list.Add(new Vector3(+2f, -(1 + G2), -G).normalized * radius);

            // IcoSphere 참조한 부분
            for (int i = 0; i < list.Count; i++)
            {
                vertList.Add(new Vector3(list[i].x, list[i].y, list[i].z));
                vertList.Add(new Vector3(list[i].z, list[i].x, list[i].y));
                vertList.Add(new Vector3(list[i].y, list[i].z, list[i].x));
            }
        }

        return vertList.ToArray();
    }

    Mesh PolygonMesh(float radius)
    {
        // 메쉬 생성
        Mesh mesh = new Mesh();
        //메쉬 이름
        mesh.name = "Cartesian Coordinate Mesh";

        List<Vector3> vertList = new List<Vector3>();

        mesh.vertices = GetCartesianCoordinate(radius);

        List<int> trianglesList = new List<int>();

        List<Pentagon> trianglesPentagon = new List<Pentagon>();



        trianglesPentagon.Add(new Pentagon(00, 49, 15, 21, 37));
        trianglesPentagon.Add(new Pentagon(01, 50, 16, 22, 38));
        trianglesPentagon.Add(new Pentagon(02, 48, 17, 23, 36));
        trianglesPentagon.Add(new Pentagon(03, 43, 18, 12, 55));
        trianglesPentagon.Add(new Pentagon(04, 44, 19, 13, 56));
        trianglesPentagon.Add(new Pentagon(05, 42, 20, 14, 54));
        trianglesPentagon.Add(new Pentagon(06, 40, 33, 27, 52));
        trianglesPentagon.Add(new Pentagon(07, 41, 34, 28, 53));
        trianglesPentagon.Add(new Pentagon(08, 39, 35, 29, 51));
        trianglesPentagon.Add(new Pentagon(09, 58, 24, 30, 46));
        trianglesPentagon.Add(new Pentagon(10, 59, 25, 31, 47));
        trianglesPentagon.Add(new Pentagon(11, 57, 26, 32, 45));



        for (int i = 0; i < trianglesPentagon.Count; i++)
        {
            trianglesList.Add(trianglesPentagon[i].dot1);
            trianglesList.Add(trianglesPentagon[i].dot2);
            trianglesList.Add(trianglesPentagon[i].dot3);

            trianglesList.Add(trianglesPentagon[i].dot1);
            trianglesList.Add(trianglesPentagon[i].dot3);
            trianglesList.Add(trianglesPentagon[i].dot4);

            trianglesList.Add(trianglesPentagon[i].dot1);
            trianglesList.Add(trianglesPentagon[i].dot4);
            trianglesList.Add(trianglesPentagon[i].dot5);
        }

        List<Hexagon> trianglesHexagon = new List<Hexagon>();

        trianglesHexagon.Add(new Hexagon(00, 03, 55, 31, 25, 49));
        trianglesHexagon.Add(new Hexagon(03, 00, 37, 13, 19, 43));
        trianglesHexagon.Add(new Hexagon(04, 01, 38, 14, 20, 44));
        trianglesHexagon.Add(new Hexagon(01, 04, 56, 32, 26, 50));
        trianglesHexagon.Add(new Hexagon(02, 05, 54, 30, 24, 48));
        trianglesHexagon.Add(new Hexagon(05, 02, 36, 12, 18, 42));
        trianglesHexagon.Add(new Hexagon(09, 06, 52, 28, 34, 58));
        trianglesHexagon.Add(new Hexagon(06, 09, 46, 22, 16, 40));
        trianglesHexagon.Add(new Hexagon(07, 10, 47, 23, 17, 41));
        trianglesHexagon.Add(new Hexagon(10, 07, 53, 29, 35, 59));
        trianglesHexagon.Add(new Hexagon(08, 11, 45, 21, 15, 39));
        trianglesHexagon.Add(new Hexagon(11, 08, 51, 27, 33, 57));
        trianglesHexagon.Add(new Hexagon(12, 36, 23, 47, 31, 55));
        trianglesHexagon.Add(new Hexagon(13, 37, 21, 45, 32, 56));
        trianglesHexagon.Add(new Hexagon(14, 38, 22, 46, 30, 54));
        trianglesHexagon.Add(new Hexagon(15, 49, 25, 59, 35, 39));
        trianglesHexagon.Add(new Hexagon(16, 50, 26, 57, 33, 40));
        trianglesHexagon.Add(new Hexagon(17, 48, 24, 58, 34, 41));
        trianglesHexagon.Add(new Hexagon(18, 43, 19, 44, 20, 42));
        trianglesHexagon.Add(new Hexagon(27, 51, 29, 53, 28, 52));

        for (int i = 0; i < trianglesHexagon.Count; i++)
        {
            trianglesList.Add(trianglesHexagon[i].dot1);
            trianglesList.Add(trianglesHexagon[i].dot2);
            trianglesList.Add(trianglesHexagon[i].dot6);

            trianglesList.Add(trianglesHexagon[i].dot3);
            trianglesList.Add(trianglesHexagon[i].dot4);
            trianglesList.Add(trianglesHexagon[i].dot5);

            trianglesList.Add(trianglesHexagon[i].dot2);
            trianglesList.Add(trianglesHexagon[i].dot5);
            trianglesList.Add(trianglesHexagon[i].dot6);

            trianglesList.Add(trianglesHexagon[i].dot3);
            trianglesList.Add(trianglesHexagon[i].dot5);
            trianglesList.Add(trianglesHexagon[i].dot2);
        }

        // triangles 적용
        mesh.triangles = trianglesList.ToArray();
        // uv 생성
        mesh.uv = new Vector2[mesh.vertices.Length];
        // Normal 적용
        mesh.RecalculateNormals();
        // Bound 적용
        mesh.RecalculateBounds();

        return mesh;
    }

    // 오각형 좌표 저장용
    class Pentagon
    {
        public int dot1;
        public int dot2;
        public int dot3;
        public int dot4;
        public int dot5;

        public Pentagon(int d1, int d2, int d3, int d4, int d5)
        {
            this.dot1 = d1;
            this.dot2 = d2;
            this.dot3 = d3;
            this.dot4 = d4;
            this.dot5 = d5;
        }
    }
    // 육각형 좌표 저장용
    class Hexagon
    {
        public int dot1;
        public int dot2;
        public int dot3;
        public int dot4;
        public int dot5;
        public int dot6;

        public Hexagon(int d1, int d2, int d3, int d4, int d5, int d6)
        {
            this.dot1 = d1;
            this.dot2 = d2;
            this.dot3 = d3;
            this.dot4 = d4;
            this.dot5 = d5;
            this.dot6 = d6;
        }
    }
}