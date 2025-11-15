# Drone vs Enemy Combat Simulation (Prototype)

**Author:** Ayesha Mahaboob  
**Controls:** WASD (move), Q/E (descend/ascend), Spacebar (fire)

**Features Implemented:**
- Player-controlled drone with physics-based movement
- Missile prefab with explosion on impact
- NavMesh enemy patrol with detection and shooting
- Destroyable dummy targets (tag: Target)
- Camera follow
- Particle and sound effects

**Known limitations:**
- Simple physics; no networking
- Basic AI; uses OverlapSphere detection
- No object pooling (may spawn many projectiles)
