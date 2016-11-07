using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct Transitionable
{
	public Transform trans           { get; private set; } // The transform of the transitionable
	public Collider  collider        { get; private set; } // The collider of the transitionable
	public Float3    oPos            { get; private set; } // The start position of the transitionable
	public Float3    ePos            { get;         set; } // The end   position of the transitionable
	public Float3    oScale          { get;         set; } // The start scale    of the transitionable
	public Float3    eScale          { get;         set; } // The end   scale    of the transitionable
	public bool      enabledCollider { get;         set; } // The state of the collider 


	// Get/Sets the position of the transform
	public Vector3 position 
	{
		get { return trans.position ; }
		set { trans.position = value; } 
	}

	// Get/Sets the scale of the transform
	public Vector3 scale
	{
		get { return trans.localScale ; } 
		set { trans.localScale = value; }
	}

	// Constrcuter
	public Transitionable( Transform _core )
	{
		trans    = _core                         ; // Store the Transitionable's transform
		collider = _core.GetComponent<Collider>(); // Store the collider for culling

		enabledCollider = collider.enabled;

		oPos   = new Float3( _core.position.x  , _core.position.y  , _core.position.z   ); // Assign the starting position of the Transitionable
		oScale = new Float3( _core.localScale.x, _core.localScale.y, _core.localScale.z ); // Assign the starting scale of the Transitionable
	}
}


// We use a custom struct instead of unity 3D's vector class
// As it contains methods which we will never need for this
// it's pretty self explanitory ( it's a struct with 3 floats ).

public struct Float3 
{
	public float x { get; set; }
	public float y { get; set; }
	public float z { get; set; }

	public Float3(float _x, float _y, float _z)
	{
		x = _x;
		y = _y;
		z = _z;
	}
}


public class ObjectTransitioner : MonoBehaviour {

	[SerializeField] private Transform  _relationalObject   ; // The object which everything moves in relation to
	[SerializeField] private string[]   _transitionableTags ; // The tags of which the objects are labled to gather them specific object/s
	[SerializeField] private float      _minCheckDist = 3F  ; // The min distance that is checked
	[SerializeField] private float      _midCheckDist = 15F ; // The mid distance that is checked
	[SerializeField] private float      _maxCheckDist = 40F ; // The max distance that is checked 
	[SerializeField] private float      _mSpeed       = 10F ; // The speed of the transition
	[SerializeField] private float      _minRandPos   = -40F; // The min random position of the transitionables
	[SerializeField] private float      _maxRandPos   =  40F; // The max random position of the transitionables
	[SerializeField] private float      _minRandScale = 1.0F; // The min random scale    of the transitionables
	[SerializeField] private float      _maxRandScale = 0.2F; // The max random scale    of the transitionables

	private Transitionable[] _toMove;                         // All of the transitionables ( change to list to edit in runtime )

	// Called when loaded
	private void Awake()
	{
		FindAllTransitioners(); // Find all the transitionabls
	}

	// We could enbed this into a custom editor for efficiency ( which would be a better option for a propper game ),
	// however to make this example easier to follow through we simply do it when the component is started. Basically,
	// This method is a complete performance mess, so apart from testing just dont use it.
	private void FindAllTransitioners()
	{
		List<Transitionable> _foundObjects = new List<Transitionable>(); // All the objects that are found

		for( int _i = 0; _i < _transitionableTags.Length; _i++ ) // Itterate through the tags
		{
			GameObject[] _cFound = GameObject.FindGameObjectsWithTag( _transitionableTags[_i] ); // Find the current objects which have the itterated tag

			for( int _iFound = 0; _iFound < _cFound.Length; _iFound++ ) // Loop through all the found objects
			{
				Transitionable _t = new Transitionable( _cFound[_iFound].transform ); // Create a new Transitionable with the found transform

				// Create a random end position for the object
				_t.ePos = new Float3( Random.Range(_minRandPos + _t.position.x, _maxRandPos + _t.position.x ),
					Random.Range(_minRandPos + _t.position.y, _maxRandPos + _t.position.y ),
					Random.Range(_minRandPos + _t.position.z, _maxRandPos + _t.position.z ) );

				// Get a random size and assign the end scale to the random size
				float _rSize = Random.Range( _minRandScale, _maxRandScale );
				_t.eScale = new Float3( _rSize, _rSize, _rSize );

				_foundObjects.Add(_t); // Add the object to the found objects list
			}
		}

		_toMove = _foundObjects.ToArray(); // Transform the found objects list into an array
	}

	private void FixedUpdate() // Update at fixed interval
	{
		UpdateTransitioners(); // Update the transitioners
	}

