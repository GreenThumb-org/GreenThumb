# GreenThumb - Technical GitHub Kanban Board Issues

## Topic 1 - Starting a Game and Choosing a Region

Issue US-01: Implement Region Selection & Globe Scene
- **Priority:** High (MVP)
- **Description:** Build an interactive 3D globe scene allowing the user to select from available geographic locations (e.g., Reykjavik, London, Phoenix, Singapore, Tokyo). Lock in the selected location's regional climate profile and initialize the corresponding weather dataset for the session.
- **Acceptance Criteria:**
  - Globe scene visually displays available locations.
  - Selecting a location sets it as the active scene data source.
  - Climate summary UI displays historical average temperatures, rainfall patterns, and weather risks upon location hover/click.
<---->

Issue US-03: Implement Season Initialization & Farm Setup
- **Priority:** High (MVP)
- **Description:** Initialize the seasonal simulation loop starting in Spring. Set up the environment scene with four distinct, editable field game objects and kick off the state tracking managers.
- **Acceptance Criteria:**
  - New game session instantiates at day 0 of the Spring season.
  - Instantiates four playable field data models.
  - Triggers the global time/season manager loop across planning, growth, harvest, and recap states.
<---->

Issue US-02: Display Regional Climate Characteristics
- **Priority:** Medium (MVP Support)
- **Description:** Create the regional profile UI widget to parse and display climate data (temperature ranges, precipitation, heat/frost risk ratings) derived from the selected region's dataset.
- **Acceptance Criteria:**
  - Regional info panel dynamically reads data from the active city climate file.
  - Correctly displays metrics for temperature ranges, seasonal rainfall, and risk factors.
<---->

## Topic 2 - Farm and Field Inspection

Issue US-04: Implement First-Person Farm Movement & Tablet Interaction
- **Priority:** High (MVP)
- **Description:** Implement a 3D first-person controller (WASD + mouse look) enabling player movement around the farm terrain. Add raycast interaction to open the data tablet UI when approaching fields or equipping the device.
- **Acceptance Criteria:**
  - First-person controller allows navigation across the entire farm map.
  - All four field zones feature collider bounds and visual identifiers.
  - Interacting with a field object or triggering the tablet button opens the detailed Field Info UI.
<---->

Issue US-05: Implement Field State Tracking & Isolated Inspection
- **Priority:** High (MVP)
- **Description:** Design an independent `FieldState` class/struct per field to track soil texture, moisture %, NPK levels, pH, current crop, and growth progress. Ensure state updates are strictly isolated per field instance.
- **Acceptance Criteria:**
  - Four independent `FieldState` instances exist in memory.
  - Field inspection UI displays moisture, NPK values, pH, and crop status specific to the target field.
  - Modifying one field's parameters (e.g., irrigating Field 1) leaves other field instances unchanged.
<---->

Issue US-06: Implement Soil Characteristics & Texture Effects
- **Priority:** Medium (MVP Support)
- **Description:** Implement soil texture enum logic (`Sand`, `SandyLoam`, `Loam`, `ClayLoam`, `Clay`) to adjust mathematical coefficients for water retention capacity and drainage speed.
- **Acceptance Criteria:**
  - Soil types directly alter soil moisture depletion/retention formulas.
  - UI displays soil type name and brief description of its hydraulic properties.
<---->

## Topic 3 - Weather and Time

Issue US-08: Integrate Historical Weather Datasets
- **Priority:** High (MVP)
- **Description:** Build a CSV weather data parser to load local historical weather records based on the selected city (2010–2026 dataset). Implement default fallback values if specific data entries are missing or corrupted.
- **Acceptance Criteria:**
  - Weather manager reads and parses historical temperature, precipitation, solar radiation, humidity, and wind data.
  - Provides mock/fallback weather data if the dataset fails to load.
<---->

Issue US-09: Build Time Scale Controller & Progression Loop
- **Priority:** High (MVP)
- **Description:** Construct a central `TimeManager` class to increment in-game hours and days. Implement configurable time-step scales (e.g., 10 seconds real-time = 1 hour vs. 40 seconds = 1 hour) for gameplay testing.
- **Acceptance Criteria:**
  - Clock advances hours and days continuously based on delta time.
  - Exposes configurable variables to adjust the real-seconds-per-in-game-hour ratio.
  - Dispatches tick events to weather and crop simulation controllers on each hour step.
<---->

