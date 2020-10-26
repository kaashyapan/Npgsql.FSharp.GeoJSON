module Points

open Expecto
open Npgsql
open Npgsql.FSharp
open Npgsql.GeoJSON
open Npgsql.FSharp.GeoJSON
open GeoJSON.Net.Geometry


let tests connection =
    [ testList "Points tests"
          [ test "Roundtrip point"
                { let point = Point(Position(1.0, 2.0))
                  connection
                  |> Sql.parameters
                      [ "geom", Geometry.point "geom" point
                        "geog", Geography.point "geom" point ]
                  |> Sql.query "INSERT INTO roundtrip (geom, geog) VALUES (@geom, @geog)"
                  |> Sql.executeNonQuery
                  |> ignore

                  connection
                  |> Sql.parameters
                      [ "geom", Geometry.point "geom" point
                        "geog", Geography.point "geom" point ]
                  |> Sql.query "SELECT * FROM roundtrip where geom = @geom AND geog = @geog"
                  |> Sql.execute (fun read ->
                      {| Geom = GeoReader(read.NpgsqlReader).point "geom"
                         Geog = GeoReader(read.NpgsqlReader).point "geog" |})
                  |> function
                  | Error error -> raise error
                  | Ok output ->
                      let equalityTest =
                          output.Head.Geom.Equals point
                          || output.Head.Geog.Equals point

                      Expect.isTrue equalityTest "Point column roundtrip is not Ok"
            } ] ]
