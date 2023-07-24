using Sandbox;
using System;

namespace MyGame;
public partial class Score {
	public Pawn Pawn;
	public Weapon PrimaryRate;
	
	public void KillGet(int amount) { Kills += amount; Killstreak += amount; }
	public void GotKilled(int amount) { Deaths += amount; }
	
	public int Deaths; 
	public int Kills;
	public int Killstreak;

}
