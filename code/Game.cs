
using Sandbox;
using System;
using System.Linq;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace MyGame;

/// <summary>
/// This is your game class. This is an entity that is created serverside when
/// the game starts, and is replicated to the client. 
/// 
/// You can use this to create things like HUDs and declare which player class
/// to use for spawned players.
/// </summary>
public partial class MyGame : Sandbox.GameManager
{
	
	[ConCmd.Admin("ent_create")]
	public static void SpawnEntity(string entName)
	{
		Log.Info("creating " + entName);
		var owner = ConsoleSystem.Caller.Pawn as Pawn;

		if (owner == null)
		{
			Log.Info("Failed to create " + entName);
			return;
		}

		var entityType = TypeLibrary.GetType<Entity>(entName)?.TargetType;
		if (entityType == null)
		{
			Log.Info("Failed to create " + entName);
			return;
		}

		var tr = Trace.Ray(owner.AimRay, 500)
			.UseHitboxes()
			.Ignore(owner)
			.Size(2)
			.Run();

		var ent = TypeLibrary.Create<Entity>(entityType);

		ent.Position = tr.EndPosition;
		ent.Rotation = Rotation.From(new Angles(0, owner.AimRay.Forward.EulerAngles.yaw, 0));

		//Log.Info( $"ent: {ent}" );
	}
	/// <summary>
	/// Called when the game is created (on both the server and client)
	/// </summary>
	public MyGame()
	{
		
		if ( Game.IsClient )
		{
			Game.RootPanel = new Hud();
		}
	}

	/// <summary>
	/// A client has joined the server. Make them a pawn to play with
	/// </summary>
	public override void ClientJoined( IClient client )
	{
		base.ClientJoined( client );

		// Create a pawn for this client to play with
		var pawn = new Pawn();
		client.Pawn = pawn;
		pawn.Respawn();
		pawn.DressFromClient( client );

		// Get all of the spawnpoints
		var spawnpoints = Entity.All.OfType<SpawnPoint>();

		// chose a random one
		var randomSpawnPoint = spawnpoints.OrderBy( x => Guid.NewGuid() ).FirstOrDefault();

		// if it exists, place the pawn there
		if ( randomSpawnPoint != null )
		{
			var tx = randomSpawnPoint.Transform;
			tx.Position = tx.Position + Vector3.Up * 50.0f; // raise it up
			pawn.Transform = tx;
		}
	}
}

