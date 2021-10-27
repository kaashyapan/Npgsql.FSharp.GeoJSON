module BadSqls

open Expecto
open Npgsql
open Npgsql.FSharp
open Npgsql.FSharp.GeoJSON
open GeoJSON.Net.Geometry

let badSqlsTest connection =
    [ test "Bad column name on insert" {
        let point = Point(Position(1.0, 2.0))
        let id = 1

        Expect.throws
            (fun () ->
                connection
                |> Sql.parameters [ ("id", Sql.int id)
                                    ("geom1", Geometry.point point)
                                    ("geog", Geometry.point point) ]
                |> Sql.query "INSERT INTO roundtrip (id, geom, geog) VALUES (@id, @geom, @geog)"
                |> Sql.executeNonQuery
                |> ignore)
            "Check invalid column fails with expected exception type"
      }
      test "Bad column type on insert" {
          let point = None
          let id = 2

          Expect.throws
              (fun () ->
                  connection
                  |> Sql.parameters [ ("id", Sql.int id)
                                      ("geom", Sql.int id)
                                      ("geog", Geometry.pointOrNone point) ]
                  |> Sql.query "INSERT INTO roundtrip (id, geom, geog) VALUES (@id, @geom, @geog)"
                  |> Sql.executeNonQuery
                  |> ignore)
              "Check invalid column fails with expected exception type"
      }
      test "Bad column name on read" {
          let point = Point(Position(1.0, 2.0)) |> ValueSome
          let id = 3

          connection
          |> Sql.parameters [ ("id", Sql.int id)
                              ("geom", Geometry.pointOrValueNone point)
                              ("geog", Geometry.pointOrValueNone point) ]
          |> Sql.query "INSERT INTO roundtrip (id, geom, geog) VALUES (@id, @geom, @geog)"
          |> Sql.executeNonQuery
          |> ignore

          Expect.throws

              (fun () ->
                  connection
                  |> Sql.parameters [ ("id", Sql.int id)
                                      ("geom", Geometry.pointOrValueNone point)
                                      ("geog", Geometry.pointOrValueNone point) ]
                  |> Sql.query "SELECT * FROM roundtrip where id = @id"
                  |> Sql.execute
                      (fun reader ->
                          {| Id = reader.int "id"
                             Geom = reader.geoPointOrValueNone "geom1"
                             Geog = reader.geoPointOrValueNone "geog" |})
                  |> ignore)
              "Check invalid column fails with expected exception type"

      }
      test "Bad column type on read" {
          let point = Point(Position(1.0, 2.0)) |> ValueSome
          let id = 4

          connection
          |> Sql.parameters [ ("id", Sql.int id)
                              ("geom", Geometry.pointOrValueNone point)
                              ("geog", Geometry.pointOrValueNone point) ]
          |> Sql.query "INSERT INTO roundtrip (id, geom, geog) VALUES (@id, @geom, @geog)"
          |> Sql.executeNonQuery
          |> ignore

          Expect.throws
              (fun () ->
                  connection
                  |> Sql.parameters [ ("id", Sql.int id) ]
                  |> Sql.query "SELECT * FROM roundtrip where id = @id"
                  |> Sql.execute
                      (fun reader ->
                          {| Id = reader.int "id"
                             Geom = reader.intOrNone "geom"
                             Geog = reader.geoPointOrValueNone "geog" |})
                  |> ignore)
              "Check invalid column fails with expected exception type"
      } ]
