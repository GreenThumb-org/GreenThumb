# GreenThumb - GitHub Kanban Board Issues

## Topic 1 - Starting a Game and Choosing a Region

Issue US-01: Implement Region Selection & Globe Scene
- **Priority:** High (MVP)
- **Description:** As a player, I want to choose a region before starting a farm so that I can experience different growing conditions[cite: 1].
- **Acceptance Criteria:**
  - The player can choose from the available locations in the globe scene[cite: 1].
  - Only one location is active in a game[cite: 1].
  - The selected location determines the weather data used in that game[cite: 1].
  - The player can see a short summary of the location's climate[cite: 1].
<---->

Issue US-03: Implement Season Initialization & Farm Setup
- **Priority:** High (MVP)
- **Description:** As a player, I want to start a new farming season in spring so that I can plan, plant, manage and harvest crops through the season[cite: 1].
- **Acceptance Criteria:**
  - A new game begins at the defined start of the season[cite: 1].
  - The player starts with four fields[cite: 1].
  - The season progresses through crop planning, growth, harvest and end-of-season recap[cite: 1].
<---->

Issue US-02: Display Regional Climate Characteristics
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want to see the main climate characteristics of a region so that I can make informed crop choices[cite: 1].
- **Acceptance Criteria:**
  - The region view shows relevant conditions such as temperature, rainfall patterns and frost or heat risk[cite: 1].
  - The information reflects the selected location's data[cite: 1].
<---->

## Topic 2 - Farm and Field Inspection

Issue US-04: Implement First-Person Farm Movement & Tablet Interaction
- **Priority:** High (MVP)
- **Description:** As a player, I want to move around the farm in a first-person view so that I can inspect fields and interact with the farming world[cite: 1].
- **Acceptance Criteria:**
  - The player can move around the farm[cite: 1].
  - All fields can be reached and identified[cite: 1].
  - The player can interact with the tablet to open info of specific fields[cite: 1].
<---->

Issue US-05: Implement Field State Tracking & Isolated Inspection
- **Priority:** High (MVP)
- **Description:** As a player, I want to inspect each field separately so that I can understand its soil, crop and current condition[cite: 1].
- **Acceptance Criteria:**
  - Each field has its own field state[cite: 1].
  - The field view shows soil type, moisture, nutrient status, crop and growth stage where applicable[cite: 1].
  - A change to one field does not incorrectly change the state of another field[cite: 1].
<---->

Issue US-06: Implement Soil Characteristics & Texture Effects
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want to see the soil type of each field so that I can understand how it affects water, roots and crop suitability[cite: 1].
- **Acceptance Criteria:**
  - Fields can use simplified soil types: Sand, Sandy Loam, Loam, Clay Loam and Clay[cite: 1].
  - Soil type influences water storage and drainage[cite: 1].
  - The game explains the main effects of the field's soil type[cite: 1].
<---->

## Topic 3 - Weather and Time

Issue US-08: Integrate Historical Weather Datasets
- **Priority:** High (MVP)
- **Description:** As a player, I want the simulation to use real weather records so that crop conditions are based on real-world environmental variation rather than only random events[cite: 1].
- **Acceptance Criteria:**
  - The game loads weather data for the selected location[cite: 1].
  - The game has a defined fallback behavior if data is unavailable[cite: 1].
<---->

Issue US-09: Build Time Scale Controller & Progression Loop
- **Priority:** High (MVP)
- **Description:** As a player, I want time to progress at a playable speed so that I can manage crops without waiting too long[cite: 1].
- **Acceptance Criteria:**
  - The game advances through in-game hours and days[cite: 1].
  - The team can test the planned time scales: 10 seconds per in-game hour and 40 seconds per in-game hour[cite: 1].
  - Weather and crop simulation update consistently as time advances[cite: 1].
<---->

Issue US-07: Build Live Weather Dashboard
- **Priority:** High (MVP)
- **Description:** As a player, I want to view the current weather so that I can decide how to manage my crops[cite: 1].
- **Acceptance Criteria:**
  - The weather dashboard can show temperature, precipitation, humidity, solar radiation and wind when available[cite: 1].
  - The values update as in-game time progresses[cite: 1].
  - Weather data comes from the selected location's dataset[cite: 1].
<---->

Issue US-10: Provide Weather Risk Information & Guide Integration
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want to be able to access information about relevant weather conditions so that I can prepare for frost, heat, drought or heavy rain[cite: 1].
- **Acceptance Criteria:**
  - The given information combined with the guide book can help with figuring out which fields or crop stages may be affected[cite: 1].
