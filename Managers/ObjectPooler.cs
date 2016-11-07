using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler Instance;

	/// <summary>
	/// Singleton
	/// </summary>
	protected virtual void Awake()
	{
		Instance = this;
	}

	/// <summary>
	/// On start, we fill the pool with the specified gameobjects
	/// </summary>
	protected virtual void Start()
	{
		FillObjectPool();
	}

	/// <summary>
	/// Implement this method to fill the pool with objects
	/// </summary>
	protected virtual void FillObjectPool()
	{
		return ;
	}

	/// <summary>
	/// Implement this method to return a gameobject
	/// </summary>
	/// <returns>The pooled game object.</returns>
	public virtual GameObject GetPooledGameObject()
	{
		return null;
	}
}

