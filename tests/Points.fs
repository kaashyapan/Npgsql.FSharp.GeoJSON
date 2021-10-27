module Points

open Expecto
open Npgsql
open Npgsql.FSharp
open Npgsql.FSharp.GeoJSON
open GeoJSON.Net.Geometry

let pointsTest connection =
    [ test "Roundtrip point" {
        let point = Point(Position(1.0, 2.0))
        let id = 1

        connection
        |> Sql.parameters [ ("id", Sql.int id)
                            ("geom", Geometry.point point)
                            ("geog", Geography.point point) ]
        |> Sql.query "INSERT INTO roundtrip (id, geom, geog) VALUES (@id, @geom, @geog)"
        |> Sql.executeNonQuery
        |> ignore

        connection
        |> Sql.parameters [ ("id", Sql.int id)
                            ("geom", Geometry.point point)
                            ("geog", Geography.point point) ]
        |> Sql.query "SELECT * FROM roundtrip where id = @id and geom = @geom and geog = @geog"
        |> Sql.execute
            (fun (reader) ->
                {| Geom = reader.geoPoint "geom"
                   Geog = reader.geoPoint "geog" |})
        |> function
            | output ->

                let equalityTest =
                    output.Head.Geom.Equals point
                    || output.Head.Geog.Equals point

                Expect.isTrue equalityTest "Point column roundtrip optional is not Ok"

      }
      test "Roundtrip point optional" {
          let point = Point(Position(1.0, 2.0)) |> Some
          let id = 2

          connection
          |> Sql.parameters [ ("id", Sql.int id)
                              ("geom", Geometry.pointOrNone point)
                              ("geog", Geography.pointOrNone point) ]
          |> Sql.query "INSERT INTO roundtrip (id, geom, geog) VALUES (@id, @geom, @geog)"
          |> Sql.executeNonQuery
          |> ignore

          connection
          |> Sql.parameters [ ("id", Sql.int id)
                              ("geom", Geometry.pointOrNone point)
                              ("geog", Geography.pointOrNone point) ]
          |> Sql.query "SELECT * FROM roundtrip where id = @id"
          |> Sql.execute
              (fun reader ->
                  {| Id = reader.int "id"
                     Geom = reader.geoPointOrNone "geom"
                     Geog = reader.geoPointOrNone "geog" |})
          |> function
              | output ->
                  let equalityTest =
                      (output.Head.Geom = point)
                      || (output.Head.Geog = point)

                  Expect.isTrue equalityTest "Point column roundtrip optional is not Ok"
      }
      test "Roundtrip point optional Null" {
          let point = None
          let id = 3

          connection
          |> Sql.parameters [ ("id", Sql.int id)
                              ("geom", Geometry.pointOrNone point)
                              ("geog", Geography.pointOrNone point) ]
          |> Sql.query "INSERT INTO roundtrip (id, geom, geog) VALUES (@id, @geom, @geog)"
          |> Sql.executeNonQuery
          |> ignore

          connection
          |> Sql.parameters [ ("id", Sql.int id)
                              ("geom", Geometry.pointOrNone point)
                              ("geog", Geography.pointOrNone point) ]
          |> Sql.query "SELECT * FROM roundtrip where id = @id"
          |> Sql.execute
              (fun reader ->
                  {| Id = reader.int "id"
                     Geom = reader.geoPointOrNone "geom"
                     Geog = reader.geoPointOrNone "geog" |})
          |> function
              | output ->
                  let equalityTest =
                      (output.Head.Geom = point)
                      || (output.Head.Geog = point)

                  Expect.isTrue equalityTest "Point column roundtrip optional is not Ok"
      }
      test "Roundtrip point value optional" {
          let point = Point(Position(1.0, 2.0)) |> ValueSome
          let id = 4

          connection
          |> Sql.parameters [ ("id", Sql.int id)
                              ("geom", Geometry.pointOrValueNone point)
                              ("geog", Geography.pointOrValueNone point) ]
          |> Sql.query "INSERT INTO roundtrip (id, geom, geog) VALUES (@id, @geom, @geog)"
          |> Sql.executeNonQuery
          |> ignore

          connection
          |> Sql.parameters [ ("id", Sql.int id)
                              ("geom", Geometry.pointOrValueNone point)
                              ("geog", Geography.pointOrValueNone point) ]
          |> Sql.query "SELECT * FROM roundtrip where id = @id"
          |> Sql.execute
              (fun reader ->
                  {| Id = reader.int "id"
                     Geom = reader.geoPointOrValueNone "geom"
                     Geog = reader.geoPointOrValueNone "geog" |})
          |> function
              | output ->
                  let equalityTest =
                      (output.Head.Geom = point)
                      || (output.Head.Geog = point)

                  Expect.isTrue equalityTest "Point column roundtrip value optional is not Ok"
      }
      test "Roundtrip point value optional Null" {
          let point = ValueNone
          let id = 5

          connection
          |> Sql.parameters [ ("id", Sql.int id)
                              ("geom", Geometry.pointOrValueNone point)
                              ("geog", Geography.pointOrValueNone point) ]
          |> Sql.query "INSERT INTO roundtrip (id, geom, geog) VALUES (@id, @geom, @geog)"
          |> Sql.executeNonQuery
          |> ignore

          connection
          |> Sql.parameters [ ("id", Sql.int id)
                              ("geom", Geometry.pointOrValueNone point)
                              ("geog", Geography.pointOrValueNone point) ]
          |> Sql.query "SELECT * FROM roundtrip where id = @id"
          |> Sql.execute
              (fun reader ->
                  {| Id = reader.int "id"
                     Geom = reader.geoPointOrValueNone "geom"
                     Geog = reader.geoPointOrValueNone "geog" |})
          |> function
              | output ->
                  let equalityTest =
                      (output.Head.Geom = point)
                      || (output.Head.Geog = point)

                  Expect.isTrue equalityTest "Point column roundtrip value optional is not Ok"
      } ]
