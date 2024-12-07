db = db.getSiblingDB('weather');

db.createCollection('forecast');

db.forecast.insertMany([
    {
        Date: {
            Year: 2024,
            Month: 12,
            Day: 7
        },
        TemperatureC: 2,
        Summary: 'Mild'
    },
    {
        Date: {
            Year: 2024,
            Month: 12,
            Day: 8
        },
        TemperatureC: 48,
        Summary: 'Warm'
    },
    {
        Date: {
            Year: 2024,
            Month: 12,
            Day: 9
        },
        TemperatureC: -11,
        Summary: 'Scorching'
    },
    {
        Date: {
            Year: 2024,
            Month: 12,
            Day: 10
        },
        TemperatureC: 7,
        Summary: 'Sweltering'
    },
    {
        Date: {
            Year: 2024,
            Month: 12,
            Day: 10
        },
        TemperatureC: 7,
        Summary: 'Hot'
    }
]);