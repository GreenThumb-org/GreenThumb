# GreenThumb - User Stories

4BHITS Jahres Projekt - Benedikt Bliem / David Unterberger / Georg Schönerer

## Purpose

User stories for the current GreenThumb scope: an educational farming simulation focused on crop production, soil, weather and field management. Money, currency, market prices and business management are out of scope.

Priorities:

- MVP = needed for the first playable version
- Later = optional / if time allows

Stories inside each topic are sorted from highest to lowest priority:

- High = core gameplay loop, needed first
- Medium = important MVP support / depth
- Low = optional or can be cut if time is short

## Topic 1 - Starting a Game and Choosing a Region

### US-01 - Choose a region

Priority: High

As a player, I want to choose a region before starting a farm so that I can experience different growing conditions.

Acceptance criteria:

- The player can choose from the available locations in the globe scene.
- Only one location is active in a game.
- The selected location determines the weather data used in that game.
- The player can see a short summary of the location's climate.

### US-03 - Start a new season

Priority: High

As a player, I want to start a new farming season in spring so that I can plan, plant, manage and harvest crops through the season.

Acceptance criteria:

- A new game begins at the defined start of the season.
- The player starts with four fields.
- The season progresses through crop planning, growth, harvest and end-of-season recap.

### US-02 - Understand regional differences

Priority: Medium

As a player, I want to see the main climate characteristics of a region so that I can make informed crop choices.

Acceptance criteria:

- The region view shows relevant conditions such as temperature, rainfall patterns and frost or heat risk.
- The information reflects the selected location's data.

## Topic 2 - Farm and Field Inspection

### US-04 - Explore the farm

Priority: High

As a player, I want to move around the farm in a first-person view so that I can inspect fields and interact with the farming world.

Acceptance criteria:

- The player can move around the farm.
- All fields can be reached and identified.
- The player can interact with the tablet to open info of specific fields.

### US-05 - Inspect an individual field

Priority: High

As a player, I want to inspect each field separately so that I can understand its soil, crop and current condition.

Acceptance criteria:

- Each field has its own field state.
- The field view shows soil type, moisture, nutrient status, crop and growth stage where applicable.
- A change to one field does not incorrectly change the state of another field.

### US-06 - View soil characteristics

Priority: Medium

As a player, I want to see the soil type of each field so that I can understand how it affects water, roots and crop suitability.

Acceptance criteria:

- Fields can use simplified soil types: Sand, Sandy Loam, Loam, Clay Loam and Clay.
- Soil type influences water storage and drainage.
- The game explains the main effects of the field's soil type.

## Topic 3 - Weather and Time

### US-08 - Use historical weather data

Priority: High

As a player, I want the simulation to use real weather records so that crop conditions are based on real-world environmental variation rather than only random events.

Acceptance criteria:

- The game loads weather data for the selected location.
- The game has a defined fallback behavior if data is unavailable.

### US-09 - Control the day cycle

Priority: High

As a player, I want time to progress at a playable speed so that I can manage crops without waiting too long.

Acceptance criteria:

- The game advances through in-game hours and days.
- The team can test the planned time scales: 10 seconds per in-game hour and 40 seconds per in-game hour.
- Weather and crop simulation update consistently as time advances.

### US-07 - View current weather

Priority: High

As a player, I want to view the current weather so that I can decide how to manage my crops.

Acceptance criteria:

- The weather dashboard can show temperature, precipitation, humidity, solar radiation and wind when available.
- The values update as in-game time progresses.
- Weather data comes from the selected location's dataset.

### US-10 - Understand weather risks

Priority: Medium

As a player, I want to be able to access information about relevant weather conditions so that I can prepare for frost, heat, drought or heavy rain.

Acceptance criteria:

- The given information combined with the guide book can help with figuring out which fields or crop stages may be affected.

## Topic 4 - Crop Selection and Planting

### US-11 - Choose a crop for each field

Priority: High

As a player, I want to choose which crop to grow in each field so that I can plan the farm according to the region and soil conditions.

Acceptance criteria:

- The MVP offers Wheat, Potatoes, Peas and Rice, subject to regional suitability.
- The game shows basic crop requirements before the player confirms the choice.

### US-14 - Plant a field

Priority: High

As a player, I want to plant a selected crop in a field so that its growth cycle can begin.

Acceptance criteria:

- The player can plant an eligible crop in a field.
- A field cannot grow multiple main crops at once in the MVP.
- Planting sets the crop's starting state and growth stage.
- The planted crop is visible in the farm view.

### US-15 - Follow crop growth stages

Priority: High

As a player, I want to see the crop's current growth stage so that I know what the plant needs at different times.

