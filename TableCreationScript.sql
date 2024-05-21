CREATE TABLE Items (
  id bigint IDENTITY(1,1),
  tpep_pickup_datetime datetime,
  tpep_dropoff_datetime datetime,
  passenger_count tinyint,
  trip_distance float,
  store_and_fwd_flag varchar(3),
  PULocationID int,
  DOLocationID int,
  fare_amount float,
  tip_amount float,
  PRIMARY KEY (ID)
)

CREATE INDEX INDEX_PULocationID
ON Items (PULocationID)