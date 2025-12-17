## 3. FSM (State Machine) Rules

### StateMachine/
- Contains **framework only**
- No actor-specific logic

### Where state implementations live
- State implementations must live under:
  - `Scripts/Content/<ActorDomain>/States/`
- `<ActorDomain>` is the owning gameplay domain, e.g.:
  - `Character`, `Boss`, `Monster`, `NPC`

### Behavioral rules
- Transitions are controlled by the owner (actor root / controller)
- States should be deterministic and side-effect limited
- States must not:
  - Poll input directly
  - Manipulate UI
  - Change scenes