<---->

## Topic 4 - Crop Selection and Planting

Issue US-11: Crop Selection UI & Suitability Checks
- **Priority:** High (MVP)
- **Description:** As a player, I want to choose which crop to grow in each field so that I can plan the farm according to the region and soil conditions[cite: 1].
- **Acceptance Criteria:**
  - The MVP offers Wheat, Potatoes, Peas and Rice, subject to regional suitability[cite: 1].
  - The game shows basic crop requirements before the player confirms the choice[cite: 1].
<---->

Issue US-14: Implement Planting Action & Field Initialization
- **Priority:** High (MVP)
- **Description:** As a player, I want to plant a selected crop in a field so that its growth cycle can begin[cite: 1].
- **Acceptance Criteria:**
  - The player can plant an eligible crop in a field[cite: 1].
  - A field cannot grow multiple main crops at once in the MVP[cite: 1].
  - Planting sets the crop's starting state and growth stage[cite: 1].
  - The planted crop is visible in the farm view[cite: 1].
<---->

Issue US-15: Crop Stage Progression Engine
- **Priority:** High (MVP)
- **Description:** As a player, I want to see the crop's current growth stage so that I know what the plant needs at different times[cite: 1].
- **Acceptance Criteria:**
  - The crop progresses through germination, seedling, vegetative growth, flowering or formation, maturity and harvest window as applicable[cite: 1].
  - Growth depends on elapsed time and environmental conditions[cite: 1].
  - The current stage is visible in the dashboard[cite: 1].
<---->

Issue US-13: Planting Conditions Assessment System
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want to check whether conditions are suitable for planting so that I can avoid planting at an unsuitable time[cite: 1].
- **Acceptance Criteria:**
  - Planting information shows the crop, date or growth window, temperature, frost risk and soil moisture[cite: 1].
  - The game indicates whether conditions are suitable, risky or unsuitable[cite: 1].
  - Planting outside the recommended window can affect crop development[cite: 1].
<---->

## Topic 5 - Soil, Water and Nutrients

Issue US-18: Implement Water Balance & Irrigation Action
- **Priority:** High (MVP)
- **Description:** As a player, I want to irrigate a field when needed so that I can reduce drought stress without overwatering[cite: 1].
- **Acceptance Criteria:**
  - The player can apply irrigation to a selected field[cite: 1].
  - Irrigation increases soil moisture according to the field's soil properties[cite: 1].
  - High water stress can reduce growth or yield potential[cite: 1].
  - Excess moisture can cause waterlogging or root stress[cite: 1].
  - Rainfall, drainage and crop water use also affect soil moisture[cite: 1].
<---->

Issue US-19: Implement NPK Nutrient Tracking & Fertilization
- **Priority:** High (MVP)
- **Description:** As a player, I want to apply nutrients based on field conditions so that I can prevent or correct nutrient deficiencies[cite: 1].
- **Acceptance Criteria:**
  - The player can apply a simplified fertilizer action to a selected field[cite: 1].
  - Nitrogen, phosphorus and potassium are tracked separately in the simulation[cite: 1].
  - Deficiency can reduce growth or yield potential[cite: 1].
  - Excess nutrients do not provide unlimited growth and can create negative effects[cite: 1].
  - The dashboard provides feedback about nutrient status after the action[cite: 1].
<---->

Issue US-17: Build Soil Testing Diagnostic Tool
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want to inspect soil measurements so that I can diagnose field conditions before taking action[cite: 1].
- **Acceptance Criteria:**
  - A soil inspection can show moisture, nitrogen, phosphorus, potassium and pH[cite: 1].
  - Organic matter and compaction can be included when implemented[cite: 1].
  - Results are specific to the selected field[cite: 1].
  - The values help the player identify possible issues but do not automatically select a treatment[cite: 1].
<---->

Issue US-20: Soil pH & Compaction Modeling
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want to understand how pH and soil condition affect crops so that I can identify problems that cannot be fixed by simply adding water or fertilizer[cite: 1].
- **Acceptance Criteria:**
  - The field dashboard displays pH[cite: 1].
  - Unsuitable pH can affect nutrient availability or crop performance in a simplified way[cite: 1].
  - Soil compaction or organic matter can influence crop/soil behavior if included in the simulation[cite: 1].
  - The game provides a clear explanation when one of these factors is limiting growth[cite: 1].
<---->

