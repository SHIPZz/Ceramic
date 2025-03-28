using UnityEngine;

namespace CodeBase.Gameplay
{
    public class CubeVisualizationView : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
     
        public void SetColor(Color color) => _renderer.material.color = color;
    }
}