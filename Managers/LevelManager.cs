using UnityEngine;
using System.Collections;


	/// <summary>
	/// Spawns the player, and 
	/// </summary>
	public class LevelManager : MonoBehaviour
	{
		/// Singleton
		static public LevelManager Instance { get { return _instance; } }
		static protected LevelManager _instance;
		protected virtual void Awake()
		{
			_instance=this;			
		}

		public enum Controls { SingleButton, LeftRight }

		/// The current speed the level is traveling at
		public float Speed { get; protected set; }	
		/// The distance traveled since the start of the level
		public float DistanceTraveled { get; protected set; }

		/// the prefab you want for your player
		[Header("Prefabs")]
		public GameObject StartingPosition;
		/// the list of playable characters - use this to tell what characters you want in your level, don't access that at runtime

		/// the x distance between each character
		public float DistanceBetweenCharacters = 1f;
		/// the elapsed time since the start of the level
		public float RunningTime { get; protected set; }
		/// the amount of points a player gets per second
		public float PointsPerSecond = 20;
		/// the text that will be shown (if not empty) at the start of the level



		[Space(10)]
		[Header("Level Bounds")]
		/// the line after which objects can be recycled
		public Bounds RecycleBounds;

		[Space(10)]
		[Header("Death Zone")]
		/// the line after which playable characters will die - leave it to zero if you don't want to use it
		public Bounds DeathBounds;

		[Space(10)]
		[Header("Speed")]
		/// the initial speed of the level
		public float InitialSpeed = 10f;
		/// the maximum speed the level will run at
		public float MaximumSpeed = 50f;
		/// the acceleration (per second) at which the level will go from InitialSpeed to MaximumSpeed
		public float SpeedAcceleration=1f;

		[Space(10)]
		[Header("Intro and Outro durations")]
		/// duration of the initial fade in
		public float IntroFadeDuration=1f;
		/// duration of the fade to black at the end of the level
		public float OutroFadeDuration=1f;


		[Space(10)]
		[Header("Start")]
		/// the duration (in seconds) of the initial countdown
		public int StartCountdown;
		/// the text displayed at the end of the countdown
		public string StartText;

		[Space(10)]
		[Header("Mobile Controls")]
		/// the mobile control scheme applied to this level
		public Controls ControlScheme;

		[Space(10)]
		[Header("Life Lost")]
		/// the effect we instantiate when a life is lost
		public GameObject LifeLostExplosion;

		// protected stuff

		protected float _savedPoints;	
		protected float _recycleX;
		protected Bounds _tmpRecycleBounds;

		/// <summary>
		/// Initialization
		/// </summary>
		protected virtual void Start()
		{
			Speed = InitialSpeed;
			DistanceTraveled = 0;

			InstantiateCharacters();

	

			// storage
			_savedPoints =GameManager.Instance.Points;
	
			GameManager.Instance.SetStatus(GameManager.GameStatus.BeforeGameStart);
			GameManager.Instance.SetPointsPerSecond(PointsPerSecond);



			PrepareStart();
		}

		/// <summary>
		/// Handles everything before the actual start of the game.
		/// </summary>
		protected virtual void PrepareStart()
		{		
			//if we're supposed to show a countdown we schedule it, otherwise we just start the level
			if (StartCountdown>0)
			{
				GameManager.Instance.SetStatus(GameManager.GameStatus.BeforeGameStart);
				StartCoroutine(PrepareStartCountdown());	
			}	
			else
			{
				LevelStart();
			}	
		}

		/// <summary>
		/// Handles the initial start countdown display
		/// </summary>
		/// <returns>The start countdown.</returns>
		protected virtual IEnumerator PrepareStartCountdown()
		{
			int countdown = StartCountdown;		


			// while the countdown is active, we display the current value, and wait for a second and show the next
			while (countdown > 0)
			{

				countdown--;
				yield return new WaitForSeconds(1f);
			}

			// when the countdown reaches 0, and if we have a start message, we display it
			if ((countdown==0) && (StartText!=""))
			{
				
				yield return new WaitForSeconds(1f);
			}

			// we turn the countdown inactive, and start the level

			LevelStart();
		}

		/// <summary>
		/// Handles the start of the level : starts the autoincrementation of the score, sets the proper status and triggers the corresponding event.
		/// </summary>
		public virtual void LevelStart()
		{
			GameManager.Instance.SetStatus(GameManager.GameStatus.GameInProgress);
			GameManager.Instance.AutoIncrementScore(true);

		}

		/// <summary>
		/// Instantiates all the playable characters and feeds them to the gameManager
		/// </summary>
		protected virtual void InstantiateCharacters()
		{
	
		}

		/// <summary>
		/// Resets the level : repops dead characters, sets everything up for a new game
		/// </summary>
		public virtual void ResetLevel()
		{
			InstantiateCharacters();
			PrepareStart();
		}

		/// <summary>
		/// Turns buttons on or off depending on the chosen mobile control scheme
		/// </summary>
		

		/// <summary>
		/// Every frame
		/// </summary>
		public virtual void Update()
		{
			_savedPoints = GameManager.Instance.Points;


			// we increment the total distance traveled so far
			DistanceTraveled = DistanceTraveled + Speed * Time.fixedDeltaTime;

			// if we can still accelerate, we apply the level's speed acceleration
			if (Speed<MaximumSpeed)
			{
				Speed += SpeedAcceleration * Time.deltaTime;
			}

			RunningTime+=Time.deltaTime;
		}

		/// <summary>
		/// Sets the speed.
		/// </summary>
		/// <param name="newSpeed">New speed.</param>
		public virtual void SetSpeed(float newSpeed)
		{
			Speed = newSpeed;
		}

		/// <summary>
		/// Adds speed to the current level speed
		/// </summary>
		/// <param name="speedAdded">Speed added.</param>
		public virtual void AddSpeed(float speedAdded)
		{
			Speed += speedAdded;
		}

		/// <summary>
		/// Temporarily multiplies the level speed by the provided factor
		/// </summary>
		/// <param name="factor">The number of times you want to increase/decrease the speed by.</param>
		/// <param name="duration">The duration of the speed change, in seconds.</param>
		public virtual void TemporarilyMultiplySpeed(float factor, float duration)
		{
			StartCoroutine(TemporarilyMultiplySpeedCoroutine(factor, duration));
		}

		/// <summary>
		/// Coroutine called by the method above to avoid issues when what initially called the method gets disabled (a pickable object for example)
		/// </summary>
		/// <returns>The multiply speed coroutine.</returns>
		/// <param name="factor">The number of times you want to increase/decrease the speed by.</param>
		/// <param name="duration">The duration of the speed change, in seconds.</param>
		protected virtual IEnumerator TemporarilyMultiplySpeedCoroutine(float factor, float duration)
		{
			float saveSpeed=Speed;
			Speed = Speed * factor;
			yield return new WaitForSeconds(1f);
			Speed = saveSpeed;
		}

		/// <summary>
		/// Determines if the object whose bounds are passed as a parameter has to be recycled or not.
		/// </summary>
		/// <returns><c>true</c>, if the object has to be recycled, <c>false</c> otherwise.</returns>
		/// <param name="objectBounds">Object bounds.</param>
		/// <param name="destroyDistance">The x distance after which the object will get destroyed.</param>
		public virtual bool CheckRecycleCondition(Bounds objectBounds,float destroyDistance)
		{
			_tmpRecycleBounds = RecycleBounds;
			_tmpRecycleBounds.extents+=Vector3.one * destroyDistance;

			if (objectBounds.Intersects(_tmpRecycleBounds)) 
			{
				return false;
			} 
			else 
			{
				return true;
			}
		}

		public virtual bool CheckDeathCondition(Bounds objectBounds)
		{
			if (objectBounds.Intersects(DeathBounds)) 
			{
				return false;
			} 
			else 
			{
				return true;
			}
		}


		/// <summary>
		/// Gets the player to the specified level
		/// </summary>
		/// <param name="levelName">Level name.</param>
		public virtual void GotoLevel(string levelName)
		{		
			
			StartCoroutine(GotoLevelCo(levelName));
		}

		/// <summary>
		/// Waits for a short time and then loads the specified level
		/// </summary>
		/// <returns>The level co.</returns>
		/// <param name="levelName">Level name.</param>
		protected virtual IEnumerator GotoLevelCo(string levelName)
		{
			if (Time.timeScale > 0.0f)
			{
				yield return new WaitForSeconds(OutroFadeDuration);
			}
			GameManager.Instance.UnPause();

		

		}

		/// <summary>
		/// Triggered when all lives are lost and you press the main action button
		/// </summary>
		public virtual void GameOverAction()
		{
			GameManager.Instance.UnPause();
			
		}

		/// <summary>
		/// Triggered when a life is lost and you press the main action button
		/// </summary>
		public virtual void LifeLostAction()
		{   
			ResetLevel();
		}

		/// <summary>
		/// Kills the player.
		/// </summary>
	
		/// <summary>
		/// Coroutine that kills the player, stops the camera, resets the points.
		/// </summary>
		/// <returns>The player co.</returns>

		/// <summary>
		/// What happens when all characters are dead (or when the character is dead if you only have one)
		/// </summary>
		protected virtual void AllCharactersAreDead()
		{
			// if we've specified an effect for when a life is lost, we instantiate it at the camera's position
			if (LifeLostExplosion != null)
			{
				GameObject explosion = Instantiate(LifeLostExplosion);
				explosion.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,0) ;
			}

			
		}

		/// <summary>
		/// Override this if needed
		/// </summary>
		protected virtual void OnEnable()
		{

		}

		/// <summary>
		/// Override this if needed
		/// </summary>
		protected virtual void OnDisable()
		{

		}
	}