## Topic 6 - Crop Rotation and Soil Protection

Issue US-21: Multi-Season Crop Rotation & Residual Tracking
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want to choose crops across fields over multiple seasons so that I can learn how crop rotation affects soil and crop health[cite: 1].
- **Acceptance Criteria:**
  - The player can select crops independently for the fields[cite: 1].
  - The game remembers the state of the field (minerals; etc.) affecting future crops[cite: 1].
  - Repeated crops can influence nutrient demand or pest/disease pressure in a simplified way[cite: 1].
  - Rotation effects are shown in field information or the season report[cite: 1].
<---->

Issue US-22: Legume Nitrogen Fixation Model
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want to include peas or other legumes in a rotation so that I can learn how different crop groups affect soil nutrient cycles[cite: 1].
- **Acceptance Criteria:**
  - Peas are available in the MVP crop list[cite: 1].
  - The rotation model represents a simplified benefit of legumes to the following crop's nitrogen availability[cite: 1].
  - The effect is not presented as unlimited or guaranteed fertilizer replacement[cite: 1].
<---->

Issue US-23: Cover Crop Off-Season System
- **Priority:** Low (Optional / Later)
- **Description:** As a player, I want to plant a cover crop between main crops so that I can protect soil and improve soil health[cite: 1].
- **Acceptance Criteria:**
  - The player can choose a cover crop during an available off-season window[cite: 1].
  - Cover crops can provide simplified soil-cover, erosion-protection or nutrient-cycling benefits[cite: 1].
  - Benefits depend on choosing a suitable time and allowing the cover crop to establish[cite: 1].
<---->

## Topic 7 - Crop Health and Diagnosis

Issue US-24: Crop Stress Indicators & Visual Feedback
- **Priority:** High (MVP)
- **Description:** As a player, I want to notice when crop health or growth is deteriorating so that I can investigate before the problem becomes severe[cite: 1].
- **Acceptance Criteria:**
  - Crops can display visible or dashboard-based stress indicators[cite: 1].
  - Possible stress causes include drought, waterlogging, frost, heat, nutrient deficiency, pests and disease[cite: 1].
  - Indicators reflect the simulation state rather than appearing randomly[cite: 1].
<---->

Issue US-25: Problem Investigation UI & Symptom Inspector
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want to investigate an unhealthy crop using field observations and dashboard data so that I can identify the most likely cause[cite: 1].
- **Acceptance Criteria:**
  - The player can inspect crop symptoms and relevant field data[cite: 1].
  - Weather history can help explain recent frost, heat or heavy rain effects[cite: 1].
  - Symptoms can have more than one possible cause where appropriate[cite: 1].
  - The game gives enough clues to make a reasoned diagnosis without always revealing the answer immediately[cite: 1].
<---->

Issue US-27: Pest Population Tracking & Field Scouting
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want to monitor pest populations and choose a suitable response so that I can prevent avoidable crop damage[cite: 1].
- **Acceptance Criteria:**
  - The player can scout a field and see a simplified pest level or population estimate[cite: 1].
  - Pest pressure can increase or decrease over time depending on conditions and actions[cite: 1].
  - The player can wait and monitor or apply an available control method[cite: 1].
  - Treatment effects and possible downsides are shown in the simulation[cite: 1].
  - Control decisions are based on likely crop damage, not money or economic thresholds[cite: 1].
<---->

Issue US-29: Integrated Pest Management (IPM) System
- **Priority:** Low (Optional / Later)
- **Description:** As a player, I want to compare monitoring, prevention and treatment options so that I learn not to apply pest control automatically[cite: 1].
- **Acceptance Criteria:**
  - The player can inspect pest pressure before treating[cite: 1].
  - Available control options have different effectiveness, speed or side effects (potentially)[cite: 1].
  - The game does not present chemical control as always bad or organic/biological control as always best[cite: 1].
<---->

Issue US-28: Disease Risk & Treatment Mechanics
- **Priority:** Low (Optional / Later)
- **Description:** As a player, I want to recognize and respond to crop disease risks so that I can reduce damage to my crops[cite: 1].
- **Acceptance Criteria:**
  - Disease risk can be influenced by weather, humidity crop condition[cite: 1].
  - The player can inspect disease indicators[cite: 1].
  - At least one preventive or treatment action can be implemented if this feature is included[cite: 1].
  - Disease pressure can affect crop health, yield or quality[cite: 1].
<---->

## Topic 8 - Growth, Yield and Harvest

