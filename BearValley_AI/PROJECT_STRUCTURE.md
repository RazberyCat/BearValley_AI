# BearValley_AI ��� Project Guide

> This document defines **non��몁egotiable rules** for project structure, architecture, and coding patterns.
> All changes must follow this guide unless the guide itself is updated first.

---

## 0. Golden Rules (Must Follow)

1. **Folder location defines responsibility**
2. Do **not** introduce new architectural patterns without documenting them here
3. Prefer **existing patterns** over refactoring to ��쐀etter��� ones
4. Avoid cross��멿ayer dependencies (UI �넄 Gameplay)
5. When unsure, **search existing code and follow precedent**

---

## 1. Project Structure Overview

### Assets/Resource (Non-code assets)

```
Resource/
 �뵜��� Animations/        # Animator, animation clips
 �뵜��� Asset/             # Third-party or external art assets
 �봻   �뵜��� 2D Pixel Art Platformer
 �봻   �뵜��� Hyenas
 �봻   �뵒��� Pet Cats pack
 �뵜��� Data/
 �봻   �뵜��� Excel/          # Design/balance source data (not runtime)
 �봻   �뵒��� JsonData/       # Runtime-loadable JSON data
 �뵜��� Effect/             # VFX, particles
 �뵜��� Materials/
 �뵜��� Modeling/           # 3D/2D source models
 �뵜��� Prefabs/
 �봻   �뵜��� Content/        # Gameplay prefabs
 �봻   �뵒��� UI/             # UI prefabs
 �뵜��� Shaders/
 �뵜��� Sounds/
 �뵜��� Spine/              # Spine animation data
 �뵒��� Sprites/
     �뵒��� Dummy/           # Placeholder visuals
```

**Rules**
- `Resource/` contains **assets only**, no scripts
- Runtime scripts may reference assets, never the opposite
- `Data/Excel` is source-only; gameplay uses converted data

---



```
Assets/
 �뵜��� AddressableAssetsData/
 �뵜��� Resource/
 �뵜��� Scenes/
 �뵒��� Scripts/
    �뵜��� Content/
    �뵜��� StateMachine/
    �뵜��� Scenes/
    �뵜��� UI/
    �뵜��� Settings/
    �뵜��� Tools/
    �뵒��� Utils/
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
- Character��몊pecific runtime logic
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
- Owns transitions and high��멿evel decisions

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
- High��멿evel lifecycle managers
- Minimal logic (mostly orchestration)

---

### Content/Module/
- Pluggable gameplay modules
- Must be reusable and isolated

---

## 3. FSM (State Machine) Rules

### StateMachine/
- Contains **framework only**
- No character��몊pecific logic

### FSM Rules
- States live under `Content/Character/States`
- Transitions are controlled by the owner (Character / Controller)
- States must be deterministic and side��멷ffect limited

---

## 4. UI Rules

### Scripts/UI/
UI code must **never** directly control gameplay systems.

Sub��멹olders:
- `Popup/` ��� modal or temporary UI
- `Scene/` ��� scene��멳ound UI
- `SubItem/` ��� reusable UI components
- `UIModule/` ��� UI logic modules

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
- Adds a new top��멿evel folder
- Introduces a new architectural pattern
- Breaks an existing rule

�옟 **This document must be updated first**

---

## 8. Agent Instructions (For Cursor / AI)

- Always read this file before making changes
- Follow existing folder responsibilities strictly
- Do not refactor architecture unless instructed

> This guide is treated as a **contract**, not documentation.

