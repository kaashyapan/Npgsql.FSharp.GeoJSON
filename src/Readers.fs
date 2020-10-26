namespace Npgsql.FSharp.GeoJSON

open Npgsql
open Npgsql.FSharp
open GeoJSON.Net
open GeoJSON.Net.Geometry
open System.Collections.Generic

type GeoReader(reader: NpgsqlDataReader) =
    let columnDict = Dictionary<string, int>()
    let columnTypes = Dictionary<string, string>()

    do
        // Populate the names of the columns into a dictionary
        // such that each read doesn't need to loop through all columns
        for fieldIndex in [ 0 .. reader.FieldCount - 1 ] do
            columnDict.Add(reader.GetName(fieldIndex), fieldIndex)
            columnTypes.Add(reader.GetName(fieldIndex), reader.GetDataTypeName(fieldIndex))

    let failToRead (column: string) (columnType: string) =
        let availableColumns =
            columnDict.Keys
            |> Seq.map (fun key -> sprintf "[%s:%s]" key columnTypes.[key])
            |> String.concat ", "

        failwithf "Could not read column '%s' as %s. Available columns are %s" column columnType availableColumns
    with

        member this.point(column: string): Point =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<Point>(columnIndex)
            | false, _ -> failToRead column "Point"

        member this.pointOrNone(column: string): Point option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex)
                then None
                else reader.GetFieldValue<Point>(columnIndex) |> Some
            | false, _ -> failToRead column "Point"

        member this.lineString(column: string): LineString =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<LineString>(columnIndex)
            | false, _ -> failToRead column "LineString"

        member this.lineStringOrNone(column: string): LineString option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<LineString>(columnIndex)
                    |> Some
            | false, _ -> failToRead column "LineString"

        member this.polygon(column: string): Polygon =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<Polygon>(columnIndex)
            | false, _ -> failToRead column "Polygon"

        member this.polygonOrNone(column: string): Polygon option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex)
                then None
                else reader.GetFieldValue<Polygon>(columnIndex) |> Some
            | false, _ -> failToRead column "Polygon"

        member this.multiPoint(column: string): MultiPoint =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<MultiPoint>(columnIndex)
            | false, _ -> failToRead column "MultiPoint"

        member this.multiPointOrNone(column: string): MultiPoint option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<MultiPoint>(columnIndex)
                    |> Some
            | false, _ -> failToRead column "MultiPoint"

        member this.multiLineString(column: string): MultiLineString =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<MultiLineString>(columnIndex)
            | false, _ -> failToRead column "MultiLineString"

        member this.multiLineStringOrNone(column: string): MultiLineString option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<MultiLineString>(columnIndex)
                    |> Some
            | false, _ -> failToRead column "MultiLineString"

        member this.multiPolygon(column: string): MultiPolygon =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<MultiPolygon>(columnIndex)
            | false, _ -> failToRead column "MultiPolygon"

        member this.multiPolygonOrNone(column: string): MultiPolygon option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<MultiPolygon>(columnIndex)
                    |> Some
            | false, _ -> failToRead column "MultiPolygon"

        member this.geometryCollection(column: string): GeometryCollection =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<GeometryCollection>(columnIndex)
            | false, _ -> failToRead column "GeometryCollection"

        member this.geometryCollectionOrNone(column: string): GeometryCollection option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<GeometryCollection>(columnIndex)
                    |> Some
            | false, _ -> failToRead column "GeometryCollection"

        member this.geoJSONObject(column: string): GeoJSONObject =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<GeoJSONObject>(columnIndex)
            | false, _ -> failToRead column "GeoJSONObject"

        member this.geoJSONObjectOrNone(column: string): GeoJSONObject option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<GeoJSONObject>(columnIndex)
                    |> Some
            | false, _ -> failToRead column "GeoJSONObject"
