using UnityEngine;

namespace Balls
{
    [RequireComponent(typeof(MeshRenderer))]
    public class RegularBallSkin : MonoBehaviour, IBallSkin
    {
        [SerializeField] private string _colorName = "_Color";
        private MeshRenderer _renderer;

        private void Awake ()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public IBallSkin Create (Transform parent)
        {
            return Instantiate(gameObject, parent).GetComponent<RegularBallSkin>();
        }

        public void Destroy ()
        {
            Destroy(gameObject);
        }

        public void SetColor (Color color)
        {
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            block.SetColor(_colorName, color);
            _renderer.SetPropertyBlock(block);
        }
    }
}