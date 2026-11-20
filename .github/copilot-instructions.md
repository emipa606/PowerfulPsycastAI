# GitHub Copilot Instructions for Powerful Psycast AI (Continued) Mod

## Mod Overview and Purpose
Powerful Psycast AI (Continued) is an update of the NilchEis mod that allows Non-Player Character (NPC) AI in RimWorld to use vanilla Psycasts against their enemies. This mod provides a more dynamic and challenging gameplay experience by enabling NPCs to use powerful mind-control and manipulation abilities.

## Key Features and Systems
- **Psycast AI for NPCs**: Enables AI-controlled pawns to cast a variety of Psycasts.
  - **Default Enabled Psycasts**: Vertigo Pulse, Waterskip, Invisibility, Beckon, Blinding Pulse, Wallraise, Smokepop, Painblock, Focus, Mass Chaos Skip, Stun, Chaos Skip, Skip, Burden.
  - **Default Disabled Psycasts**: Berserk Pulse, Manhunter Pulse, Berserk.
- **Tech Level Integration**: Allows factions of Neolithic and Ultra tech levels (e.g., Tribals and Empire) to generate pawns with Psycast capabilities.
- **Settings Customization**: Mod settings allow players to adjust the generation of Psycast-capable NPC pawns.

## Coding Patterns and Conventions
- **Mod Structure**: The mod is organized into C# source files and XML definition files.
- **C# Files**: Handle the logic and behavior for Psycast jobs and AI decisions.
  - Example: `JobDriver_CastBerserk.cs`, `JobDriver_CastBeckon.cs`, etc.
- **XML Files**: Define game objects, jobs, and AI behavior using RimWorld's XML format.
  - Example: `Jobs_Misc.xml`, `ThinkTreeDefs.xml`.

## XML Integration
- **Job Definitions**: Define new job types for different Psycasts in XML files.
  - Example: `Jobs_Misc.xml` contains `JobDef` elements for various Psycasts.
- **Think Tree Definitions**: Define AI decision-making processes related to Psycasts.
  - Example: `ThinkTreeDefs.xml` contains `ThinkTreeDef` elements for AI actions.

## Harmony Patching
- **Harmony Patching**: The mod uses Harmony to patch existing game code to enable NPC Psycast AI.
  - **Example Patch**: `Faction_Empire_Patch.xml` modifies faction behavior to include Psycast-capable pawns.

## Suggestions for Copilot
- **C# Job Drivers**: Use Copilot to generate new `JobDriver` classes for additional Psycasts.
  - Example: `JobDriver_CastNewPsycast.cs`
- **XML Definitions**: Use Copilot to create or modify XML files for new Psycasts or AI behaviors.
  - Example: Add new `JobDef` entries to `Jobs_Misc.xml`.
- **Harmony Patches**: Use Copilot to generate Harmony patches for new game behavior or to modify existing patches.
  - Example: `Faction_NewFaction_Patch.xml`

## Contributing
- **Coding Standards**: Follow RimWorld coding standards and conventions.
- **Testing**: Test changes in-game and ensure compatibility with other mods.
- **Documentation**: Update the mod's documentation and README with new features and changes.

By following these guidelines, contributors can effectively use GitHub Copilot to enhance the Powerful Psycast AI (Continued) mod and contribute to a more immersive and challenging gameplay experience in RimWorld.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

