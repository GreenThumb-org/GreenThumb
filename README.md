# GreenThumb

4BHITS Jahres Projekt - Benedikt Bliem / David Unterberger / Georg Schönerer

## GreenThumb - Technical Agro-Climatic & Small Business Simulation

## Project Overview

Data-driven farming simulation focused on:

- regional climate
- soil / crop interaction
- weather impact
- crop yield + quality
- pest / disease management
- small business / cash flow
- reading data dashboards

Main idea:

- player starts in spring and manages farm until winter
- decisions should have realistic consequences
- not just "water plant = good", player should observe -> diagnose -> decide -> see result
- educational value is very important
- should still be realistic enough to feel like a game and be implementable for us

## Core Architecture

- Weather Data
  - real historical weather data
  - Temperature
  - Solar Radiation
  - Humidity
  - Precipitation
  - Wind
  - only one location active at a time

- World / location selection
  - globe scene
  - different locations with different climate, crops, prices, risks
  - region decides climate
  - production system decides open field / greenhouse

- 3D 3rd person view
  - walk around farm
  - inspect crops / fields
  - interact with equipment
  - dashboard / tablet for data

- Farm consists of 3 fields
  - useful for 3-Felder-Wirtschaft / crop rotation
  - each field can have different soil / crop

- Simple crop models
  - growth stages
  - water / nutrient / temperature stress
  - pests / disease / weeds
  - yield
  - quality
  - harvest window

- Soil system
  - soil texture
  - moisture
  - NPK
  - pH
  - organic matter
  - compaction

- Economy
  - seeds
  - fertilizer
  - water
  - electricity
  - lease
  - labor
  - equipment
  - storage
  - selling
  - seasonal market prices
  - cash flow
  - market oversupply / price changes

## Regions

### Cold / Nordic

- Reykjavik
- Moscow
- short growing window
- frost risk
- low temperatures
- heating can matter

### Temperate / Europe

- London
- Paris
- moderate temperatures
- regular rainfall
- open field focused
- excess moisture / disease can matter

### Hot / Dry

- Phoenix
- Dubai
- high temperature
- high irrigation demand
- drought risk
- water price important

### Hot / Humid

- Singapore
- Delhi
- high humidity
- heavy rain
- disease pressure
- drainage / waterlogging

### East Asian / Seasonal

- Beijing
- Tokyo
- seasonal temperature changes
- different rainfall patterns
- crop choice based on local conditions

### Possible later

- Spain -> Mediterranean scenario, greenhouses possible
- more locations only when they actually change gameplay

## Production Systems

### Open Field

- low capital
- bulk crops
- direct weather exposure
- high weather / pest risk
- main MVP system

### Greenhouse

- more control
- higher investment + electricity costs
- HVAC
- heating
- cooling
- ventilation
- irrigation
- humidity
- VPD as advanced metric
- greenhouse not only in Spain

## Play Cycle

- Spring
  - planning
  - soil testing
  - crop / seed selection
  - planting

- Summer
  - growth
  - irrigation
  - fertilizer
  - scouting
  - pest / disease management
  - weather decisions

- Autumn
  - harvest
  - quality
  - sell / store
  - market decisions

- Winter
  - costs still exist
  - need enough money / liquidity to continue
  - end-of-season review
  - if failed -> restart at desired location

### Day cycle

- possible:
  - 10 sec real life = 1 in-game hour -> ~4 min day
  - 40 sec real life = 1 in-game hour -> ~10 min day
- needs testing

## Crops

MVP:

- Wheat
- Potatoes
- Peas

Possible later:

- more crops depending on region

Different crops / varieties have:

- climate requirements
- soil preferences
- water demand
- nutrient demand
- drought tolerance
- disease resistance
- yield potential
- market value

### Crop growth stages

- Germination
- Seedling
- Vegetative
- Flowering / formation
- Maturity
- Harvest window

### GDD

- Growing Degree Days as possible educational feature
- crop development should depend on accumulated temperature, not only calendar days

## Soil

### Soil types

- Sand
- Sandy Loam
- Loam
- Clay Loam
- Clay

Effects:

- drainage
- water storage
- irrigation frequency
- waterlogging risk
- crop suitability
- field work

### Soil values

- Moisture
- Nitrogen
- Phosphorus
- Potassium
- pH
- Organic Matter
- Compaction

### Soil testing

- player can pay for / use soil test
- shows pH, N, P, K, moisture etc.
- player has to interpret it before deciding what to do

## Water Management

- not "more water = better"
- soil water balance:
  - rain
  - irrigation
  - evaporation / transpiration
  - drainage
- high temperature -> higher water demand
- soil type affects water storage
- too little -> drought stress
- too much -> waterlogging / root stress
- water has a price

## Nutrient Management

- N / P / K
- pH affects nutrient availability
- too little fertilizer -> deficiency / lower yield
- too much -> cost + waste + possible environmental / crop problems
- timing matters
- source / rate / time / place
- mineral / organic / manure / compost possible

## Crop Rotation / 3-Felder-Wirtschaft

