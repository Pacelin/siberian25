using UnityEngine;

namespace Siberian25.Game.World
{
    public class GridLayoutFactory : MonoBehaviour
    {
        [SerializeField] private float _spacing;
        [SerializeField] private int _columnsCount;
        [SerializeField] private int _rowsCount;

        [ContextMenu("Create Layout")]
        private void Create()
        {
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);

            var width = _spacing * (_columnsCount - 1);
            var height = _spacing * (_rowsCount - 1);
            var x = -width / 2;
            var y = -height / 2;
            
            for (int j = 0; j < _rowsCount; j++)
            {
                for (int i = 0; i < _columnsCount; i++)
                {
                    var go = new GameObject($"Point ({j}, {i})");
                    go.transform.SetParent(transform);
                    go.transform.localPosition = new Vector3(x + _spacing * i, y + _spacing * j, 0);
                }
            }
        }
    }
}