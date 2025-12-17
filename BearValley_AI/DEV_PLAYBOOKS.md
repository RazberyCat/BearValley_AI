# BearValley_AI – Dev Playbooks

This file contains **operational rules** used by humans and agents for consistent development.
It complements `PROJECT_GUIDE.md`.

---

## 1) Prefab Rules

### 1.1 Where prefabs live
- **Gameplay prefabs** → `Assets/Resource/Prefabs/Content/`
- **UI prefabs** → `Assets/Resource/Prefabs/UI/`

> Never place prefabs under `Assets/Scripts/`.

### 1.2 Content Prefab (Gameplay)
A prefab is **Content** if it:
- Spawns in the world (player, enemy, interactable)
- Has gameplay components (HP, movement, AI, hitbox, item drop, etc.)
- Is referenced by spawners, wave systems, or scene setup

**Rules**
- Content prefabs must not reference UI prefabs directly.
- Content prefabs may raise events / signals, but do not open popups.
- Prefer composition: small components over one mega-controller.

### 1.3 UI Prefab
A prefab is **UI** if it:
- Renders UI (panels, popups, HUD, widgets)
- Depends on UI frameworks/components

**Rules**
- UI prefabs live under `Resource/Prefabs/UI/` only.
- UI prefabs may subscribe to game state via a UI-facing adapter (see 1.4).
- Avoid direct references to `Content/*` behaviours.

### 1.4 Binding rule (UI ↔ Gameplay)
To connect UI and gameplay:
- Use **data models / view models / events** as the seam
- Preferred direction:
  - Gameplay → (event/state) → UI

**Anti-patterns (do not do)**
- UI calling gameplay methods directly (e.g., `player.TakeDamage()` from a button)
- Content objects instantiating UI prefabs

### 1.5 Naming conventions
- Content prefabs: `PF_` prefix recommended (optional), e.g., `PF_Player`, `PF_Enemy_Bear`
- UI prefabs: `UI_` prefix recommended, e.g., `UI_HUD`, `UI_Popup_Settings`

---

## 2) Data Pipeline Rules (Excel → JsonData → Runtime)

### 2.1 Source vs Runtime
- `Assets/Resource/Data/Excel/` = **source-of-truth for designers** (not runtime)
- `Assets/Resource/Data/JsonData/` = **runtime-loadable** exported data

**Rule**
- Runtime code must **not** load `.xlsx` directly.
- If runtime needs data, it reads JSON (or converted ScriptableObject), not Excel.

### 2.2 Recommended pipeline
1. Edit balance/config in `Resource/Data/Excel/`
2. Export to JSON into `Resource/Data/JsonData/`
3. Runtime loads JSON into:
   - Plain C# data structs/classes, or
   - ScriptableObjects generated/updated from JSON

### 2.3 File naming
- Excel: `Balance_*.xlsx`, `Config_*.xlsx`
- JSON: `balance_*.json`, `config_*.json`

### 2.4 Versioning & safety
- Commit JSON exports alongside code changes
- When changing schema:
  - Add a migration note in the commit message
  - Keep backward compatibility when possible

### 2.5 Runtime access rules
- Loading:
  - Prefer Addressables if already in use
  - Otherwise `Resources`/direct file load only if project standard requires it
- Parsing:
  - Keep parsing isolated in a single module/service
  - Avoid parsing JSON inside gameplay components

---

## 3) Agent Workflow Checklist (Pre / During / Post)

### 3.1 Pre-flight (before editing)
Agent must:
1. Read `PROJECT_GUIDE.md`
2. Confirm target folder responsibility
3. Find precedent in existing code before inventing new patterns

**Prompt snippet**
- “Read PROJECT_GUIDE.md and follow folder responsibilities.”

### 3.2 During changes
Agent must:
- Keep edits minimal and localized
- Match naming conventions and file placement
- Avoid cross-layer dependencies (UI ↔ Content)
- Use `StateMachine/` only for framework code
- Put state implementations only in `Content/<ActorDomain>/States/` (Character/Boss/Monster/NPC ...)

### 3.3 Post-flight (before finishing)
Agent must run a quick self-review:
- [ ] New files are in correct folders
- [ ] No UI prefab referenced from Content prefab
- [ ] No runtime code reading Excel
- [ ] No Editor-only code in runtime assemblies
- [ ] FSM: state implementations live in `Scripts/Content/<ActorDomain>/States/` (Character/Boss/Monster/NPC ...)
- [ ] FSM: transitions are owned by the actor root / controller, not by UI

### 3.4 Reporting format
When delivering changes, agent should summarize:
- What changed (files + intent)
- Why it matches existing patterns
- Risks / follow-ups (if any)

### 3.5 “Stop and ask” triggers
Agent must stop and ask if:
- There is no clear precedent for a pattern
- It requires changing folder responsibilities
- It introduces a new framework (DI/event bus/etc.)

---

## 4) Quick copy prompts

### Re-sync after guide updates
“PROJECT_GUIDE.md was updated. Re-read it and confirm understanding.”

### Work request template
“Follow PROJECT_GUIDE.md strictly. Implement X with minimal changes, matching existing patterns.”

