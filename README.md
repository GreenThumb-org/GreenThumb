## GreenThumb
4BHITS Jahren Projekt- Benedikt Bliem/David Unterberger/Georg Schönerer

## GreenThumb – Technical Agro-Climatic & Weather Simulation Engine

## Project Overview
A data-driven greenhouse and open-field simulation engine focusing on regional climate impact, thermodynamic balance, and dynamic crop yield modeling using real-world historical weather data (2010–2026).
## Core Architecture
1. Weather Data Pipeline (ETL)
   - Ingestion of multi-region weather datasets (Beijing, London, Phoenix, etc.)
   - Time-series playback & acceleration (Temperature, Solar Radiation, Humidity, Precipitation)

2. Microclimate & Soil Physics Engine
   - Heat transfer & greenhouse energy balance calculation
   - Soil moisture dynamics & nutrient (NPK/pH) depletion algorithms

3. Crop Physiology Model
   - GDD-based growth modeling
   - Yield prediction under environmental stress vectors (frost, drought, nutrient deficiency)

4. Control Systems & Analytics
   - Automated climate control (HVAC, irrigation, shading)
   - Real-time telemetry dashboard & scenario comparative analysis

5. **Environment Strategy:** 
	- Open field (low capital, high weather/pest risk, bulk crops)
	- Greenhouse (high setup/heating costs year-round high-value crops)

6. **Decisions:** 
	- Soil NPK/pH balancing
	- pest control choices (cheap chemical vs. organic tag)

7. **Business Simulation:** 
	- Managing cash flow
	- equipment degradation
	- water access rights
	- seasonal market price fluctuations
	- and winter heating bills
## Weather Database
[Weather Dataset (2010–2026)](# Global Historical Weather Dataset (2010–2026))
```
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

People who are trying to learn about agroculture and how different environments affect plants and their price. But who also have a Technical understanding on how to read Data Dashboards.
They should also understand how different environments and economical variations affect how crops sell sell and grow. The User needs a base Understanding on how different factors affect plants and their value.

The target audience is able to deepen their understanding on how environmental and economical factors are responsible for what crops can be planted where and for how much profit they can be sold. It should also help them visualise how the different external costs (equipment, seeds, water, heating) affect the value of crops and their demand.

