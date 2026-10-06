# GreenThumb

4BHITS Jahres Projekt - Benedikt Bliem / David Unterberger / Georg Schönerer

## GreenThumb - Technical Agro-Climatic Farming Simulation

## Project Overview

Data-driven farming simulation focused on:

- regional climate, soil / crop interaction, weather impact
- crop growth, yield + quality
- pests / diseases / weeds
- field management
- reading agricultural data dashboards

Main idea:

- player starts in spring and manages crops through the season
- decisions should have realistic consequences
- observe -> diagnose -> decide -> see result
- educational value is a big focus
- realistic enough to teach something, but still implementable for us
- no money / currency / business simulation -> focus completely on crop production and farming

## Core Architecture

- Weather Data: real historical data, Temperature, Solar Radiation, Humidity, Precipitation, Wind
- one location active at a time
- globe scene for location selection
- location changes climate, suitable crops and risks
- production system = Open Field or Greenhouse
- 3D 3rd person view: walk around, inspect fields, interact with equipment
- dashboard / tablet for data

- Farm: 4 fields
  - each field can have different soil / crop
  - mainly for crop rotation and field management

- Simple crop models: growth stages, water / nutrient / temperature stress, pests / disease / weeds, yield, quality, harvest window

- Soil: texture, moisture, NPK, pH, organic matter, compaction

## Regions

### Cold / Nordic

- Reykjavik, Moscow
- short growing window, frost risk, low temperatures, heating can matter

### Temperate / Europe

- London, Paris
- moderate temperatures, regular rainfall, open field focused, excess moisture / disease can matter

### Hot / Dry

- Phoenix, Dubai
- high temperature, high irrigation demand, drought risk, water availability important

### Hot / Humid

- Singapore, Delhi
- high humidity, heavy rain, disease pressure, drainage / waterlogging

### East Asian / Seasonal

- Beijing, Tokyo
- seasonal temperature changes, different rainfall patterns, crop choice based on local conditions

### Possible later

- Spain -> Mediterranean scenario, greenhouse possible
- more locations only when they actually change gameplay

## Production Systems

### Open Field

- direct weather exposure
- strong dependence on rainfall / irrigation
- weather, drought, frost and pests are major factors
- main MVP system

### Greenhouse

- more control over environment
- heating, cooling, ventilation, irrigation, humidity
- HVAC
- VPD (vapor pressure deficit) as advanced metric
- greenhouse not only in Spain

## Play Cycle

- Spring: soil testing, field planning, crop / seed selection, planting
- Summer: growth, irrigation, fertilizer, scouting, pest / disease management, weather decisions
- Autumn: harvest, quality, field review
- Winter: end-of-season review, soil / field planning for next season
- season should be about how well the player manages the crop system, not about money

### Day cycle

- 10 sec real life = 1 in-game hour -> ~4 min day
- 40 sec real life = 1 in-game hour -> ~16 min day
- needs testing

## Crops

MVP:

- Wheat, Potatoes, Peas, Rice

Possible later:

- more crops depending on region

Different crops / varieties:

- climate requirements, soil preferences, water demand, nutrient demand
- drought tolerance, disease resistance
- yield potential, growth speed, harvest timing

### Crop growth stages

- Germination, Seedling, Vegetative, Flowering / formation, Maturity, Harvest window

### GDD

- Growing Degree Days as possible educational feature
- crop development depends on accumulated temperature, not only calendar days

## Soil

### Soil types

- Sand, Sandy Loam, Loam, Clay Loam, Clay

Effects:

- drainage, water storage, irrigation frequency
- waterlogging risk, crop suitability, field work

### Soil values

- Moisture, Nitrogen, Phosphorus, Potassium, pH
- Organic Matter, Compaction

### Soil testing

- player can use soil test
- shows pH, N, P, K, moisture etc.
- player has to interpret it before deciding what to do

## Water Management

- soil water balance: rain + irrigation - evaporation / transpiration - drainage
- high temperature -> higher water demand
- soil type affects water storage
- too little -> drought stress
- too much -> waterlogging / root stress
- goal is correct water management, not maximum water use

## Nutrient Management

- N / P / K
- pH affects nutrient availability
- too little -> deficiency / lower growth and yield
- too much -> waste + possible environmental / crop problems
- timing matters
- source / rate / time / place
- mineral / organic / manure / compost possible

## Crop Rotation / Field Management

- don't teach "monoculture destroys the soil" as a simple rule
- repeated crops can increase nutrient depletion and some pest / disease pressure
- 4 fields are mainly a gameplay structure for rotation
- player chooses which crop goes where
- peas / legumes can be useful in rotation
- different crops have different nutrient demands

Possible field tactics:

- crop rotation
- cover crops
- keeping soil covered / crop residues
- reduced tillage as possible later mechanic
- intercropping / more complex systems only if time allows

Cover crops:

- soil protection, organic matter, nutrient cycling, weed suppression
- timing matters

## Planting

- planting window, soil condition, temperature, frost risk
- moisture, seed / variety, rotation
- planting too early / late can reduce crop potential

