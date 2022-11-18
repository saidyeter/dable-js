import { useMemo, useState } from "react";
import { convertData, extractColumns } from "./core";
import { IDableProps, IDableStyleProps, ISortProps } from "./types";

export function Dable<T>(props: IDableProps<T>) {
  const { style } = props;
  const { classNames } = style ?? ({} as IDableStyleProps);
  const [sorting, setSorting] = useState({} as ISortProps);
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
    const sorter = columns[sortVal.key]?.onSort;
    if (sorter) {
      sorter();
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

  return (
    <div className={classNames?.wrapper ?? ""}>
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
                </div>
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
