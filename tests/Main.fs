module Main

open Expecto
open System
open Npgsql
open System.Data
open Npgsql.FSharp

let buildDatabaseConnection =

    let connection =
        Sql.host "xx.xx.xxx.xxx"
        |> Sql.port 5432
        |> Sql.username "xxxxxx"
        |> Sql.password "xxxxxxxxxx"
        |> Sql.database "xxxxxxx"
        |> Sql.formatConnectionString
        |> Sql.connect

    connection

let connection = buildDatabaseConnection

let makeDB () =
    connection
    |> Sql.query "CREATE TABLE IF NOT EXISTS roundtrip (id int, geom GEOMETRY, geog GEOGRAPHY)"
    |> Sql.executeNonQuery

let cleanupDB () =
    connection
    |> Sql.query "DROP TABLE IF EXISTS roundtrip"
    |> Sql.executeNonQuery


let tests =
    testList "Connection tests" (Tests.tests connection)


[<EntryPoint>]
let main args =
    NpgsqlConnection.GlobalTypeMapper.UseGeoJson()
    |> ignore

    makeDB () |> ignore
    afterRunTests (fun () -> cleanupDB () |> ignore)
    runTests defaultConfig tests
