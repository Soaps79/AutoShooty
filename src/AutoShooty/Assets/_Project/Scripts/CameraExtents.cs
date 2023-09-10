using UnityEngine;
using QGame;
using System.Runtime.InteropServices.WindowsRuntime;

public class CameraExtents : QScript
{
    [SerializeField]
    private Transform _topLeft;
    [SerializeField] 
    private Transform _topRight;
    [SerializeField]
    private Transform _bottomLeft;
    [SerializeField] 
    private Transform _bottomRight;

    public Vector3 TopLeft => _topLeft.position;
    public Vector3 TopRight => _topRight.position;
    public Vector3 BottomLeft => _bottomLeft.position;
    public Vector3 BottomRight => _bottomRight.position;


    private void Awake()
    {
        
    }
}
