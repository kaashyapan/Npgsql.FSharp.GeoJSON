
Add a Global Type mapper
    NpgsqlConnection.GlobalTypeMapper.UseGeoJson()

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

Requires https://github.com/kaashyapan/Npgsql.FSharp

Thats because, a GeoJSON parameter requires
```
    cmd.Parameters.AddWithValue("@p", point);
```
as per https://www.npgsql.org/doc/types/geojson.html