## Plant Problems / Diagnosis

Possible causes:

- nitrogen deficiency, waterlogging, drought, root damage
- wrong pH, nutrient imbalance, frost, heat stress
- pest, weed, disease

Player should investigate instead of instantly knowing the answer.

Possible tools:

- Soil test, Field inspection, Weather dashboard, Root inspection

Main loop:

- plant looks bad -> investigate -> identify likely cause -> choose intervention -> wait -> observe result

## Pest / Weed / Disease

### Weeds

- compete for water / nutrients / light
- manual, mechanical, herbicide, prevention

### Pests

- scout -> identify -> measure population -> compare to threshold
- decide treatment / wait
- chemical / biological options

### Disease

- affected by humidity, rainfall, temperature, crop susceptibility
- prevention / monitoring / treatment

### IPM

- Integrated Pest Management
- monitoring first
- treatment when justified
- use pest / damage thresholds instead of automatically treating everything
- not simply "chemical bad / organic good"
- different methods have different effects / risks

## Yield / Crop Model

Simple model idea:

- base growth x temperature factor x water factor x nutrient factor x health factor

Stress vectors:

- drought, frost, heat, waterlogging, nutrient deficiency, pests, disease

- show player why yield changed
- stress can affect different growth stages differently

## Harvest / Quality

- harvest window
- too early -> possible lower yield / quality
- optimal -> good balance
- too late -> weather / quality / loss risk
- yield != quality

Possible grades:

- Grade A, Grade B, damaged / waste

Possible quality factors:

- maturity, weather damage, disease / pest damage, water stress, handling

## Dashboard / Data Learning

Dashboard is one of the main educational parts.

Example:

FIELD 2 - POTATOES

Soil:

- Moisture 42%, N 61, P 72, K 38, pH 6.1, Compaction medium

Crop:

- Growth stage, Growth %, Stress level

Weather:

- Temperature, Rain forecast, Humidity, Solar radiation

Pests:

- Current population, Threshold

Player should learn to connect:

- Weather -> Soil -> Crop -> Decision -> Yield + Quality

Possible metrics:

- kg/ha / t/ha
- water use, fertilizer use
- soil health
- stress days
- pest pressure
- crop quality
- growth progress

Do not show everything immediately.

## End of Season

Simple report:

- yield, quality, water use, fertilizer use
- pest / disease pressure
- soil condition
- main stress events
- field management / rotation used

Also explain why things happened.

Example:

- yield reduced because of water stress during tuber development
- soil moisture was below target for 6 days

Main idea:

- don't only give score
- explain cause and effect
- player should understand what they did right / wrong

## Learning Factors

- soil is a system, not just dirt
- different soils store / drain water differently
- crop choice depends on climate / soil
- planting time + growth stage matter
- GDD can explain development
- water demand changes with weather / crop stage
- too little and too much water can both be bad
- nutrient deficiency vs excess fertilizer
- crop rotation + cover crops
- diagnose before treating
- IPM / pest thresholds
- seed variety trade-offs
- yield vs quality
- harvest timing
- weather affects crop decisions throughout the season
- data dashboards can be used to make farming decisions

## Optional / If Time Allows

- greenhouse, HVAC, VPD
- storage / post-harvest quality
- biological pest control
- different seed varieties
- reduced tillage
- intercropping
- more crops / locations
- more detailed disease / soil systems
- equipment upgrades / maintenance / degradation

## What Not to Over-Simulate

- every plant cell
- every nutrient interaction
- every microorganism
- realistic machinery physics
- dozens of crops / pests / diseases in MVP
- overly complex climate models

Goal:

- few variables
- meaningful interactions
- clear feedback
- realistic farming decisions

## Main Game Loop

PLAN -> PLANT -> MONITOR -> READ DATA -> DIAGNOSE -> DECIDE -> WEATHER CHANGES -> CROP RESPONDS -> ADJUST -> HARVEST -> REVIEW -> NEXT SEASON

## Weather Database

Global-Weather-Dataset (2010-2026)

```text
Global-Weather-Dataset/
│
├── individual_cities/
│   ├── beijing.csv
│   ├── delhi.csv
│   ├── dubai.csv
│   ├── london.csv
│   ├── los_angeles.csv
│   ├── moscow.csv
│   ├── new_york.csv
│   ├── paris.csv
│   ├── phoenix.csv
│   ├── reykjavik.csv
│   ├── singapore.csv
│   └── tokyo.csv
│
├── merged_dataset/
│   └── Global_Weather_Dataset_2010_2026.csv
│
├── README.md
└── LICENSE
```

- one location at a time for now
- 2026 is incomplete -> keep in mind when using it as historical data

## Target Audience

People who want to learn:

- basics of agriculture
- how environments affect plants
- how to read agricultural data dashboards
- how crops are influenced by weather / soil / management
- how different farming decisions affect yield and quality

## Project Definition

GreenThumb = educational farming simulation focused on crop production and field management.

Main chain:

Environment -> Soil + Weather -> Crop Response -> Player Decisions -> Yield + Quality