Issue US-07: Build Live Weather Dashboard
- **Priority:** High (MVP)
- **Description:** Create a live weather dashboard UI that subscribes to the `TimeManager` and `WeatherManager` to output temperature, precipitation, humidity, solar radiation, and wind values.
- **Acceptance Criteria:**
  - UI components bind to real-time simulation output.
  - Values refresh automatically on every hourly simulation tick.
<---->

Issue US-10: Provide Weather Risk Information & Guide Integration
- **Priority:** Medium (MVP Support)
- **Description:** Build an in-game alert and documentation lookup system that flags upcoming weather events (frost, heatwave, drought, heavy rain) and cross-references crop vulnerability indices.
- **Acceptance Criteria:**
  - UI flags active/impending weather risk warnings.
  - Links risk warnings to relevant guidebook topics explaining crop impact.
<---->

## Topic 4 - Crop Selection and Planting

Issue US-11: Crop Selection UI & Suitability Checks
- **Priority:** High (MVP)
- **Description:** Build the crop selection menu listing MVP crops (`Wheat`, `Potatoes`, `Peas`, `Rice`) along with environmental requirement thresholds (soil type suitability, temperature range, water demand).
- **Acceptance Criteria:**
  - Selection UI lists all four MVP crops.
  - Displays base crop requirements and flags incompatibilities with the current field state or climate.
<---->

Issue US-14: Implement Planting Action & Field Initialization
- **Priority:** High (MVP)
- **Description:** Implement the `PlantCrop` command. Lock the target field to the chosen crop type, set initial growth parameters to zero, and swap the visual mesh/texture of the field ground model.
- **Acceptance Criteria:**
  - Executing `PlantCrop` assigns a single active crop model to the field state.
  - Prevents planting on already occupied fields.
  - Instantiates initial crop growth graphics on the 3D field model.
<---->

Issue US-15: Crop Stage Progression Engine
- **Priority:** High (MVP)
- **Description:** Build the crop development controller using a Growing Degree Day (GDD) or thermal time model to advance crops through stages: Germination -> Seedling -> Vegetative -> Flowering/Formation -> Maturity -> Harvest Window.
- **Acceptance Criteria:**
  - Crop stage updates sequentially based on accumulated heat units and simulation ticks.
  - Environmental stress factors slow down or arrest stage progression.
  - Current stage state is updated in the database and visible in the tablet UI.
<---->

Issue US-13: Planting Conditions Assessment System
- **Priority:** Medium (MVP Support)
- **Description:** Create a pre-planting diagnostic check that evaluates soil temperature, soil moisture, and seasonal date against optimal crop parameters to generate a suitability score (Suitable / Risky / Unsuitable).
- **Acceptance Criteria:**
  - Calculates suitability based on real-time environmental data vs. crop constants.
  - Displays visual indicator status (Green/Yellow/Red) in the planting menu.
  - Out-of-window planting applies growth/yield penalty multipliers.
<---->

## Topic 5 - Soil, Water and Nutrients

Issue US-18: Implement Water Balance & Irrigation Action
- **Priority:** High (MVP)
- **Description:** Implement the soil water balance equation: `Moisture_t = Moisture_t-1 + Rain + Irrigation - Evapotranspiration - Drainage`. Provide an `Irrigate` player action that adds water volume to a field.
- **Acceptance Criteria:**
  - `Irrigate()` function adds specified moisture % based on field soil capacity.
  - Evapotranspiration scales dynamically with ambient temperature and solar radiation.
  - Low moisture triggers drought stress; moisture above field capacity causes waterlogging/root stress.
<---->

Issue US-19: Implement NPK Nutrient Tracking & Fertilization
- **Priority:** High (MVP)
- **Description:** Track Nitrogen (N), Phosphorus (P), and Potassium (K) values separately in `FieldState`. Implement a `Fertilize` action to replenish NPK levels and apply deficit/excess yield penalties.
- **Acceptance Criteria:**
  - N, P, and K values decrement as crops consume nutrients per growth stage.
  - `Fertilize()` action restores NPK levels up to defined maximums.
  - Deficient NPK levels reduce growth rate; extreme over-fertilization triggers toxicity penalties.
<---->

Issue US-17: Build Soil Testing Diagnostic Tool
- **Priority:** Medium (MVP Support)
- **Description:** Create a soil sampling tool/action that reads current field parameters (Moisture %, N, P, K, pH) and outputs a detailed diagnostic panel on the tablet UI.
- **Acceptance Criteria:**
  - Triggering a soil test fetches exact field metrics.
  - Displays breakdown of Moisture, NPK, pH, and soil compaction levels without automatic solution prompts.
