namespace Npgsql.FSharp.GeoJSON

open Npgsql
open NpgsqlTypes
open Npgsql.FSharp
open GeoJSON.Net
open GeoJSON.Net.Geometry

[<RequireQualifiedAccess>]
type Geometry =
    static member point (name: string) (value: Point) = Sql.objOfType (box value, NpgsqlDbType.Geometry)

    static member lineString (name: string) (value: LineString) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geometry)

    static member polygon (name: string) (value: Polygon) = Sql.objOfType ((value :> obj), NpgsqlDbType.Geometry)

    static member multiPoint (name: string) (value: MultiPoint) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geometry)

    static member multiLineString (name: string) (value: MultiLineString) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geometry)

    static member multiPolygon (name: string) (value: MultiPolygon) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geometry)

    static member geometryCollection (name: string) (value: GeometryCollection) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geometry)

    static member geoJsonObject (name: string) (value: GeoJSONObject) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geometry)

[<RequireQualifiedAccess>]
type Geography =
    static member point (name: string) (value: Point) = Sql.objOfType ((value :> obj), NpgsqlDbType.Geography)

    static member lineString (name: string) (value: LineString) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geography)

    static member polygon (name: string) (value: Polygon) = Sql.objOfType ((value :> obj), NpgsqlDbType.Geography)

    static member multiPoint (name: string) (value: MultiPoint) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geography)

    static member multiLineString (name: string) (value: MultiLineString) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geography)

    static member multiPolygon (name: string) (value: MultiPolygon) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geography)

    static member geometryCollection (name: string) (value: GeometryCollection) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geography)

    static member geoJsonObject (name: string) (value: GeoJSONObject) =
        Sql.objOfType ((value :> obj), NpgsqlDbType.Geography)
