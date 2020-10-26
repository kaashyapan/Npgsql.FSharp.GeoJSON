module Main

open Expecto
open System
open Npgsql
open System.Data
open Npgsql.FSharp

let buildDatabaseConnection =

    // Travis CI uses an empty string for the password of the database
    let databasePassword =
        let runningTravis =
            Environment.GetEnvironmentVariable "TESTING_IN_TRAVISCI"

        if isNull runningTravis
           || String.IsNullOrWhiteSpace runningTravis then "postgres" // for local tests
        else "" // for Travis CI

    let connection =
        Sql.host "xx.xxx.xx.xxx"
        |> Sql.port 5432
        |> Sql.username "xxxxxx"
        |> Sql.password "xxxxxxx"
        |> Sql.database "xxxxxxx"
        |> Sql.formatConnectionString
        |> Sql.connect

    connection

let connection = buildDatabaseConnection

let makeDB () =
    connection
    |> Sql.query "CREATE TABLE IF NOT EXISTS roundtrip (geom GEOMETRY, geog GEOGRAPHY)"
    |> Sql.executeNonQuery
    |> function
    | Ok r -> printfn "%A" "Created test table"
    | Error exn -> raise exn

let cleanupDB () =
    connection
    |> Sql.query "DROP TABLE IF EXISTS roundtrip"
    |> Sql.executeNonQuery
    |> function
    | Ok r -> printfn "%A" "Deleted test table"
    | Error exn -> raise exn


let tests =
    testList "Connection tests" (Points.tests connection)


[<EntryPoint>]
let main args =
    NpgsqlConnection.GlobalTypeMapper.UseGeoJson()
    |> ignore

    makeDB ()
    afterRunTests (fun () -> cleanupDB ())
    runTests defaultConfig tests