Acceptance criteria:

- The crop progresses through germination, seedling, vegetative growth, flowering or formation, maturity and harvest window as applicable.
- Growth depends on elapsed time and environmental conditions.
- The current stage is visible in the dashboard.

### US-13 - Check planting conditions

Priority: Medium

As a player, I want to check whether conditions are suitable for planting so that I can avoid planting at an unsuitable time.

Acceptance criteria:

- Planting information shows the crop, date or growth window, temperature, frost risk and soil moisture.
- The game indicates whether conditions are suitable, risky or unsuitable.
- Planting outside the recommended window can affect crop development.

## Topic 5 - Soil, Water and Nutrients

### US-18 - Manage irrigation

Priority: High

As a player, I want to irrigate a field when needed so that I can reduce drought stress without overwatering.

Acceptance criteria:

- The player can apply irrigation to a selected field.
- Irrigation increases soil moisture according to the field's soil properties.
- High water stress can reduce growth or yield potential.
- Excess moisture can cause waterlogging or root stress.
- Rainfall, drainage and crop water use also affect soil moisture.

### US-19 - Manage nutrients

Priority: High

As a player, I want to apply nutrients based on field conditions so that I can prevent or correct nutrient deficiencies.

Acceptance criteria:

- The player can apply a simplified fertilizer action to a selected field.
- Nitrogen, phosphorus and potassium are tracked separately in the simulation.
- Deficiency can reduce growth or yield potential.
- Excess nutrients do not provide unlimited growth and can create negative effects.
- The dashboard provides feedback about nutrient status after the action.

### US-17 - Test soil

Priority: Medium

As a player, I want to inspect soil measurements so that I can diagnose field conditions before taking action.

Acceptance criteria:

- A soil inspection can show moisture, nitrogen, phosphorus, potassium and pH.
- Organic matter and compaction can be included when implemented.
- Results are specific to the selected field.
- The values help the player identify possible issues but do not automatically select a treatment.

### US-20 - Understand pH and soil condition

Priority: Medium

As a player, I want to understand how pH and soil condition affect crops so that I can identify problems that cannot be fixed by simply adding water or fertilizer.

Acceptance criteria:

- The field dashboard displays pH.
- Unsuitable pH can affect nutrient availability or crop performance in a simplified way.
- Soil compaction or organic matter can influence crop/soil behavior if included in the simulation.
- The game provides a clear explanation when one of these factors is limiting growth.

## Topic 6 - Crop Rotation and Soil Protection

### US-21 - Plan crop rotation across fields

Priority: Medium

As a player, I want to choose crops across fields over multiple seasons so that I can learn how crop rotation affects soil and crop health.

Acceptance criteria:

- The player can select crops independently for the fields
- The game remembers the state of the field (minerals; etc.) affecting future crops
- Repeated crops can influence nutrient demand or pest/disease pressure in a simplified way.
- Rotation effects are shown in field information or the season report.

### US-22 - Use legumes in rotation

Priority: Medium

As a player, I want to include peas or other legumes in a rotation so that I can learn how different crop groups affect soil nutrient cycles.

Acceptance criteria:

- Peas are available in the MVP crop list.
- The rotation model represents a simplified benefit of legumes to the following crop's nitrogen availability.
- The effect is not presented as unlimited or guaranteed fertilizer replacement.

### US-23 - Plant a cover crop (Later)

Priority: Low

As a player, I want to plant a cover crop between main crops so that I can protect soil and improve soil health.

Acceptance criteria:

- The player can choose a cover crop during an available off-season window.
- Cover crops can provide simplified soil-cover, erosion-protection or nutrient-cycling benefits.
- Benefits depend on choosing a suitable time and allowing the cover crop to establish.

## Topic 7 - Crop Health and Diagnosis

### US-24 - Detect that a crop is under stress

Priority: High

As a player, I want to notice when crop health or growth is deteriorating so that I can investigate before the problem becomes severe.

Acceptance criteria:

- Crops can display visible or dashboard-based stress indicators.
- Possible stress causes include drought, waterlogging, frost, heat, nutrient deficiency, pests and disease.
- Indicators reflect the simulation state rather than appearing randomly.

### US-25 - Investigate crop problems

Priority: Medium

As a player, I want to investigate an unhealthy crop using field observations and dashboard data so that I can identify the most likely cause.

Acceptance criteria:

- The player can inspect crop symptoms and relevant field data.
- Weather history can help explain recent frost, heat or heavy rain effects.
- Symptoms can have more than one possible cause where appropriate.
- The game gives enough clues to make a reasoned diagnosis without always revealing the answer immediately.

