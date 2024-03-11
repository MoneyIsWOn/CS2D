using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Grid grid;
    public Transform seeker, target;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    private void Update()
    {
        // FindPath(seeker.position, target.position);
        Node startNode = grid.NodeFromWorldPoint(seeker.position);

    }


    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos); //���� ��� 
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        List<Node> openSet = new List<Node>(); //�湮�ؾ��� ����
        HashSet<Node> closeSet = new HashSet<Node>(); //�湮�� ����
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                    {
                        node = openSet[i];
                    }
                }
            }
            openSet.Remove(node);
            closeSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return; //�������� ������ ������ ���� �� �˰��� ����

            }
            foreach (Node neighber in grid.GetNeighbors(node)) //�̿� ��带 ���� Ȯ���ϸ鼭
            {
                if (!neighber.walkable || closeSet.Contains(neighber)) //���� �̿� ��尡 �̵��� �� ���ų�, �̹� �湮�� ����� ����
                {
                    continue;
                }

                int newCostToNeighbor = node.gCost + GetDistance(node, neighber);
                if (newCostToNeighbor < neighber.gCost || !openSet.Contains(neighber))
                {
                    neighber.gCost = newCostToNeighbor;
                    neighber.hCost = GetDistance(neighber, targetNode);
                    neighber.parent = node;
                }
            }
        }
    }
    int GetDistance(Node A, Node B) //���� ��尣�� �Ÿ� ���ϱ�.
    {
        int dstX = Mathf.Abs(A.gridX - B.gridX);
        int dstY = Mathf.Abs(A.gridY - B.gridY);
        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        grid.path = path;
    }
}
