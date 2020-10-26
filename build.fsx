#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
nuget Fake.Core.Target //"

#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet
open Fake.IO
open System.Threading

//Npgsql.FSharp.GeoJSON.fsproj
let lib = "./src" |> Path.getFullName
let test = "./tests" |> Path.getFullName

// *** Define Targets ***
Target.create "Clean" (fun _ ->
    Trace.log " --- Cleaning stuff --- "
    [ lib + "/bin"
      lib + "/obj"
      test + "/bin"
      test + "/obj" ]
    |> Shell.cleanDirs)

Target.create "Build" (fun _ ->
    Trace.log " --- Building the app --- "
    let buildOptions = DotNet.BuildOptions.Create()
    DotNet.build (fun buildOptions -> buildOptions) lib)

Target.create "Deploy" (fun _ -> Trace.log " --- Deploying app --- ")

Target.create "Test" (fun _ -> Shell.Exec("dotnet", "run", test) |> ignore)

open Fake.Core.TargetOperators

// *** Define Dependencies ***
"Clean" ==> "Build" ==> "Deploy"

// *** Start Build ***
Target.runOrDefault "Deploy"
