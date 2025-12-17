# BearValley_AI – Project Guide

> This document defines **non‑negotiable rules** for project structure, architecture, and coding patterns.
> All changes must follow this guide unless the guide itself is updated first.

---

## 0. Golden Rules (Must Follow)

1. **Folder location defines responsibility**
2. Do **not** introduce new architectural patterns without documenting them here
3. Prefer **existing patterns** over refactoring to “better” ones
4. Avoid cross‑layer dependencies (UI ↔ Gameplay)
5. When unsure, **search existing code and follow precedent**

---

## 1. Project Structure Overview

### Assets/Resource (Non-code assets)

```
Resource/
 ├─ Animations/        # Animator, animation clips
 ├─ Asset/             # Third-party or external art assets
 │   ├─ 2D Pixel Art Platformer
 │   ├─ Hyenas
 │   └─ Pet Cats pack
 ├─ Data/
 │   ├─ Excel/          # Design/balance source data (not runtime)
 │   └─ JsonData/       # Runtime-loadable JSON data
 ├─ Effect/             # VFX, particles
 ├─ Materials/
 ├─ Modeling/           # 3D/2D source models
 ├─ Prefabs/
 │   ├─ Content/        # Gameplay prefabs
 │   └─ UI/             # UI prefabs
 ├─ Shaders/
 ├─ Sounds/
 ├─ Spine/              # Spine animation data
 └─ Sprites/
     └─ Dummy/           # Placeholder visuals
```

**Rules**
- `Resource/` contains **assets only**, no scripts
- Runtime scripts may reference assets, never the opposite
- `Data/Excel` is source-only; gameplay uses converted data

---



```
Assets/
 ├─ AddressableAssetsData/
 ├─ Resource/
 ├─ Scenes/
 └─ Scripts/
    ├─ Content/
    ├─ StateMachine/
    ├─ Scenes/
    ├─ UI/
    ├─ Settings/
    ├─ Tools/
    └─ Utils/
```

### Core Principle
- `Scripts/Content` = **Game domain logic**
- `Scripts/StateMachine` = **Reusable FSM framework**
- `Scripts/UI` = **All UI logic**
- `Scripts/Utils` = **Pure helpers only**
- `Scripts/Tools` = **Editor / dev tools only**

---

## 2. Scripts/Content Rules

### Content/Character/
- Character‑specific runtime logic
- No UI or scene management

#### Character/States/
- **All character states must live here**
- States must:
  - Not poll input directly
  - Not manipulate UI
  - Not change scenes

---

### Content/Controller/
- Coordinates multiple systems
- Owns transitions and high‑level decisions

---

### Content/Data/
- ScriptableObjects and pure data definitions
- No runtime logic

---

### Content/Input/
- Input abstraction layer
- No gameplay decisions

---

### Content/Manager/
- High‑level lifecycle managers
- Minimal logic (mostly orchestration)

---

### Content/Module/
- Pluggable gameplay modules
- Must be reusable and isolated

---

## 3. FSM (State Machine) Rules

### StateMachine/
- Contains **framework only**
- No character‑specific logic

### FSM Rules
- States live under `Content/Character/States`
- Transitions are controlled by the owner (Character / Controller)
- States must be deterministic and side‑effect limited

---

## 4. UI Rules

### Scripts/UI/
UI code must **never** directly control gameplay systems.

Sub‑folders:
- `Popup/` – modal or temporary UI
- `Scene/` – scene‑bound UI
- `SubItem/` – reusable UI components
- `UIModule/` – UI logic modules

---

## 5. Utils & Tools

### Utils/
- Pure helper functions
- No scene references
- No gameplay logic

### Tools/
- Editor or development utilities only
- Must not be included in runtime builds

---

## 6. Coding Standards

- Avoid unnecessary allocations
- Prefer clarity over cleverness
- Logging must be removable / configurable
- Async/await only if explicitly required

---

## 7. Change Policy

If a change:
- Adds a new top‑level folder
- Introduces a new architectural pattern
- Breaks an existing rule

➡ **This document must be updated first**

---

## 8. Agent Instructions (For Cursor / AI)

- Always read this file before making changes
- Follow existing folder responsibilities strictly
- Do not refactor architecture unless instructed

> This guide is treated as a **contract**, not documentation.

