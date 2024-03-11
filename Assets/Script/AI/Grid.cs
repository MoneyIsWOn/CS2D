using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] grid;
    public LayerMask unwalkableMask; //������ �� ���� ������ ��Ÿ���� ���̾��ũ
    public Vector2 gridWorldSize;//������ ��ü ������
    public float nodeRadius; //����� ������
    float nodeDiameter; // ����� ����(���� ��ĭ�� ���� ���̸� �������� ũ��)
    public int gridSizeX, gridSizeY; //������ �غ��� ���� ũ��
    public GameObject Endpoint;
    

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/ nodeDiameter); //(���� ���� �غ� / ����� ����)���� ���ڿ� ��尡 ��� �� �� �ִ��� ���� ���.
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);//(���� ���� ���� / ����� ����)���� ���ڿ� ��尡 ��� �� �� �ִ��� ���� ���.
        CreateGrid();
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY]; //start���� ����� ũ�⸸ŭ ��� 2���� �迭 ����
        Vector3 worldBottomLeft = transform.position - Vector3.right * (gridWorldSize.x / 2) - Vector3.up*(gridWorldSize.y / 2); //���ڿ��� ���� �Ʒ� ������
        Debug.Log($"{worldBottomLeft} is WorldBottemLeft");
        for(int x=0;x<gridSizeX;x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                //������ ���߾� ��ġ���� worldPoint��� �����ϰ� ���⿡ ��带 �ɴ� ����. 
                if (Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask) == false)
                {
                    grid[x, y] = new Node(true, worldPoint);
                    // Debug.Log($"{worldPoint} New Node Create...");
                }
                else
                {
                    grid[x, y] = new Node(false, worldPoint);
                    Debug.Log($"{worldPoint} unwalkableMask Node Create...");
                }
            }
        }
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);

        return grid[x,y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x,gridWorldSize.y,0));
        // ���̾������� ť��׸���(ť���� �߾� ��ġ�� ���� ����, ť���� ������)
        if(grid != null)
        {
            foreach(Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }

}
