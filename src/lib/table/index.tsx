import { useEffect, useMemo, useState } from "react";
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
} from "../types";
import { BodyRow } from "../components/body-row";
import { HeaderRow } from "../components/header-row";

export function Dable<T>(props: IDableProps<T>) {
  const { style } = props;
  const { classNames } = style ?? ({} as IDableStyleProps);

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

  const [sorting, setSorting] = useState({} as ISortProps);
  const [filters, setFilters] = useState([] as IFilterProps[]);

  useEffect(() => {
    console.log("on filter");

    if (filters.length === 0) {
      return;
    }

    if (props.onFilter) {
      props.onFilter(filters);
    } else {
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

  useEffect(() => {
    console.log("on sort");
    if (!sorting.key) {
      return;
    }
    console.log(sorting);

    if (props.onSort) {
      props.onSort(sorting);
    } else {
      setData((prev) => {
        const last = prev.sort((a, b) => {
          const aVal = a[sorting.key].toString();
          const bVal = b[sorting.key].toString();
          if (sorting.orderBy === "a") {
            return aVal.localeCompare(bVal);
          } else {
            return bVal.localeCompare(aVal);
          }
        });
        console.log(last);
        return last;
      });
    }
  }, [sorting, props]);

  if (props.data.length === 0) {
    return <div>no values</div>;
  }
  if (columns.length === 0) {
    return <div>no columns</div>;
  }

  function handleFilter(_filters: IFilterProps[]) {
    setFilters(_filters);
  }

  function handleSort(sortVal: ISortProps) {
    setSorting(sortVal);
  }

  return (
    <div className={classNames?.wrapper ?? ""}>
      <div>{JSON.stringify(filters)}</div>
      <table className={classNames?.table ?? ""}>
        <thead className={classNames?.thead?.thead ?? ""}>
          <HeaderRow
            columns={columns}
            style={style}
            onSort={handleSort}
            onFilter={handleFilter}
          />
        </thead>
        <tbody>
          {data.map((items, key) => {
            const data = {
              classNamesTbodyTr: classNames?.tbody?.tr,
              classNamesTbodyTrTd: classNames?.tbody?.trTd,
              headerClassName: columns[key]?.headerClassName,
              items,
            };
            return (
              <BodyRow
                key={key}
                {...data}
              />
            );
          })}
        </tbody>
      </table>
    </div>
  );
}