### US-27 - Monitor and manage pests

Priority: Medium

As a player, I want to monitor pest populations and choose a suitable response so that I can prevent avoidable crop damage.

Acceptance criteria:

- The player can scout a field and see a simplified pest level or population estimate.
- Pest pressure can increase or decrease over time depending on conditions and actions.
- The player can wait and monitor or apply an available control method.
- Treatment effects and possible downsides are shown in the simulation.
- Control decisions are based on likely crop damage, not money or economic thresholds.

### US-29 - Use Integrated Pest Management

Priority: Low

As a player, I want to compare monitoring, prevention and treatment options so that I learn not to apply pest control automatically.

Acceptance criteria:

- The player can inspect pest pressure before treating.
- Available control options have different effectiveness, speed or side effects (potentially).
- The game does not present chemical control as always bad or organic/biological control as always best.

### US-28 - Manage crop diseases (Later / simplified MVP option)

Priority: Low

As a player, I want to recognize and respond to crop disease risks so that I can reduce damage to my crops.

Acceptance criteria:

- Disease risk can be influenced by weather, humidity crop condition.
- The player can inspect disease indicators.
- At least one preventive or treatment action can be implemented if this feature is included.
- Disease pressure can affect crop health, yield or quality.

## Topic 8 - Growth, Yield and Harvest

### US-30 - See how conditions affect crop growth

Priority: High

As a player, I want weather, soil and management decisions to affect crop development so that I can learn the consequences of farming choices.

Acceptance criteria:

- Temperature, water, nutrients and crop health influence growth.
- Stress has different effects depending on crop and growth stage where implemented.
- Growth and yield are not determined by time alone.
- The dashboard or season report identifies the main causes of reduced performance.

### US-32 - Harvest a crop

Priority: High

As a player, I want to harvest a mature field so that I can complete the crop cycle and review the result.

Acceptance criteria:

- Harvest records the field's yield and quality result.
- The field becomes available for post-harvest management or the next season.

### US-31 - Know when a crop is ready to harvest

Priority: High

As a player, I want to see when a crop is approaching maturity so that I can plan the harvest at a suitable time.

Acceptance criteria:

- The crop has a maturity state or harvest window.
- The dashboard shows the current maturity/harvest status.
- Harvesting too early or too late can affect yield or quality.

### US-33 - Understand crop quality

Priority: Medium

As a player, I want to see crop quality separately from total yield so that I understand that a large harvest is not always a high-quality harvest.

Acceptance criteria:

- The harvest result includes yield and a quality indicator.
- Quality can be affected by maturity, weather damage, water stress, pests or disease.

## Topic 9 - Dashboard and Learning Feedback

### US-34 - Use the farming dashboard

Priority: High

As a player, I want to view important field, crop and weather data in one place so that I can make informed decisions.

Acceptance criteria:

- The dashboard includes soil, crop stage/health, current weather and relevant pest or disease information.
- Data corresponds to the currently selected field and region.
- Values and labels use understandable units and descriptions.
- The dashboard shows essential data first and avoids overwhelming the player.

### US-36 - Review the season

Priority: High

As a player, I want an end-of-season report so that I can understand how well I managed the fields and what I should change next season.

Acceptance criteria:

- The report includes yield, quality, water use, nutrient status/use, pest or disease pressure and soil condition where tracked.
- It highlights important stress events during the season.
- It explains major effects on yield or quality, such as drought during a sensitive growth stage.
- The report contains no currency, sales, prices, profit or business metrics.

### US-35 - Understand why an action worked or failed

Priority: Medium

As a player, I want feedback after making a farming decision so that I can learn from its consequences.

Acceptance criteria:

- The game shows the help and info on the tablet.
- Feedback explains the most relevant cause where the simulation can determine it.
- The game avoids claiming certainty when multiple causes may explain a problem.

### US-37 - Learn agricultural concepts through play

Priority: Medium

As a player, I want short explanations of agricultural concepts when they are relevant so that I can understand the science behind my decisions.

Acceptance criteria:

- Explanations are available for key concepts such as soil texture, waterlogging, NPK, crop rotation and pest monitoring.
- Explanations use simple language and connect to the current field or decision.
- Explanations do not interrupt normal gameplay unnecessarily.

## Topic 10 - Optional Production Systems and Expansion

### US-40 - Save and continue a season

Priority: Low

As a player, I want to save my farm and continue later so that I do not lose my progress.

Acceptance criteria:

- A save stores the selected location, in-game date, field states, crops, soil conditions and relevant simulation state.
- Loading restores the saved state consistently.
- If saving is not feasible for the first prototype, this story can be moved out of the MVP.