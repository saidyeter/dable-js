import React from "react";
import { StringOperations, NumberOperations } from "./constants";

export type ColumnTypeNames = "string" | "number" | "operation";
export type ColumnType = string | number;
export interface IColumnProps {
    name?: string;
    identifier?: string;
    width?: number;
    sortable?: boolean;
    // onSort?: () => void;
    filterable?: boolean;
    // onFilter?: (val: ColumnType) => void;
    isAction?: boolean;
    valType?: ColumnTypeNames;
    headerClassName?: string
}

export interface ISortProps {
    key: number;
    orderBy: "a" | "d";
}


export type OperationType = typeof StringOperations[number] | typeof NumberOperations[number]


export interface IFilterProps {
    key: number;
    val: ColumnType
    operation: OperationType
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
            trThWrapperFilterButton?: string
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
    filterButtons?: {
        filtered: string | React.ReactElement
        unFiltered: string | React.ReactElement
    }
}
export interface IDableProps<T> {
    data: T[];
    columns?: IColumnProps[];
    style?: IDableStyleProps
    onFilter?: (filters: IFilterProps[]) => void
    onSort?: (sortCol: ISortProps) => void
}

// credit: https://stackoverflow.com/a/68526558/3516508
type Time1 = 0 | 1;
type Time3 = Time1 | 2 | 3;
type Time5 = Time3 | 4 | 5;
type Time9 = Time5 | 6 | 7 | 8 | 9;

type Hours = `${Time9}` | `${Time1}${Time9}` | `2${Time3}`;
type Minutes = `${Time5}${Time9}`;
export type Time = `${Hours}:${Minutes}`;

