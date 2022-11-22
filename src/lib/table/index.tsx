import { useEffect, useMemo, useState } from "react";
import { StringOperations, NumberOperations } from "./constants";
import {
  checkNumVal,
  checkStringVal,
  convertData,
  extractColumns,
} from "./core";
import {
  IDableProps,
  IDableStyleProps,
  IFilterProps,
  ISortProps,
  OperationType,
} from "./types";

export function Dable<T>(props: IDableProps<T>) {
  const { style } = props;
  const { classNames } = style ?? ({} as IDableStyleProps);
  const [sorting, setSorting] = useState({} as ISortProps);

  const [filterVal, setFilterVal] = useState("");
  const [filteringColumnKey, setFilteringColumnKey] = useState(-1);
  const [filterOperationType, setFilterOperationType] = useState(
    "Equals" as OperationType
  );

  const columns = useMemo(() => {
    if (props.columns) {
      return props.columns;
    } else {
      if (props.data.length === 0) {
        return [];
      }
      return extractColumns(props.data[0]);
    }
  }, [props.columns, props.data]);
  const _data = convertData(columns, props.data);
  const [data, setData] = useState(_data);

  const [filters, setFilters] = useState([] as IFilterProps[]);

  useEffect(() => {
    if (props.onFilter) {
      props.onFilter(filters);
    } else if (filters.length > 0) {
      const filteredData = props.data.filter((item, index) => {
        let remains = true;
        filters.forEach((filter) => {
          const col = columns[filter.key];
          const colVal = item[col.identifier as keyof T];
          if (col.valType === "string") {
            const colValStr = colVal as string;
            remains = checkStringVal(colValStr, filter);
          }
          if (col.valType === "number") {
            const colValNum = colVal as number;
            remains = checkNumVal(colValNum, filter);
          }
        });

        return remains;
      });
      setData(convertData(columns, filteredData));
    }
  }, [filters, props, columns]);

  if (props.data.length === 0) {
    return <div>no values</div>;
  }
  if (columns.length === 0) {
    return <div>no columns</div>;
  }
  function handleSorting(key: number) {
    const sortVal: ISortProps = {
      key,
      orderBy: "a",
    };

    if (sorting?.key === key) {
      sortVal.orderBy = sorting.orderBy === "a" ? "d" : "a";
    }
    setSorting(sortVal);

    if (props.onSort) {
      props.onSort(sortVal);
    } else {
      setData((prev) => {
        return prev.sort((a, b) => {
          if (sortVal.orderBy === "a") {
            return a[sortVal.key]
              .toString()
              .localeCompare(b[sortVal.key].toString());
          } else {
            return b[sortVal.key]
              .toString()
              .localeCompare(a[sortVal.key].toString());
          }
        });
      });
    }
  }

  function handleFiltering(key: number) {
    setFilteringColumnKey(key);
  }

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
    setFilters((prev) => [
      ...prev,
      {
        key: filteringColumnKey,
        operation: filterOperationType,
        val: filterVal,
      },
    ]);
    setFilterVal("");
    setFilteringColumnKey(-1);
    setFilterOperationType("Equals");
  }

  return (
    <div className={classNames?.wrapper ?? ""}>
      <div>{JSON.stringify(filters)}</div>
      <table className={classNames?.table ?? ""}>
        <thead className={classNames?.thead?.thead ?? ""}>
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
                      className={
                        classNames?.thead?.trThWrapperFilterButton ?? ""
                      }
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
                          setFilterOperationType(
                            e.target.value as OperationType
                          )
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
        </thead>
        <tbody>
          {data.map((items, key) => (
            <tr
              className={classNames?.tbody?.tr ?? ""}
              key={key}
            >
              {items.map((item, key) => (
                <td
                  className={
                    /* general first*/
                    (classNames?.tbody?.trTd ?? "") +
                    " " +
                    /* specific last*/
                    (columns[key]?.headerClassName ?? "")
                  }
                  //   className="p-3 text-sm group-hover:bg-blue-50 group-hover:text-blue-600"
                  key={key}
                >
                  {item}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
