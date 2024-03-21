using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class LightsOutGameView: MonoBehaviour, ILightsOutGameView
{
    public event Action<int> ItemClicked;

    [SerializeField] LightItemView _lightPrefab;
    [SerializeField] GridLayoutGroup _gridLayoutItemsContainer;

    private LightItemView[] _lights;

    void OnDestroy()
    {
        ResetView();
    }

    public void Initialize(int rows, int columns)
    {
        ResetView();

        _lights = new LightItemView[columns * rows];

        for (var i = 0; i < columns * rows; i++)
        {
            var lightObj = Instantiate(_lightPrefab, _gridLayoutItemsContainer.transform);
            lightObj.Initialize(i);
            lightObj.OnClick += OnItemClicked;

            _lights[i] = lightObj;
        }

        StartCoroutine(RefreshGridLayout(rows, columns));
    }

    public void SetLightState(int index, bool turnedOn)
    {
        _lights[index].SetState(turnedOn);
    }

    private void OnItemClicked(int index)
    {
        ItemClicked?.Invoke(index);
    }

    private void ResetView()
    {
        if (_lights != null)
        {
            foreach (var lightObj in _lights)
            {
                lightObj.OnClick -= OnItemClicked;
                lightObj.gameObject.SetActive(false);

                Destroy(lightObj.gameObject);
            }
            _lights = null;
        }
    }

    private IEnumerator RefreshGridLayout(int rows, int columns)
    {
        // Adjust grid to fit required count of items
        AdjustGrid(rows, columns);

        // Enable layout
        _gridLayoutItemsContainer.enabled = true;

        // Let grid layout to position items
        yield return new WaitForEndOfFrame();

        // Disable layout to avoid unintentional UI redrawals
        _gridLayoutItemsContainer.enabled = false;
    }

    private void AdjustGrid(int rows, int columns)
    {
        _gridLayoutItemsContainer.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutItemsContainer.constraintCount = columns;

        var rectTransform = _gridLayoutItemsContainer.gameObject.GetComponent<RectTransform>();

        var targetItemWidth = (rectTransform.rect.width - _gridLayoutItemsContainer.spacing.x * columns) / columns;
        var targetItemHeight = (rectTransform.rect.height - _gridLayoutItemsContainer.spacing.y * rows) / rows;

        var side = Mathf.Min(targetItemWidth, targetItemHeight);

        _gridLayoutItemsContainer.cellSize = new Vector2(side, side);
    }
}
