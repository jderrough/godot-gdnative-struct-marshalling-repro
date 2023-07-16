using System;

using Godot;
using Godot.Collections;

public class CSharpBulletSpawner : Node2D
{
	[Export]
	public Resource BulletKit;

	private Godot.Object _bullets;

	public override void _Ready()
	{
		base._Ready();


		_bullets = GetNode<Godot.Object>("/root/Bullets");
	}

	public override void _Process(float delta)
	{
		base._Process(delta);

		if (Input.IsKeyPressed((int)KeyList.Enter))
			Obtain();
		if (Input.IsKeyPressed((int)KeyList.Space))
			Spawn();
		if (Input.IsKeyPressed((int)KeyList.Right))
			Rotation += Mathf.Pi * delta;
		if (Input.IsKeyPressed((int)KeyList.Left))
			Rotation -= Mathf.Pi * delta;
	}

	void Spawn()
	{
		var bulletVelocity = new Vector2(Mathf.Cos(GlobalRotation), Mathf.Sin(GlobalRotation)) * 30.0f;

		// Define which properties will be set to the newly spawned bullet.
		// The bullet will be spawned in the same position as this node,
		// travelling in the direction defined by its rotation.
		var properties = new Dictionary();
		properties.Add("transform", new Transform2D(GlobalRotation, GlobalPosition));
		properties.Add("velocity", bulletVelocity);

		// Spawn a bullet using the selected BulletKit and setting the properties defined above.
		// Bullets is an autoload.
		_bullets.Call("spawn_bullet", BulletKit, properties);
	}

	void Obtain()
	{
		var bulletVelocity = new Vector2(Mathf.Cos(GlobalRotation), Mathf.Sin(GlobalRotation)) * 30.0f;

		var bulletId = _bullets.Call("obtain_bullet", BulletKit) as Int32[];

		GD.Print("C#: ", bulletId[0], ",", bulletId[1], ",", bulletId[2]);

		_bullets.Call("set_bullet_property", bulletId, "transform", new Transform2D(GlobalRotation, GlobalPosition));
		_bullets.Call("set_bullet_property", bulletId, "velocity", bulletVelocity);
	}
}