<---->

Issue US-20: Soil pH & Compaction Modeling
- **Priority:** Medium (MVP Support)
- **Description:** Add soil pH and compaction modifiers to the nutrient uptake formula, reducing effective NPK absorption when pH falls outside optimal crop tolerances.
- **Acceptance Criteria:**
  - Tablet UI displays field pH and compaction metrics.
  - Sub-optimal pH scales down crop nutrient intake efficiency.
  - System generates diagnostic warning notes when pH limits growth.
<---->

## Topic 6 - Crop Rotation and Soil Protection

Issue US-21: Multi-Season Crop Rotation & Residual Tracking
- **Priority:** Medium (MVP Support)
- **Description:** Persist field state properties across seasons (retaining nutrient balances and pest build-up factors) to calculate rotation bonuses or continuous monoculture penalties.
- **Acceptance Criteria:**
  - Field data models retain residual attributes between season resets.
  - Planting the same crop class sequentially increases base pest vulnerability and specific nutrient depletion rates.
  - Rotation historical log displays past crop sequences per field.
<---->

Issue US-22: Legume Nitrogen Fixation Model
- **Priority:** Medium (MVP Support)
- **Description:** Code specific nitrogen-fixation logic for legume crops (`Peas`). Upon harvesting/terminating a pea crop, add a residual Soil Nitrogen bonus to that field state for the following crop cycle.
- **Acceptance Criteria:**
  - Planting and harvesting Peas adds a fixed +N bonus to the target field's baseline soil state.
  - Nitrogen increase is logged and shown in the soil breakdown summary.
<---->

Issue US-23: Cover Crop Off-Season System
- **Priority:** Low (Optional / Later)
- **Description:** Implement an off-season cover crop state that can be planted post-harvest to reduce soil erosion and nitrogen leaching over winter.
- **Acceptance Criteria:**
  - Player can toggle cover crop planting during the post-harvest phase.
  - Cover crops active during off-season reduce N-leaching rates and stabilize compaction.
<---->

## Topic 7 - Crop Health and Diagnosis

Issue US-24: Crop Stress Indicators & Visual Feedback
- **Priority:** High (MVP)
- **Description:** Implement a global stress calculation engine for crops (accumulating Drought, Waterlogging, Heat, Frost, Nutrient Deficit, and Pest stress factors) and trigger corresponding UI alerts/3D plant visual changes.
- **Acceptance Criteria:**
  - Aggregates stress factors into an overall health percentage value.
  - Stress conditions update visual shader/texture states on 3D crop models (e.g., yellowing leaves for low N or drought).
<---->

Issue US-25: Problem Investigation UI & Symptom Inspector
- **Priority:** Medium (MVP Support)
- **Description:** Create a crop diagnostic UI panel where players can review symptom clues (e.g., leaf discoloration, stunted root growth) alongside historical weather data to infer root causes.
- **Acceptance Criteria:**
  - UI displays observed physical crop symptoms.
  - Links historical weather logs (recent temperature extremes or rain spikes) to aid in manual diagnosis.
<---->

Issue US-27: Pest Population Tracking & Field Scouting
- **Priority:** Medium (MVP Support)
- **Description:** Implement a dynamic pest population model (`PestLevel`) influenced by ambient temperature and humidity. Add a "Scout Field" action that reveals current pest intensity.
- **Acceptance Criteria:**
  - `PestLevel` grows dynamically based on favorable weather formulas.
  - "Scout Field" action updates the tablet with estimated pest population counts.
  - Pest control actions reduce pest counts with calculated efficacy rates.
<---->

Issue US-29: Integrated Pest Management (IPM) System
- **Priority:** Low (Optional / Later)
- **Description:** Build an IPM control module offering multiple pest management actions (biological control, physical barriers, chemical sprays) with distinct speed, cost, and secondary soil impact parameters.
- **Acceptance Criteria:**
  - UI offers multiple treatment methods with differing efficacy and side-effect profiles.
  - Provides monitoring thresholds to guide treatment necessity.
<---->

Issue US-28: Disease Risk & Treatment Mechanics
- **Priority:** Low (Optional / Later)
- **Description:** Implement a fungal/bacterial disease risk model driven by prolonged high relative humidity and leaf wetness duration.
- **Acceptance Criteria:**
  - High humidity + rain triggers increased `DiseaseRisk` percentage.
  - Disease outbreaks apply direct health and quality penalties to infected fields.
  - Preventive/curative fungicide treatment action reduces active disease levels.
