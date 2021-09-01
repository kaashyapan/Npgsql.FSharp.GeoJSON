module Tests

open Expecto
open Npgsql
open Npgsql.FSharp
open Points
open BadSqls

let tests connection =
    [ testList "Points" (pointsTest connection)
      testList "Bad Sqls" (badSqlsTest connection) ]
