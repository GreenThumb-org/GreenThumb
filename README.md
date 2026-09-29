# GreenThumb

4BHITS Jahres Projekt- Benedikt Bliem/David Unterberger/Georg Schönerer

## GreenThumb – Technical Agro-Climatic & Weather Simulation Engine

## Project Overview

A data-driven open-field simulation game focusing on regional climate impact, thermodynamic balance, and dynamic crop yield modeling using real-world historical weather data.

## Core Architecture

- Weather Data
  - Ingestion of multi-region weather datasets (Beijing, London, Phoenix, etc.)
  - Temperature, Solar Radiation, Humidity, Precipitation

- Only One Location for now at a time

- multiple locations to choose from. each with differnt crops etc.
  - Spain -> Green Houses
  - etc

- Implementing real life like soil properties (eg watering, Soil NPK(Fertilizer)/pH balancing)

- pest control choices (cheap chemical vs. organic tag)

- Simple models for plants

- Yield prediction under environmental stress vectors (frost, drought, nutrient deficiency)

- Automated climate control (HVAC, irrigation)

- Open field (low capital, high weather/pest risk, bulk crops)

- Managing cash flow: selling, buying at good times, and not destroying crops

- cost: water price, electricity, lease

- seasonal market price fluctuations

- globe scene for region selection

- 3d 3rd person view

- play cycle:
  - start in the spring and play until winter
  - need to make enough money to support costs through the winter or else no continuation
  - lenght of cycle from spring to winter -> ?
  - if you fail, you restart at a desired location

- day cycle
  - options of day lenght:
    - secondsingame per 1 hour "real life" data
    - 10s/h for 4 min ig days
    - 40s/h for 10min ig days

## Optional/Debate/UpToTesting

- equipment degradation
- Glashaus not only in spain
  
## Weather Database

[Weather Dataset (2010–2026)](# Global Historical Weather Dataset (2010–2026))

```plaintext

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

## Target Audience

This simulation game will be for people who are trying to learn about agriculture and how different environments affect plants and their price and also have a technical understanding on how to read Data Dashboards.
They should also understand how different environments and economical variations affect how crops sell and grow. The user learns a base understanding on how different factors affect plants and their value.

The target audience is able to deepen their understanding on how environmental and economical factors are responsible for what crops can be planted where and for how much profit they can be sold. It should also help them visualise how the different external costs (equipment, seeds, water, heating) affect the value of crops and their demand.
