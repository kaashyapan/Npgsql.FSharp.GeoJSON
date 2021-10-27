## Usage

### Add a Global Type mapper once
```
    NpgsqlConnection.GlobalTypeMapper.UseGeoJson()
```

### Insert
```
    let point = Point(Position(1.0, 2.0))
    connection
    |> Sql.parameters
        [ "geom", Geometry.point "geom" point
          "geog", Geography.point "geom" point ]
    |> Sql.query "INSERT INTO roundtrip (geom, geog) VALUES (@geom, @geog)"
    |> Sql.executeNonQuery
    |> ignore
```

### Read
```
    connection
    |> Sql.parameters [ ("id", Sql.int id) ]
    |> Sql.query "SELECT * FROM roundtrip where id = @id"
    |> Sql.execute
        (fun (reader) ->
            // point & polygon are read as geoPoint & geoPolygon 
            // to avoid clash with native postgres types
            {| Geom = reader.geoPoint "geom"
               Geog = reader.geoPoint "geog" |})
```

### Commands
```
Tests - dotnet run -p tests
Nupkg - build.sh
```