- repeated same crop can increase nutrient depletion and some pest / disease pressure
- 3 fields rotate crops each year
- peas / legumes can be useful in the rotation
- different crops have different nutrient demands
- possible later: cover crops
  - soil protection
  - organic matter
  - nutrient cycling
  - extra cost / time

## Planting

- planting window
- soil condition
- temperature
- frost risk
- moisture
- seed / variety
- crop rotation

## Plant Problems / Diagnosis

Possible causes:

- nitrogen deficiency
- waterlogging
- drought
- root damage
- wrong pH
- nutrient imbalance
- frost
- heat stress
- pest
- weed
- disease

Player should investigate instead of instantly knowing the answer.

Possible tools:

- Soil test
- Field inspection
- Weather dashboard
- Root inspection

Main loop:

- plant looks bad
- investigate
- identify likely cause
- choose intervention
- wait
- observe result

## Pest / Weed / Disease

### Weeds

- compete for water / nutrients / light
- manual
- mechanical
- herbicide
- prevention

### Pests

- scout
- identify
- measure population
- compare to threshold
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
- avoid "chemical bad / organic good" simplification
- different methods have different costs / effects / risks

## Yield / Crop Model

Simple model idea:

- base growth
- temperature factor
- water factor
- nutrient factor
- health factor

Stress vectors:

- drought
- frost
- heat
- waterlogging
- nutrient deficiency
- pests
- disease

Important:

- show player why yield changed
- stress can affect different growth stages differently

## Harvest / Quality

- harvest window
- too early -> possible lower yield / quality
- optimal -> good balance
- too late -> weather / quality / loss risk
- yield is not the same as quality
- quality affects price

Possible grades:

- Grade A
- Grade B
- damaged / waste

## Storage

Possible later / maybe MVP:

- sell now vs store
- storage cost
- electricity
- spoilage / quality loss
- market price can change

## Market / Business

- seasonal market price fluctuations
- supply / demand
- too much product -> price can fall
- selling timing matters
- possible later:
  - contracts
  - different buyers
  - quality requirements

### Main costs

- seeds
- fertilizer
- water
- electricity
- lease
- labor
- equipment
- greenhouse
- storage

### Cash flow

- starting money
- income
- expenses
- cash on hand
- expected future costs

Important learning:

- revenue != profit
- profit != cash flow
- limited money -> opportunity cost

## Dashboard / Data Learning

Dashboard is one of the main educational parts.

Example:

FIELD 2 - POTATOES

Soil:

- Moisture 42%
- N 61
- P 72
- K 38
- pH 6.1
- Compaction medium

Crop:

- Growth stage
- Growth %
- Stress level

Weather:

- Temperature
- Rain forecast
- Humidity
- Solar radiation

Pests:

- Current population
- Threshold

Economy:

- Crop value
- Treatment cost
- Expected loss

Important:

- player should learn to connect:
  - Weather
  - Soil
  - Crop
  - Decision
  - Yield
  - Profit

Possible metrics:

- kg/ha / t/ha
- revenue / ha
- production cost / ha
- gross margin
- water use
- fertilizer use
- quality
- soil health
- stress days
- pest pressure

Do not show everything immediately.

## Learning Factors

- soil is a system, not just dirt
- soil type affects water and crop suitability
- crop choice depends on climate / soil / market
- planting time matters
- crop growth stages matter
- GDD can explain development
- water needs change with weather and crop stage
- too little and too much water can both be bad
- nutrient deficiency vs excess fertilizer
- crop rotation
- cover crops possible
- diagnose before treating
- IPM / economic thresholds
- seed variety trade-offs
- yield vs quality vs profit
- harvest timing
- market oversupply
- cash flow / opportunity cost
- external costs affect actual crop value

## End of Season

Show a simple report:

- yield
- quality
- water use
- fertilizer use
- pest control
- revenue
- costs
- profit
- soil condition

Also explain WHY results happened.

Example:

- yield reduced because of water stress during tuber development
- soil moisture was below target for 6 days

Main idea:

- don't only give score
- explain cause and effect

## Optional / If Time Allows

- greenhouse
- HVAC
- VPD
- storage
- cover crops
- economic thresholds
- biological pest control
- different seed varieties
- contracts
- detailed market system
- equipment upgrades
- maintenance
- equipment degradation
- more crops
- more locations
- more detailed disease / soil systems

## What Not to Over-Simulate

- every plant cell
- every nutrient interaction
- every microorganism
- realistic machinery physics
- dozens of crops in MVP
- dozens of pests / diseases

Goal:

- few variables
- meaningful interactions
- clear feedback
- realistic decisions

## Main Game Loop

PLAN
-> PLANT
-> MONITOR
-> READ DATA
-> DECIDE
-> WEATHER CHANGES
-> CROP RESPONDS
-> ADJUST
-> HARVEST
-> SELL / STORE
-> REVIEW
-> NEXT SEASON

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

Environment
-> Soil + Weather
-> Crop Response
-> Player Decisions
-> Yield + Quality
-> Market
-> Profit
