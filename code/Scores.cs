using Sandbox;
using System;

namespace MyGame;
public partial class Score {
	public Pawn Pawn;
	public void KillGet(int amount) { Kills += amount; }
	public void GotKilled(int amount) { Deaths += amount; }
	public int Deaths; 
	public int Kills;

}