<---->

## Topic 8 - Growth, Yield and Harvest

Issue US-30: Environmental Impact Engine on Yield & Growth
- **Priority:** High (MVP)
- **Description:** Create the main yield potential calculation function: `Yield = BaseYield * TempFactor * WaterFactor * NutrientFactor * HealthFactor`.
- **Acceptance Criteria:**
  - Growth and final yield potential calculate continuously on each hourly tick.
  - Stress events permanently truncate max achievable yield based on growth stage sensitivity.
  - Log details exact loss contributors for season review integration.
<---->

Issue US-31: Harvest Window & Maturity Indicators
- **Priority:** High (MVP)
- **Description:** Track crop maturity state and activate a designated `Harvest Window` phase upon reaching full maturity.
- **Acceptance Criteria:**
  - Reaching 100% maturity shifts crop state to `Harvest Ready`.
  - Tablet UI highlights fields in the optimal harvest window.
  - Remaining unharvested past the window triggers over-ripening quality decay.
<---->

Issue US-32: Harvest Action & Field State Reset
- **Priority:** High (MVP)
- **Description:** Implement the `HarvestField` command. Finalize and store yield weight (kg/ha) and quality grade, clear crop game objects from the field, and set field state to idle/post-harvest.
- **Acceptance Criteria:**
  - `HarvestField` calculates final crop output metrics.
  - Clears crop models from the 3D field scene.
  - Resets field status so a new crop can be planted in subsequent cycles.
<---->

Issue US-33: Crop Quality Calculation Model
- **Priority:** Medium (MVP Support)
- **Description:** Implement a secondary quality metric (Grade A, B, C, Damaged) computed independently of raw yield weight based on stress history during sensitive flowering/maturity stages.
- **Acceptance Criteria:**
  - Harvest output includes both total weight (kg/ha) and Quality Rating.
  - Severe weather, water stress, or pest damage degrades final quality rating independently of mass.
<---->

## Topic 9 - Dashboard and Learning Feedback

Issue US-34: Main Farming Dashboard UI
- **Priority:** High (MVP)
- **Description:** Design and implement the primary data tablet interface consolidating soil metrics, crop health %, growth stages, live weather, and pest warnings into a clean tabbed layout.
- **Acceptance Criteria:**
  - Displays selected field's soil parameters, crop status, live weather, and pest metrics.
  - Uses standard agricultural units (e.g., °C, mm, %, kg/ha).
  - Clean UI architecture prioritizing immediate key diagnostics.
<---->

Issue US-36: End-of-Season Report Generator
- **Priority:** High (MVP)
- **Description:** Build an end-of-season summary window that parses seasonal log data to show total yield, average quality, water spent, fertilizer applied, and key stress event timeline explanations.
- **Acceptance Criteria:**
  - Generates a full seasonal summary report upon season end.
  - Highlights specific stress root causes (e.g., "Yield decreased 15% due to 4 days of drought in Vegetative stage").
  - Strictly excludes all financial/monetary/profit metrics.
<---->

Issue US-35: Decision Feedback & Tablet Help Integration
- **Priority:** Medium (MVP Support)
- **Description:** Add contextual feedback popups and tablet help logs that explain the outcome of recent player interventions (e.g., fertilizing, irrigating).
- **Acceptance Criteria:**
  - Interventions generate informative confirmation logs in the tablet feedback tab.
  - Explanations outline primary simulation cause-and-effect pathways.
<---->

Issue US-37: Agricultural Concepts Glossary & In-Game Guide
- **Priority:** Medium (MVP Support)
- **Description:** Build an in-game wiki/guidebook system accessible via the tablet covering core agronomic concepts (Soil Textures, NPK, Crop Rotation, IPM, GDD).
- **Acceptance Criteria:**
  - Guidebook UI allows searching and navigating topic articles.
  - Articles provide educational breakdowns connected to gameplay mechanics.
<---->

## Topic 10 - Optional Production Systems and Expansion

Issue US-40: Game Save & Load Persistence
- **Priority:** Low (Optional / Later)
- **Description:** Build a JSON serialization system to save and restore all simulation states, active region, current date/time, individual field attributes, crop progress, and weather history to disk.
- **Acceptance Criteria:**
  - Save system serializes all game data models into a persistent save file.
  - Load system completely reconstructs farm, field, weather, and tablet states upon loading.
<---->
