# Project Structure ? BearValley_AI

## Core Rule
Folder location defines responsibility.
Do NOT place code outside its intended layer.

---

## Scripts/

### Content/
Game domain logic only.

- Character/
  - Character-specific logic
  - States must live under Character/States

- Controller/
  - Runtime controllers coordinating systems
  - No UI logic

- Data/
  - ScriptableObjects and pure data definitions
  - No runtime behavior

- Input/
  - Input abstraction layer
  - No gameplay logic

- Manager/
  - High-level lifecycle or system managers
  - Minimal logic, mostly orchestration

- Module/
  - Pluggable gameplay modules
  - Must be reusable and isolated

---

### StateMachine/
Reusable FSM framework.
- No character-specific logic
- Used by Character/AI systems

---

### UI/
All UI-related logic.

- Popup/
- Scene/
- SubItem/
- UIModule/

UI code must not reference gameplay logic directly.

---

### Utils/
- Pure helper functions
- No Unity scene references

### Tools/
- Editor or development tools only
- Must not be included in runtime builds
