namespace Npgsql.FSharp.GeoJSON

open Npgsql
open NpgsqlTypes
open Npgsql.FSharp
open GeoJSON.Net
open GeoJSON.Net.Geometry

module private Utils =
    let makeGeometry (value: IGeoJSONObject) =
        let parameter = NpgsqlParameter()
        parameter.NpgsqlDbType <- NpgsqlDbType.Geometry
        parameter.Value <- value
        SqlValue.Parameter parameter

    let makeGeography (value: IGeoJSONObject) =
        let parameter = NpgsqlParameter()
        parameter.NpgsqlDbType <- NpgsqlDbType.Geography
        parameter.Value <- value
        SqlValue.Parameter parameter

    let sqlMap (option: 'a option) (f: 'a -> SqlValue) : SqlValue =
        Option.defaultValue SqlValue.Null (Option.map f option)

    let sqlValueMap (option: 'a voption) (f: 'a -> SqlValue) : SqlValue =
        ValueOption.defaultValue SqlValue.Null (ValueOption.map f option)

[<RequireQualifiedAccess>]
type Geometry =
    static member point(value: Point) = Utils.makeGeometry value
    static member pointOrNone(value: Point option) = Utils.sqlMap value Geometry.point
    static member pointOrValueNone(value: Point voption) = Utils.sqlValueMap value Geometry.point
    static member lineString(value: LineString) = Utils.makeGeometry value
    static member lineStringOrNone(value: LineString option) = Utils.sqlMap value Utils.makeGeometry

    static member lineStringOrValueNone(value: LineString voption) =
        Utils.sqlValueMap value Utils.makeGeometry

    static member polygon(value: Polygon) = Utils.makeGeometry value
    static member polygonOrNone(value: Polygon option) = Utils.sqlMap value Utils.makeGeometry

    static member polygonOrValueNone(value: Polygon voption) =
        Utils.sqlValueMap value Utils.makeGeometry

    static member multiPoint(value: MultiPoint) = Utils.makeGeometry value
    static member multiPointOrNone(value: MultiPoint option) = Utils.sqlMap value Utils.makeGeometry

    static member multiPointOrValueNone(value: MultiPoint voption) =
        Utils.sqlValueMap value Utils.makeGeometry

    static member multiLineStringPoint(value: MultiLineString) = Utils.makeGeometry value
    static member multiLineStringOrNone(value: MultiLineString option) = Utils.sqlMap value Utils.makeGeometry

    static member multiLineStringOrValueNone(value: MultiLineString voption) =
        Utils.sqlValueMap value Utils.makeGeometry

    static member multiPolygon(value: MultiPolygon) = Utils.makeGeometry value
    static member multiPolygonOrNone(value: MultiPolygon option) = Utils.sqlMap value Utils.makeGeometry

    static member multiPolygonOrValueNone(value: MultiPolygon voption) =
        Utils.sqlValueMap value Utils.makeGeometry

    static member geometryCollection(value: GeometryCollection) = Utils.makeGeometry value
    static member geometryCollectionOrNone(value: GeometryCollection option) = Utils.sqlMap value Utils.makeGeometry

    static member geometryCollectionOrValueNone(value: GeometryCollection voption) =
        Utils.sqlValueMap value Utils.makeGeometry

    static member geoJsonObject(value: GeoJSONObject) = Utils.makeGeometry value
    static member geoJsonObjectOrNone(value: GeoJSONObject option) = Utils.sqlMap value Utils.makeGeometry

    static member geoJsonObjectOrValueNone(value: GeoJSONObject voption) =
        Utils.sqlValueMap value Utils.makeGeometry


[<RequireQualifiedAccess>]
type Geography =
    static member point(value: Point) = Utils.makeGeography value
    static member pointOrNone(value: Point option) = Utils.sqlMap value Geometry.point
    static member pointOrValueNone(value: Point voption) = Utils.sqlValueMap value Geometry.point
    static member lineString(value: LineString) = Utils.makeGeography value
    static member lineStringOrNone(value: LineString option) = Utils.sqlMap value Utils.makeGeography

    static member lineStringOrValueNone(value: LineString voption) =
        Utils.sqlValueMap value Utils.makeGeography

    static member polygon(value: Polygon) = Utils.makeGeography value
    static member polygonOrNone(value: Polygon option) = Utils.sqlMap value Utils.makeGeography

    static member polygonOrValueNone(value: Polygon voption) =
        Utils.sqlValueMap value Utils.makeGeography

    static member multiPoint(value: MultiPoint) = Utils.makeGeography value
    static member multiPointOrNone(value: MultiPoint option) = Utils.sqlMap value Utils.makeGeography

    static member multiPointOrValueNone(value: MultiPoint voption) =
        Utils.sqlValueMap value Utils.makeGeography

    static member multiLineStringPoint(value: MultiLineString) = Utils.makeGeography value
    static member multiLineStringOrNone(value: MultiLineString option) = Utils.sqlMap value Utils.makeGeography

    static member multiLineStringOrValueNone(value: MultiLineString voption) =
        Utils.sqlValueMap value Utils.makeGeography

    static member multiPolygon(value: MultiPolygon) = Utils.makeGeography value
    static member multiPolygonOrNone(value: MultiPolygon option) = Utils.sqlMap value Utils.makeGeography

    static member multiPolygonOrValueNone(value: MultiPolygon voption) =
        Utils.sqlValueMap value Utils.makeGeography

    static member geometryCollection(value: GeometryCollection) = Utils.makeGeography value
    static member geometryCollectionOrNone(value: GeometryCollection option) = Utils.sqlMap value Utils.makeGeography

    static member geometryCollectionOrValueNone(value: GeometryCollection voption) =
        Utils.sqlValueMap value Utils.makeGeography

    static member geoJsonObject(value: GeoJSONObject) = Utils.makeGeography value
    static member geoJsonObjectOrNone(value: GeoJSONObject option) = Utils.sqlMap value Utils.makeGeography

    static member geoJsonObjectOrValueNone(value: GeoJSONObject voption) =
        Utils.sqlValueMap value Utils.makeGeography
