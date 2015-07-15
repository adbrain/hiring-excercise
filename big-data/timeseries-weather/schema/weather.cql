CREATE KEYSPACE test WITH replication = {'class': 'SimpleStrategy', 'replication_factor' : 3};

CREATE TABLE test.raw_daily_weather (
    station_id text,
    date text,
    type text,
    value decimal,
    A text,
    B text,
    C text,
    time text,
    primary key(station_id, date, type)
);

COPY test.raw_daily_weather (station_id, date, type, value, a, b, c, time)
    FROM './timeseries-weather/data/2015.csv' with header=true and delimiter=',';