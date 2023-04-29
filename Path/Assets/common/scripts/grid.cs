using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    public GameObject m_gridBox;
    public int m_X = 0;
    public int m_Z = 0;

    public int m_objectiveX = 0;
    public int m_objectiveZ = 0;

    public int m_originX = 0;
    public int m_originZ = 0;
    public Vector2 m_originSolid = new Vector2(0, 0);
    public float m_manhattanDistance = 0f;
    public Box m_originBox;
    public Box m_newOriginBox;

    //materialsm_originX
    public Material m_originMat;
    public Material m_objectiveMat;
    public Material m_impassableMat;
    public Material m_routeMat;
    public Material m_neutralMat;

    public int m_f;
    public int m_g = 1;
    public int m_h;


    //list
    public static List<Box> m_gridBoxInstances = new List<Box>();
    //public static List<Box> m_gridBoxStorage = new List<Box>();

    // Start is called before the first frame update
    void Start()
    {
        InstantiateGrid();
        m_newOriginBox = m_originBox;
        while (m_originBox.m_posX != m_objectiveX && m_originBox.m_posZ != m_objectiveZ)
        {
            FindPath();
        }
        // Search();
    }

    // Update is called once per frame
    void Update()
    {
        //FindPath();
    }

    /*
      List<Box> boxNeighbours = new List<Box>();
        Box t_up = gameObject.AddComponent<Box>();
        boxNeighbours.Add(t_up);
        Box t_down = gameObject.AddComponent<Box>();
        boxNeighbours.Add(t_down);
        Box t_left = gameObject.AddComponent<Box>();
        boxNeighbours.Add(t_left);
        Box t_right = gameObject.AddComponent<Box>();
        boxNeighbours.Add(t_right);

        t_up.m_posX = m_newOriginBox.m_up.x;
        t_up.m_posZ = m_newOriginBox.m_up.y;

        t_down.m_posX = m_newOriginBox.m_down.x;
        t_down.m_posZ = m_newOriginBox.m_down.y;

        t_left.m_posX = m_newOriginBox.m_left.x;
        t_left.m_posZ = m_newOriginBox.m_left.y;

        t_right.m_posX = m_newOriginBox.m_right.x;
        t_right.m_posZ = m_newOriginBox.m_right.y;

        float minorManh = 9999;
        Box currentPath = new Box(); //gameObject.AddComponent<Box>();

        for (int i = 0; i < boxNeighbours.Count; ++i)
        {
            float t_manh = ManhattanDist(boxNeighbours[i]);
            if (minorManh > t_manh)
            {
                minorManh = t_manh;
                currentPath = boxNeighbours[i];
            }
        }
        m_newOriginBox = currentPath;

        m_newOriginBox.m_pathMember = true;
        boxNeighbours.Clear();

        for (int i = 0; i < m_gridBoxInstances.Count; ++i)
        {
            //if (m_gridBoxInstances[i].m_posX == currentPath.m_posX && m_gridBoxInstances[i].m_posZ == currentPath.m_posZ)
            if (m_gridBoxInstances[i].m_pathMember)
            {
                m_gridBoxInstances[i].GetComponent<MeshRenderer>().material = m_routeMat;
            }
        }
     */
    void FindPath()
    {
        List<Vector2> boxNeighbours = new List<Vector2>();
        Vector2 t_up = new Vector2(0, 0);
        Vector2 t_down = new Vector2(0, 0);
        Vector2 t_left = new Vector2(0, 0);
        Vector2 t_right = new Vector2(0, 0);

        t_up.y = m_newOriginBox.m_posZ;
        t_up.x = m_newOriginBox.m_posX + 1f;

        t_down.y = m_newOriginBox.m_posZ;
        t_down.x = m_newOriginBox.m_posX - 1f;

        t_left.y = m_newOriginBox.m_posZ - 1f;
        t_left.x = m_newOriginBox.m_posX;

        t_right.y = m_newOriginBox.m_posZ + 1f;
        t_right.x = m_newOriginBox.m_posX;

        boxNeighbours.Add(t_up);
        boxNeighbours.Add(t_down);
        boxNeighbours.Add(t_left);
        boxNeighbours.Add(t_right);
        float minorManh = 9999;
        Vector2 currentPath = new Vector2(0,0); //gameObject.AddComponent<Box>();

        for (int i = 0; i < boxNeighbours.Count; ++i)
        {
            float t_manh = ManhattanDistVector(boxNeighbours[i]);
            if (minorManh > t_manh)
            {
                minorManh = t_manh;
                currentPath.x = boxNeighbours[i].x;
                currentPath.y = boxNeighbours[i].y;
            }
        }
        m_newOriginBox.m_posX = currentPath.x;
        m_newOriginBox.m_posZ = currentPath.y;

        m_newOriginBox.m_pathMember = true;
        boxNeighbours.Clear();

        for (int i = 0; i < m_gridBoxInstances.Count; ++i)
        {
            //if (m_gridBoxInstances[i].m_posX == currentPath.m_posX && m_gridBoxInstances[i].m_posZ == currentPath.m_posZ)
            if (m_gridBoxInstances[i].m_posX == m_newOriginBox.m_posX && m_gridBoxInstances[i].m_posZ == m_newOriginBox.m_posZ)
            {
                Box temp = m_gridBoxInstances[i];
                Vector3 vect = new Vector3(temp.transform.position.x, temp.transform.position.y, temp.transform.position.z);
                temp.transform.position += vect;
                m_gridBoxInstances[i] = temp;
                GameObject gridMesh = Instantiate(m_gridBox, new Vector3(temp.m_posX, temp.transform.position.y+0.5f, temp.m_posZ), Quaternion.identity);
                gridMesh.GetComponent<MeshRenderer>().material = m_routeMat;
            }
        }
        //m_originBox = m_newOriginBox;
    }

    void InstantiateGrid( )
    {
        // searches random points for origin and objective
        m_objectiveX = Random.Range(0, m_X);
        m_objectiveZ = Random.Range(0, m_Z);

        m_originX = Random.Range(0, m_X);
        m_originZ = Random.Range(0, m_Z);

        while (m_originX == m_objectiveX)
        {
            m_originX = Random.Range(0, m_X);
        }
        while (m_originZ == m_objectiveZ)
        {
            m_originZ = Random.Range(0, m_Z);
        }

        // instantiates all the boxes of the grid
        int counter = 0;
        for (int i = 0; i < m_Z; ++i)
        {
            int xCounter = 0;
            for (int j = 0; j < m_X; ++j)
            {
                if (m_objectiveX == j && m_objectiveZ == i)
                {

                    GameObject gridMesh = Instantiate(m_gridBox, new Vector3(transform.position.x + xCounter, transform.position.y, transform.position.z + counter), Quaternion.identity);
                    gridMesh.GetComponent<MeshRenderer>().material = m_objectiveMat;
                    gridMesh.GetComponent<Box>().m_posX = j;
                    gridMesh.GetComponent<Box>().m_posZ = i;
                    Box originBox = gameObject.AddComponent<Box>();
                    originBox.m_posX = j;
                    originBox.m_posZ = i;
                    //m_originBox = originBox;
                    m_gridBoxInstances.Add(originBox);
                }
                else if (m_originX == j && m_originZ == i)
                {
                    GameObject gridMesh = Instantiate(m_gridBox, new Vector3(transform.position.x + xCounter, transform.position.y, transform.position.z + counter), Quaternion.identity);
                    gridMesh.GetComponent<MeshRenderer>().material = m_originMat;
                    gridMesh.GetComponent<Box>().m_posX = j;
                    gridMesh.GetComponent<Box>().m_posZ = i;
                    Box originBox = gameObject.AddComponent<Box>();
                    originBox.m_posX = j;
                    originBox.m_posZ = i;
                    m_originBox = originBox;
                    m_gridBoxInstances.Add(originBox);
                }
                else
                {
                    GameObject gridMesh = Instantiate(m_gridBox, new Vector3(transform.position.x + xCounter, transform.position.y, transform.position.z + counter), Quaternion.identity);
                    gridMesh.GetComponent<MeshRenderer>().material = m_neutralMat;
                    gridMesh.GetComponent<Box>().m_posX = j;
                    gridMesh.GetComponent<Box>().m_posZ = i;
                    Box originBox = gameObject.AddComponent<Box>();
                    originBox.m_posX = j;
                    originBox.m_posZ = i;
                    //m_originBox = originBox;
                    m_gridBoxInstances.Add(originBox);
                }
                xCounter += 1;
            }
            counter += 1;
        }

        // manhattan distance
        ManhattanDist(m_originBox);

        m_originSolid.x = m_originX;
        m_originSolid.y = m_originZ;

        // path searching
        SearchPath();
    }


    float ManhattanDist(Box point)
    {
        return (Mathf.Abs(point.m_posX - m_objectiveX) + Mathf.Abs(point.m_posZ - m_objectiveZ)) ;
    }

    float ManhattanDistVector(Vector2 point)
    {
        return (Mathf.Abs(point.x - m_objectiveX) + Mathf.Abs(point.y - m_objectiveZ)) ;
    }

    //path searching
    void SearchPath()
    {
        m_h = Mathf.Abs(m_objectiveX - m_originX) + Mathf.Abs(m_objectiveZ - m_originZ);
        //IEnumerable<GameObject> m_possibleBoxes;
        {
            foreach (Box box in m_gridBoxInstances)
            {
                if (   box.GetComponent<Box>().m_posX == m_originX -1 
                    || box.GetComponent<Box>().m_posX == m_originX + 1
                    || box.GetComponent<Box>().m_posZ == m_originZ + 1
                    || box.GetComponent<Box>().m_posZ == m_originZ - 1)
                {
                    //yield return box;
                }
            }
            //yield break;
        }
        m_f = m_g + m_h;

    }
    void Search()
    {
        int searchX = 0;
        int searchZ = 0;
        bool foundX = false;
        bool foundZ = false;
        for (int i = 0; i < m_gridBoxInstances.Count; ++i)
        {
            if (searchX == m_originX && !foundX)
            {
                foundX = true;
                Debug.Log("X position: " + searchX);
            }
            if (searchZ == m_originZ && !foundZ)
            {
                foundZ = true;
                Debug.Log("Z position: " + searchZ);
            }
            ++searchX;
            if (searchX > m_X - 1)
            {
                searchX = 0;
                searchZ += 1;
            }
        }
    }
}
