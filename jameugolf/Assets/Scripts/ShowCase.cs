using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCase : MonoBehaviour
{
	int _index;
	[SerializeField] GameObject[] list;

	private void Start()
	{
		StartCoroutine(Show());
	}

	private void Update()
	{
		for(int i = 0; i<list.Length; i++)
		{
			if (i == _index)
				list[i].SetActive(true);
			else
				list[i].SetActive(false);
		}
	}

	IEnumerator Show()
	{
		yield return new WaitForSeconds(3);
		_index = _index + 1 < list.Length ? _index + 1 : 0;

		StartCoroutine(Show());
	}
}
