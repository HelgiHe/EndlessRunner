using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Spawns and positions/resizes objects based on the distance traveled 
/// </summary>
public class DistanceSpawner : Spawner 
{
	[Header("Gap between objects")]
	/// the minimum gap bewteen two spawned objects
	public Vector3 MinimumGap = new Vector3(1,1,1);
	/// the maximum gap between two spawned objects
	public Vector3 MaximumGap = new Vector3(1,1,1);
	[Space(10)]	
	[Header("Y Clamp")]
	/// the minimum Y position we can spawn the object at
	public float MinimumYClamp;
	/// the maximum Y position we can spawn the object at
	public float MaximumYClamp;
	[Header("Z Clamp")]
	/// the minimum Z position we can spawn the object at
	public float MinimumZClamp;
	/// the maximum Z position we can spawn the object at
	public float MaximumZClamp;
	[Space(10)]
	[Header("Spawn angle")]
	/// if true, the spawned objects will be rotated towards the spawn direction
	public bool SpawnRotatedToDirection=true;

	protected Transform _lastSpawnedTransform;
	protected float _nextSpawnDistance;

	/// <summary>
	/// Triggered at the start of the level, initialization
	/// </summary>
	protected virtual void Start () 
	{
		/// we get the object pooler component
		_objectPooler = GetComponent<ObjectPooler> ();	
	}

	/// <summary>
	/// Triggered every frame
	/// </summary>
	protected virtual void Update () 
	{
		CheckSpawn();
	}

	/// <summary>
	/// Checks if the conditions for a new spawn are met, and if so, triggers the spawn of a new object
	/// </summary>
	protected virtual void CheckSpawn()
	{
		// if we've set our distance spawner to only spawn when the game's in progress :
		if (OnlySpawnWhileGameInProgress)
		{
			if (GameManager.Instance.Status != GameManager.GameStatus.GameInProgress)
			{
				_lastSpawnedTransform = null;
				return ;
			}
		}

		// if we haven't spawned anything yet, or if the last spawned transform is inactive, we reset to first spawn.
		if ((_lastSpawnedTransform== null) || (!_lastSpawnedTransform.gameObject.activeInHierarchy))
		{
			//DistanceSpawn(transform.position + MMMaths.RandomVector3(MinimumGap,MaximumGap));	
			return;
		}

		// if the last spawned object is far ahead enough, we spawn a new object
		if (transform.InverseTransformPoint(_lastSpawnedTransform.position).x < -_nextSpawnDistance )
		{
			Vector3 spawnPosition = transform.position;		
			DistanceSpawn(spawnPosition);	
		}
	}

	/// <summary>
	/// Spawns an object at the specified position and determines the next spawn position
	/// </summary>
	/// <param name="spawnPosition">Spawn position.</param>
	protected virtual void DistanceSpawn(Vector3 spawnPosition)
	{
		// we spawn a gameobject at the location we've determined previously
		GameObject spawnedObject = Spawn(spawnPosition,false);

		// if the spawned object is null, we're gonna start again with a fresh spawn next time we get fresh objects.
		if (spawnedObject==null)
		{
			_lastSpawnedTransform=null;
			_nextSpawnDistance = UnityEngine.Random.Range(MinimumGap.x, MaximumGap.x) ;
			return;
		}

		// we need to have a poolableObject component for the distance spawner to work.
		if (spawnedObject.GetComponent<PoolableObject>()==null)
		{
			throw new Exception(gameObject.name+" is trying to spawn objects that don't have a PoolableObject component.");					
		}

		// if we have a movingObject component, we rotate it towards movement if needed
		if (SpawnRotatedToDirection)
		{
			spawnedObject.transform.rotation *= transform.rotation;
		}
		// if this is a moving object, we tell it to move in the designated direction
		if (spawnedObject.GetComponent<MovingObject>()!=null)
		{
			spawnedObject.GetComponent<MovingObject>().Direction=transform.rotation*Vector3.left;
		}

		// if we've already spawned at least one object, we'll reposition our new object according to that previous one
		if (_lastSpawnedTransform!=null)
		{
			// we center our object on the spawner's position
			spawnedObject.transform.position = transform.position;

			// we determine the relative x distance between our spawner and the object.
			float xDistanceToLastSpawnedObject = transform.InverseTransformPoint(_lastSpawnedTransform.position).x;

			// we position the new object so that it's side by side with the previous one,
			// taking into account the width of the new object and the last one.
			spawnedObject.transform.position += transform.rotation
				* Vector3.right
				* (xDistanceToLastSpawnedObject 
					+ _lastSpawnedTransform.GetComponent<PoolableObject>().Size.x/2 
					+ spawnedObject.GetComponent<PoolableObject>().Size.x/2) ;

			// we apply a clamped gap to our object, based on what's been defined in the inspector
			//spawnedObject.transform.position += (transform.rotation * ClampedPosition(MMMaths.RandomVector3(MinimumGap,MaximumGap)/2));

			// if what we spawn is a moving object (it should usually be), we tell it to move to account for initial movement gap
			if (spawnedObject.GetComponent<MovingObject>()!=null)
			{
				spawnedObject.GetComponent<MovingObject>().Move();
			}
		}

		//we tell our object it's now completely spawned
		spawnedObject.GetComponent<PoolableObject>().TriggerOnSpawnComplete();

		// we determine after what distance we should try spawning our next object
		_nextSpawnDistance = spawnedObject.GetComponent<PoolableObject>().Size.x/2 ;
		// we store our new object, which will now be the previously spawned object for our next spawn
		_lastSpawnedTransform = spawnedObject.transform;

	}

	/// <summary>
	/// Returns a Vector3 clamped on the Y and Z axis based on the inspector settings
	/// </summary>
	/// <returns>The new position.</returns>
	/// <param name="vectorToClamp">Vector to clamp.</param>
	protected virtual Vector3 ClampedPosition(Vector3 vectorToClamp)
	{
		vectorToClamp.y = Mathf.Clamp (vectorToClamp.y, MinimumYClamp, MaximumYClamp);
		vectorToClamp.z = Mathf.Clamp (vectorToClamp.z, MinimumZClamp, MaximumZClamp);
		return vectorToClamp;
	}
}

