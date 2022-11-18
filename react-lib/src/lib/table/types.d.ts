import React from "react";

export type ColumnTypeNames = "string" | "number" | "operation";
export type ColumnType = string | number;
export interface IColumnProps {
    name?: string;
    identifier?: string;
    width?: number;
    sortable?: boolean;
    onSort?: () => void;
    filterable?: boolean;
    onFilter?: (val: ColumnType) => void;
    isAction?: boolean;
    valType?: ColumnTypeNames;
    headerClassName?: string
}

export interface ISortProps {
    key: number;
    orderBy: "a" | "d";
}
export interface IDableStyleProps {
    classNames?: {
        wrapper?: string;
        table?: string;
        thead?: {
            thead?: string;
            tr?: string;
            trTh?: string;
            trThWrapper?: string
            trThWrapperSortButton?: string
        }
        tbody?: {
            tbody?: string;
            tr?: string;
            trTd?: string;
            trTdWrapper?: string
            trTdWrapperSortButton?: string
        }
    },
    sortButtons?: {
        asc: string | React.ReactElement
        desc: string | React.ReactElement
        unSorted: string | React.ReactElement
    }
}
export interface IDableProps<T> {
    data: T[];
    columns?: IColumnProps[];
    style?: IDableStyleProps
}