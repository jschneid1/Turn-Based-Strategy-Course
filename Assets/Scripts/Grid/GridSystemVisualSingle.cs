using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer _gridVisualMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(Material material)
    {
        _gridVisualMeshRenderer.enabled = true;
        _gridVisualMeshRenderer.material = material;    
    }

    public void Hide()
    {
        _gridVisualMeshRenderer.enabled = false;
    }
}
