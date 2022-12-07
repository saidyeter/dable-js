import { useState } from "react";
import { StringOperations, NumberOperations } from "../table/constants";
import {
  IColumnProps,
  IDableStyleProps,
  IFilterProps,
  ISortProps,
  OperationType,
} from "../types";

interface HeaderRowProps {
  columns: IColumnProps[];
  style?: IDableStyleProps;
  onFilter?: (filters: IFilterProps[]) => void;
  onSort: (sortCol: ISortProps) => void;
}
export function HeaderRow(props: HeaderRowProps) {
  const { columns, style, onFilter, onSort } = props;
  const { classNames } = style ?? ({} as IDableStyleProps);
  const [sorting, setSorting] = useState({} as ISortProps);

  function handleSorting(key: number) {
    const sortVal: ISortProps = {
      key,
      orderBy: "a",
    };

    if (sorting?.key === key) {
      sortVal.orderBy = sorting.orderBy === "a" ? "d" : "a";
    }
    setSorting(sortVal);

    onSort(sortVal);
  }

  function handleFiltering(key: number) {
    setFilteringColumnKey(key);
  }
  const [filterVal, setFilterVal] = useState("");
  const [filteringColumnKey, setFilteringColumnKey] = useState(-1);
  const [filterOperationType, setFilterOperationType] = useState(
    "Equals" as OperationType
  );
  const StringOperationsOptions = StringOperations.map((s, i) => (
    <option
      key={i}
      value={s}
    >
      {s}
    </option>
  ));

  const NumberOperationsOptions = NumberOperations.map((s, i) => (
    <option
      key={i}
      value={s}
    >
      {s}
    </option>
  ));
  function applyFilter() {
    const newFilters = [
      ...filters,
      {
        key: filteringColumnKey,
        operation: filterOperationType,
        val: filterVal,
      },
    ];
    setFilters(newFilters);
    setFilterVal("");
    setFilteringColumnKey(-1);
    setFilterOperationType("Equals");
    onFilter?.(newFilters);
  }
  const [filters, setFilters] = useState([] as IFilterProps[]);

  return (
    <tr className={classNames?.thead?.tr ?? ""}>
      {columns.map((h, key) => (
        <th
          className={
            /* general first*/
            (classNames?.thead?.trTh ?? "") +
            " " +
            /* specific last*/
            (h.headerClassName ?? "")
          }
          style={{ width: h.width }}
          key={key}
        >
          <div className={classNames?.thead?.trThWrapper ?? ""}>
            {h.name}
            {h.sortable && (
              <button
                className={classNames?.thead?.trThWrapperSortButton ?? ""}
                onClick={() => handleSorting(key)}
              >
                {sorting?.key === key
                  ? sorting.orderBy === "a"
                    ? style?.sortButtons?.asc || "asc"
                    : style?.sortButtons?.desc || "desc"
                  : style?.sortButtons?.unSorted || "unsorted"}
              </button>
            )}

            {h.filterable && (
              <button
                className={classNames?.thead?.trThWrapperFilterButton ?? ""}
                onClick={() => handleFiltering(key)}
              >
                {"filter"}
              </button>
            )}
          </div>
          {filteringColumnKey === key && (
            <div>
              <div>
                <select
                  defaultValue={filterOperationType}
                  onChange={(e) =>
                    setFilterOperationType(e.target.value as OperationType)
                  }
                >
                  {columns[key].valType === "string" ? (
                    StringOperationsOptions
                  ) : columns[key].valType === "number" ? (
                    NumberOperationsOptions
                  ) : (
                    <></>
                  )}
                </select>
              </div>

              <div>
                <input
                  value={filterVal}
                  onChange={(e) => setFilterVal(e.target.value)}
                />
                <button onClick={applyFilter}>apply</button>
              </div>
            </div>
          )}
        </th>
      ))}
    </tr>
  );
}
