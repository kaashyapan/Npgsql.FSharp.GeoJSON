##Usage

###Add a Global Type mapper
```
    NpgsqlConnection.GlobalTypeMapper.UseGeoJson()
```

###Insert
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

###Read
```
    connection
    |> Sql.parameters [ ("id", Sql.int id) ]
    |> Sql.query "SELECT * FROM roundtrip where id = @id"
    |> Sql.execute
        (fun (read) ->
            // construct a reader for geo types
            let geoReader = GeoReader(read.NpgsqlReader)

            // point & polygon are read as geoPoint & geoPolygon 
            // to avoid clash with native postgres types
            {| Geom = geoReader.geoPoint "geom"
               Geog = geoReader.geoPoint "geog" |})
```
