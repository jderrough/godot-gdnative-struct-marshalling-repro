extends Node2D

# Assign a valid BulletKit via the inspector.
export(Resource) var bullet_kit


func _process(delta):
	if Input.is_key_pressed(KEY_ENTER):
		obtain()
	if Input.is_key_pressed(KEY_SPACE):
		spawn()
	if Input.is_key_pressed(KEY_RIGHT):
		rotation += PI * delta
	if Input.is_key_pressed(KEY_LEFT):
		rotation -= PI * delta


func spawn():
	var bullet_velocity = Vector2(cos(global_rotation), sin(global_rotation)) * 30.0

	# Define which properties will be set to the newly spawned bullet.
	# The bullet will be spawned in the same position as this node,
	# travelling in the direction defined by its rotation.
	var properties = {
		"transform": Transform2D(global_rotation, global_position),
		"velocity": bullet_velocity
	}
	# Spawn a bullet using the selected BulletKit and setting the properties defined above.
	# Bullets is an autoload.
	Bullets.spawn_bullet(bullet_kit, properties)

func obtain():
	var bullet_velocity = Vector2(cos(global_rotation), sin(global_rotation)) * 30.0

	var bullet_id = Bullets.obtain_bullet(bullet_kit)

	print("GDScript: ", bullet_id)

	Bullets.set_bullet_property(bullet_id, "transform", Transform2D(global_rotation, global_position))
	Bullets.set_bullet_property(bullet_id, "velocity", bullet_velocity)
