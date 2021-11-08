using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
	[SerializeField] private int _baseCapacity = 16;
	[SerializeField] private int _additionCapacity = 16;
	[SerializeField] private GameObject _template;
	private bool _isInit;
	private List<PullElement> _pool;

	private void OnValidate ()
    {
		if (_template != null && _template.GetComponent<T>() == null)
        {
			_template = null;
			Debug.LogError("Template must contain "+typeof(T)+" component");
		}
		if (_baseCapacity < 1)
        {
			_baseCapacity = 1;
			Debug.LogError("Base Capacity cannot be less 1");
		}
		if (_additionCapacity < 1)
		{
			_additionCapacity = 1;
			Debug.LogError("Addition Capacity cannot be less 1");
		}
	}

	public void Initialize ()
	{
		if (_isInit)
			return;
		_pool = new List<PullElement>();
		SpawnElements(_baseCapacity);
		_isInit = true;
	}

	public T GetElement ()
	{
		var pElement = _pool.FirstOrDefault(e => e.Used == false);
		if (pElement != null)
        {
			pElement.Used = true;
			pElement.Object.gameObject.SetActive(true);
			return pElement.Object;
		} 
		else
        {
			SpawnElements(_additionCapacity);
			return GetElement();
		}
	}

	public void ReturnToPull (T element)
    {
		var pElement = _pool.FirstOrDefault(e => e.Object == element);
		if (pElement != null)
		{
			pElement.Used = false;
			element.gameObject.SetActive(false);
			CheckSize();
		}
		else
        {
			Debug.LogWarning("Object is not element of pull");
        }
	}

	private void SpawnElements (int count)
    {
		Transform parent = transform;
		for (int i = 0; i < count; i++)
		{
			GameObject element = Instantiate(_template, parent);
			element.SetActive(false);
			_pool.Add(new PullElement(element.GetComponent<T>()));
		}
	}

	private void CheckSize ()
    {
		if (_pool.Count > _baseCapacity)
        {
			var freePool = _pool.FindAll(e => e.Used == false).ToArray();
			if (freePool.Length > _additionCapacity)
            {
				for (int i = 0; i < _additionCapacity; i++)
                {
					_pool.Remove(freePool[i]);
					Destroy(freePool[i].Object.gameObject);
                }
				_pool.Capacity = _pool.Count;
			}
        }
    }

	private class PullElement
    {
		public T Object;
		public bool Used;

        public PullElement (T @object)
        {
            Object = @object;
            Used = false;
        }
    }
}