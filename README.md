# GreenThumb

4BHITS Jahres Projekt - Benedikt Bliem / David Unterberger / Georg Schönerer

## GreenThumb - Technical Agro-Climatic & Small Business Simulation

## Project Overview

Data-driven farming simulation focused on:

- regional climate, soil / crop interaction, weather impact
- crop yield + quality, pests / diseases
- small business / cash flow
- reading data dashboards

Main idea:

- player starts in spring and manages farm until winter
- decisions should have realistic consequencesy
- observe -> diagnose -> decide -> see result
- educational value is a big focus
- realistic enough to teach something, but still implementable for us

## Core Architecture

- Weather Data: real historical data, Temperature, Solar Radiation, Humidity, Precipitation, Wind
- one location active at a time
- globe scene for location selection
- location changes climate, suitable crops, prices and risks
- production system = Open Field or Greenhouse
- 3D 3rd person view: walk around, inspect fields, interact with equipment
- dashboard / tablet for data

- Farm: 4 fields
  - each field can have different soil / crop
  - 4 fields mainly for crop rotation / field management gameplay

- Simple crop models: growth stages, water / nutrient / temperature stress, pests / disease / weeds, yield, quality, harvest window

- Soil: texture, moisture, NPK, pH, organic matter, compaction

- Economy: seeds, fertilizer, water, electricity, lease, labor, equipment, storage, selling, market prices, supply / demand

## Regions

### Cold / Nordic

- Reykjavik, Moscow
- short growing window, frost risk, low temperatures, heating can matter

### Temperate / Europe

- London, Paris
- moderate temperatures, regular rainfall, open field focused, excess moisture / disease can matter

### Hot / Dry

- Phoenix, Dubai
- high temperature, high irrigation demand, drought risk, water price important

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

- low capital, bulk crops, direct weather exposure, higher weather / pest risk
- main MVP system

### Greenhouse

- more control but higher investment + electricity costs
- heating, cooling, ventilation, irrigation, humidity, HVAC
- VPD (vapor pressure deficit) as advanced metric
- greenhouse not only in Spain

## Play Cycle

- Spring: planning, soil testing, crop / seed selection, planting
- Summer: growth, irrigation, fertilizer, scouting, pest / disease management, weather decisions
- Autumn: harvest, quality, sell / store, market decisions
- Winter: costs still exist, need enough money / liquidity to continue, end-of-season review
- if failed -> restart at desired location

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
- yield potential, market value

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

- player can pay for / use soil test
- shows pH, N, P, K, moisture etc.
- player has to interpret it before deciding what to do

## Water Management

- soil water balance: rain + irrigation - evaporation / transpiration - drainage
- high temperature -> higher water demand
- soil type affects water storage
- too little -> drought stress
- too much -> waterlogging / root stress
- water has a price

## Nutrient Management

- N / P / K
- pH affects nutrient availability
- too little -> deficiency / lower yield
- too much -> cost + waste + possible environmental / crop problems
- timing matters
- source / rate / time / place
- mineral / organic / manure / compost possible

## Crop Rotation / Field Management

- don't teach "monoculture destroys the soil" as a simple rule
- repeated crops can increase nutrient depletion and some pest / disease pressure
- 4 fields are mainly a gameplay structure for planning rotation
- player chooses which crop goes where each year
- peas / legumes can be useful in rotation
- different crops have different nutrient demands

Possible field tactics:

- crop rotation (3/4 felder wirtschaft etc)
- cover crops
- keeping soil covered / crop residues
- reduced tillage as a possible later mechanic
- intercropping / more complex systems only if time allows

Cover crops:

- soil protection, organic matter, nutrient cycling, weed suppression
- cost + time, and crop choice / timing matter

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
- economic threshold idea
- not simply "chemical bad / organic good"
- different methods have different costs / effects / risks

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
- quality affects price

Possible grades:

- Grade A, Grade B, damaged / waste

## Storage

Possible later / maybe MVP:

- sell now vs store
- storage cost + electricity
- spoilage / quality loss
- market price can change

## Market / Business

- seasonal market price fluctuations
- supply / demand
- too much product -> price can fall
- selling timing matters

Possible later:

- contracts, different buyers, quality requirements

### Main costs

- seeds, fertilizer, water, electricity
- lease, labor, equipment, greenhouse, storage

### Cash flow

- starting money, income, expenses, cash on hand, expected future costs

Important learning:

- revenue != profit
- profit != cash flow
- limited money -> opportunity cost

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

Economy:

- Crop value, Treatment cost, Expected loss

Player should learn to connect:

- Weather -> Soil -> Crop -> Decision -> Yield -> Profit

Possible metrics:

- kg/ha / t/ha, revenue / ha, production cost / ha, gross margin
- water use, fertilizer use, quality, soil health
- stress days, pest pressure

Do not show everything immediately.

## End of Season

Simple report:

- yield, quality, water use, fertilizer use, pest control
- revenue, costs, profit, soil condition

Also explain why things happened.

Example:

- yield reduced because of water stress during tuber development
- soil moisture was below target for 6 days

Main idea:

- don't only give score
- explain cause and effect

## Learning Factors

- soil is a system, not just dirt
- different soils store / drain water differently
- crop choice depends on climate / soil / market
- planting time + growth stage matter
- GDD can explain development
- water demand changes with weather / crop stage
- too little and too much water can both be bad
- nutrient deficiency vs excess fertilizer
- crop rotation + cover crops
- diagnose before treating
- IPM / economic thresholds
- seed variety trade-offs
- yield vs quality vs profit
- harvest timing
- market oversupply
- cash flow / opportunity cost
- external costs affect actual crop value

## Optional / If Time Allows

- greenhouse, HVAC, VPD
- storage
- economic thresholds
- biological pest control
- different seed varieties
- contracts
- detailed market system
- equipment upgrades, maintenance, equipment degradation
- reduced tillage / intercropping
- more crops / locations
- more detailed disease / soil systems

## What Not to Over-Simulate

- every plant cell
- every nutrient interaction
- every microorganism
- realistic machinery physics
- dozens of crops / pests / diseases in MVP

Goal:

- few variables
- meaningful interactions
- clear feedback
- realistic decisions

## Main Game Loop

PLAN -> PLANT -> MONITOR -> READ DATA -> DECIDE -> WEATHER CHANGES -> CROP RESPONDS -> ADJUST -> HARVEST -> SELL / STORE -> REVIEW -> NEXT SEASON

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
- how to read data dashboards
- how crops are influenced by weather / soil / management
- how production costs affect crop value
- how market conditions affect selling decisions

## Project Definition

GreenThumb = educational farming simulation + small business management.

Main chain:

Environment -> Soil + Weather -> Crop Response -> Player Decisions -> Yield + Quality -> Market -> Profit