	// Identical to Vector.Distance apart from it inputs a Float3 instead of a Vector3
	private float Distance( Float3 _a, Vector3 _b )
	{
		Float3 _delta = new Float3(_a.x - _b.x, _a.y - _b.y, _a.z - _b.z); // Calculate the delta value

		return Mathf.Sqrt( _delta.x * _delta.x + _delta.y * _delta.y + _delta.z * _delta.z ); // Calculate the distance
	}

	private Vector3 Lerp( Vector3 _from, Float3 _to, float _t )
	{
		_t = Mathf.Clamp01(_t); // Clamp the time interval

		return new Vector3( _from.x + ( _to.x - _from.x ) * _t, _from.y + ( _to.y - _from.y ) * _t, _from.z + ( _to.z - _from.z ) * _t ); // Calculate the lerptation value
	}


	// Updates the transitioners position and size relative to the distance
	private void UpdateTransitioners()
	{
		for( int _i = 0; _i < _toMove.Length; _i++ ) // Itterate through the transitionables
		{
			float _d = Distance( _toMove[ _i ].oPos, _relationalObject.position ); // Calculate the distance between the relational object and current transitionable

			if( _d < _minCheckDist ) // If the distance is less than the min distance checked
				OnMinDist(_i, _d );  // Call respective method
			else
				if( _d < _midCheckDist ) // If the distance is less than the mid distance checked
					OnMidDist(_i);       // Call respective method
				else
					if( _d < _maxCheckDist ) // If the distance is less than the max distance checked
						OnMaxDist(_i);       // Call respective method
					else  
						OnOutOfBounds(_i); // Otherwise we are out of bounds
		}
	}

	private void OnMinDist( int _i, float _d)
	{
		// If the collider is disabled then enable it 
		if(!_toMove [_i].enabledCollider)
		{
			_toMove [_i].collider.enabled = false;
			_toMove [_i].enabledCollider  = false;
		}

		// If distance is less than 3
		if( _d < 3.0F )
		{ 
			// If the distance is less than two
			if( _d < 2.0F )
			{
				// Then just set the size and sclae instead of lerping as it is too close
				_toMove[_i].position = new Vector3( _toMove[_i].oPos.x  , _toMove[_i].oPos.y  , _toMove[_i].oPos.z   );
				_toMove[_i].scale    = new Vector3( _toMove[_i].oScale.x, _toMove[_i].oScale.y, _toMove[_i].oScale.z );
			}
			else
			{
				// Lerp at a faster pace compared to normal
				_toMove[_i].position = Lerp( _toMove[_i].position, _toMove[_i].oPos, Time.fixedDeltaTime   * ( _mSpeed * 3 ) );
				_toMove[_i].scale    = Lerp( _toMove[_i].scale   , _toMove[_i].oScale, Time.fixedDeltaTime * ( _mSpeed     ) );
			}
		}
		else
		{
			// Lerp at a faster pace as we aer closer
			_toMove[_i].position = Lerp( _toMove[_i].position, _toMove[_i].oPos  , Time.fixedDeltaTime *   _mSpeed       );
			_toMove[_i].scale    = Lerp( _toMove[_i].scale   , _toMove[_i].oScale, Time.fixedDeltaTime * ( _mSpeed / 2)  );
		}
	}

	private void OnMidDist( int _i )
	{
		//Disable collision
		DisableCollision(_i);

		//Lerp the scale and position to there original positions
		_toMove[_i].position = Lerp( _toMove[_i].position, _toMove[_i].oPos  , Time.fixedDeltaTime *   _mSpeed      );
		_toMove[_i].scale    = Lerp( _toMove[_i].scale   , _toMove[_i].oScale, Time.fixedDeltaTime * ( _mSpeed / 2) );
	}

	private void OnMaxDist( int _i )
	{
		// Disable collision
		DisableCollision(_i);

		// Lerp the scale and position to there end positions
		_toMove[_i].position = Lerp( _toMove[_i].position, _toMove[_i].ePos  , Time.fixedDeltaTime *   _mSpeed      );
		_toMove[_i].scale    = Lerp( _toMove[_i].scale   , _toMove[_i].eScale, Time.fixedDeltaTime * ( _mSpeed / 2) );
	}

	private void OnOutOfBounds(int _i)
	{
		// Here we could also stop the rendering of the object, but for this example we'll just disable collision
		DisableCollision(_i);
	}

	// Called instead of copying and pasting code, but all it does is disable collision if it's enabled
	private void DisableCollision(int _i)
	{
		if(_toMove [_i].enabledCollider)
		{
			_toMove [_i].collider.enabled = false;
			_toMove [_i].enabledCollider  = false;
		}
	}
}

