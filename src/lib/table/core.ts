import { IColumnProps, IFilterProps } from "../types";

export function convertData<T>(columns: IColumnProps[], body: T[]): (string | number)[][] {
    return body.map((item) => {
        const _vals: (string | number)[] = [];
        columns.forEach((col) => {
            const val = item[col.identifier as keyof T];
            _vals.push(val as string | number);
        });
        return _vals;
    });
}

export function extractColumns<T>(instance: T): IColumnProps[] {
    if (
        instance === null ||
        instance === undefined ||
        Array.isArray(instance) ||
        typeof instance !== "object"
    ) {
        return [] as IColumnProps[]
    }

    const keys = Object.keys(instance)
    const cols: IColumnProps[] = []
    keys.forEach(key => {
        const propType = typeof instance[key as keyof T]
        if (propType === 'string' || propType === 'number') {
            const columnProps: IColumnProps = {
                name: camelToFlat(key),
                identifier: key
            }
            cols.push(columnProps)
        }
    })
    return cols
}

// credit : https://dev.to/suyashvash/camelcase-to-normal-string-in-javascript-592g
const camelToFlat = (camel: string) => {
    const camelCase = camel.replace(/([a-z])([A-Z])/g, '$1 $2').split(" ")

    let flat = ""

    camelCase.forEach(word => {
        flat = flat + word.charAt(0).toUpperCase() + word.slice(1) + " "
    })
    return flat
}


export function checkStringVal(colVal: string, filter: IFilterProps) {

    const filterValStr = filter.val as string
    const lowerCaseColVal = colVal.toLowerCase()
    const lowerCasefilterVal = filterValStr.toLowerCase()
    switch (filter.operation) {
        case "Contains":
            if (!lowerCaseColVal.includes(lowerCasefilterVal)) {
                return false;
            }
            break;
        case "StartsWith":
            if (!colVal.toLowerCase().startsWith(lowerCasefilterVal)) {
                return false;
            }
            break;
        case "EndsWith":
            if (!lowerCaseColVal.endsWith(lowerCasefilterVal)) {
                return false;
            }
            break;
        case "Equals":
            if (lowerCaseColVal !== filterValStr.toLowerCase()) {
                return false;
            }
            break;
    }
    return true
}

export function checkNumVal(colVal: number, filter: IFilterProps) {
    const filterVal = filter.val as number
    switch (filter.operation) {
        case "Bigger":
            if (colVal <= filterVal) {
                return false;
            }
            break;
        case "Less":
            if (colVal >= filterVal) {
                return false;
            }
            break;
        case "Equals":
            if (colVal === filterVal) {
                return false;
            }
            break;
    }
    return true
}