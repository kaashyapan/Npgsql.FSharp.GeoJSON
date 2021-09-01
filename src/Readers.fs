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
    with
        member this.ColumnDict = columnDict
        member this.ColumnTypes = columnTypes

        member this.failToRead (column: string) (columnType: string) =
            let availableColumns =
                columnDict.Keys
                |> Seq.map (fun key -> sprintf "[%s:%s]" key columnTypes.[key])
                |> String.concat ", "

            raise
            <| UnknownColumnException(
                sprintf "Could not read column '%s' as %s. Available columns are %s" column columnType availableColumns
            )

        member this.geoPoint(column: string) : Point =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<Point>(columnIndex)
            | false, _ -> this.failToRead column "GeoPoint"

        member this.geoPointOrNone(column: string) : Point option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<Point>(columnIndex) |> Some
            | false, _ -> this.failToRead column "GeoPoint option"

        member this.geoPointOrValueNone(column: string) : Point voption =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    ValueNone
                else
                    reader.GetFieldValue<Point>(columnIndex)
                    |> ValueSome
            | false, _ -> this.failToRead column "GeoPoint voption"

        member this.lineString(column: string) : LineString =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<LineString>(columnIndex)
            | false, _ -> this.failToRead column "LineString"

        member this.lineStringOrNone(column: string) : LineString option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<LineString>(columnIndex)
                    |> Some
            | false, _ -> this.failToRead column "LineString"

        member this.lineStringOrValueNone(column: string) : LineString voption =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    ValueNone
                else
                    reader.GetFieldValue<LineString>(columnIndex)
                    |> ValueSome
            | false, _ -> this.failToRead column "LineString"

        member this.geoPolygon(column: string) : Polygon =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<Polygon>(columnIndex)
            | false, _ -> this.failToRead column "GeoPolygon"

        member this.geoPolygonOrNone(column: string) : Polygon option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<Polygon>(columnIndex) |> Some
            | false, _ -> this.failToRead column "GeoPolygon"

        member this.geoPolygonOrValueNone(column: string) : Polygon voption =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    ValueNone
                else
                    reader.GetFieldValue<Polygon>(columnIndex)
                    |> ValueSome
            | false, _ -> this.failToRead column "GeoPolygon"

        member this.multiPoint(column: string) : MultiPoint =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<MultiPoint>(columnIndex)
            | false, _ -> this.failToRead column "MultiPoint"

        member this.multiPointOrNone(column: string) : MultiPoint option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<MultiPoint>(columnIndex)
                    |> Some
            | false, _ -> this.failToRead column "MultiPoint"

        member this.multiPointOrValueNone(column: string) : MultiPoint voption =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    ValueNone
                else
                    reader.GetFieldValue<MultiPoint>(columnIndex)
                    |> ValueSome
            | false, _ -> this.failToRead column "MultiPoint"

        member this.multiLineString(column: string) : MultiLineString =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<MultiLineString>(columnIndex)
            | false, _ -> this.failToRead column "MultiLineString"

        member this.multiLineStringOrNone(column: string) : MultiLineString option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<MultiLineString>(columnIndex)
                    |> Some
            | false, _ -> this.failToRead column "MultiLineString"

        member this.multiLineStringOrValueNone(column: string) : MultiLineString voption =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    ValueNone
                else
                    reader.GetFieldValue<MultiLineString>(columnIndex)
                    |> ValueSome
            | false, _ -> this.failToRead column "MultiLineString"

        member this.multiPolygon(column: string) : MultiPolygon =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<MultiPolygon>(columnIndex)
            | false, _ -> this.failToRead column "MultiPolygon"

        member this.multiPolygonOrNone(column: string) : MultiPolygon option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<MultiPolygon>(columnIndex)
                    |> Some
            | false, _ -> this.failToRead column "MultiPolygon"

        member this.multiPolygonOrValueNone(column: string) : MultiPolygon voption =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    ValueNone
                else
                    reader.GetFieldValue<MultiPolygon>(columnIndex)
                    |> ValueSome
            | false, _ -> this.failToRead column "MultiPolygon"

        member this.geometryCollection(column: string) : GeometryCollection =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<GeometryCollection>(columnIndex)
            | false, _ -> this.failToRead column "GeometryCollection"

        member this.geometryCollectionOrNone(column: string) : GeometryCollection option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<GeometryCollection>(columnIndex)
                    |> Some
            | false, _ -> this.failToRead column "GeometryCollection"

        member this.geometryCollectionOrValueNone(column: string) : GeometryCollection voption =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    ValueNone
                else
                    reader.GetFieldValue<GeometryCollection>(columnIndex)
                    |> ValueSome
            | false, _ -> this.failToRead column "GeometryCollection"

        member this.geoJSONObject(column: string) : GeoJSONObject =
            match columnDict.TryGetValue(column) with
            | true, columnIndex -> reader.GetFieldValue<GeoJSONObject>(columnIndex)
            | false, _ -> this.failToRead column "GeoJSONObject"

        member this.geoJSONObjectOrNone(column: string) : GeoJSONObject option =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    None
                else
                    reader.GetFieldValue<GeoJSONObject>(columnIndex)
                    |> Some
            | false, _ -> this.failToRead column "GeoJSONObject"

        member this.geoJSONObjectOrValueNone(column: string) : GeoJSONObject voption =
            match columnDict.TryGetValue(column) with
            | true, columnIndex ->
                if reader.IsDBNull(columnIndex) then
                    ValueNone
                else
                    reader.GetFieldValue<GeoJSONObject>(columnIndex)
                    |> ValueSome
            | false, _ -> this.failToRead column "GeoJSONObject"