Issue US-30: Environmental Impact Engine on Yield & Growth
- **Priority:** High (MVP)
- **Description:** As a player, I want weather, soil and management decisions to affect crop development so that I can learn the consequences of farming choices[cite: 1].
- **Acceptance Criteria:**
  - Temperature, water, nutrients and crop health influence growth[cite: 1].
  - Stress has different effects depending on crop and growth stage where implemented[cite: 1].
  - Growth and yield are not determined by time alone[cite: 1].
  - The dashboard or season report identifies the main causes of reduced performance[cite: 1].
<---->

Issue US-31: Harvest Window & Maturity Indicators
- **Priority:** High (MVP)
- **Description:** As a player, I want to see when a crop is approaching maturity so that I can plan the harvest at a suitable time[cite: 1].
- **Acceptance Criteria:**
  - The crop has a maturity state or harvest window[cite: 1].
  - The dashboard shows the current maturity/harvest status[cite: 1].
  - Harvesting too early or too late can affect yield or quality[cite: 1].
<---->

Issue US-32: Harvest Action & Field State Reset
- **Priority:** High (MVP)
- **Description:** As a player, I want to harvest a mature field so that I can complete the crop cycle and review the result[cite: 1].
- **Acceptance Criteria:**
  - Harvest records the field's yield and quality result[cite: 1].
  - The field becomes available for post-harvest management or the next season[cite: 1].
<---->

Issue US-33: Crop Quality Calculation Model
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want to see crop quality separately from total yield so that I understand that a large harvest is not always a high-quality harvest[cite: 1].
- **Acceptance Criteria:**
  - The harvest result includes yield and a quality indicator[cite: 1].
  - Quality can be affected by maturity, weather damage, water stress, pests or disease[cite: 1].
<---->

## Topic 9 - Dashboard and Learning Feedback

Issue US-34: Main Farming Dashboard UI
- **Priority:** High (MVP)
- **Description:** As a player, I want to view important field, crop and weather data in one place so that I can make informed decisions[cite: 1].
- **Acceptance Criteria:**
  - The dashboard includes soil, crop stage/health, current weather and relevant pest or disease information[cite: 1].
  - Data corresponds to the currently selected field and region[cite: 1].
  - Values and labels use understandable units and descriptions[cite: 1].
  - The dashboard shows essential data first and avoids overwhelming the player[cite: 1].
<---->

Issue US-36: End-of-Season Report Generator
- **Priority:** High (MVP)
- **Description:** As a player, I want an end-of-season report so that I can understand how well I managed the fields and what I should change next season[cite: 1].
- **Acceptance Criteria:**
  - The report includes yield, quality, water use, nutrient status/use, pest or disease pressure and soil condition where tracked[cite: 1].
  - It highlights important stress events during the season[cite: 1].
  - It explains major effects on yield or quality, such as drought during a sensitive growth stage[cite: 1].
  - The report contains no currency, sales, prices, profit or business metrics[cite: 1].
<---->

Issue US-35: Decision Feedback & Tablet Help Integration
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want feedback after making a farming decision so that I can learn from its consequences[cite: 1].
- **Acceptance Criteria:**
  - The game shows the help and info on the tablet[cite: 1].
  - Feedback explains the most relevant cause where the simulation can determine it[cite: 1].
  - The game avoids claiming certainty when multiple causes may explain a problem[cite: 1].
<---->

Issue US-37: Agricultural Concepts Glossary & In-Game Guide
- **Priority:** Medium (MVP Support)
- **Description:** As a player, I want short explanations of agricultural concepts when they are relevant so that I can understand the science behind my decisions[cite: 1].
- **Acceptance Criteria:**
  - Explanations are available for key concepts such as soil texture, waterlogging, NPK, crop rotation and pest monitoring[cite: 1].
  - Explanations use simple language and connect to the current field or decision[cite: 1].
  - Explanations do not interrupt normal gameplay unnecessarily[cite: 1].
<---->

## Topic 10 - Optional Production Systems and Expansion

Issue US-40: Game Save & Load Persistence
- **Priority:** Low (Optional / Later)
- **Description:** As a player, I want to save my farm and continue later so that I do not lose my progress[cite: 1].
- **Acceptance Criteria:**
  - A save stores the selected location, in-game date, field states, crops, soil conditions and relevant simulation state[cite: 1].
  - Loading restores the saved state consistently[cite: 1].
  - If saving is not feasible for the first prototype, this story can be moved out of the MVP[cite: 1].
<---->