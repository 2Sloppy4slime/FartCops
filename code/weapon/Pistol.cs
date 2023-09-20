using Sandbox;
using System.Linq;

namespace MyGame;

public partial class Pistol : Weapon
{
	public override string ModelPath => "weapons/rust_pistol/rust_pistol.vmdl";
	public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";

	public Weapon FartShoot;

	static IEntity FirstPersonViewer { get; set; }

	public Score sc = new Score();

	[ClientRpc]
	protected virtual void ShootEffects()
	{
		Game.AssertClient();

		Pawn.SetAnimParameter( "b_attack", true );
		ViewModelEntity?.SetAnimParameter( "fire", true );
	}

	public override void PrimaryAttack()
	{
		ShootEffects();
		FartShoot();
	}

	protected override void Animate()
	{
		Pawn.SetAnimParameter( "holdtype", (int)CitizenAnimationHelper.HoldTypes.Pistol );
	}


}
