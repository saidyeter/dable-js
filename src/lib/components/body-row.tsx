interface BodyRowProps {
  items: (string | number)[];
  classNamesTbodyTr?: string;
  classNamesTbodyTrTd?: string;
  headerClassName?: string;
}

export function BodyRow(props: BodyRowProps) {
  const { items, headerClassName, classNamesTbodyTr, classNamesTbodyTrTd } =
    props;
  return (
    <tr className={classNamesTbodyTr ?? ""}>
      {items.map((item, key) => (
        <td
          className={
            /* general first*/
            (classNamesTbodyTrTd ?? "") +
            " " +
            /* specific last*/
            (headerClassName ?? "")
          }
          //   className="p-3 text-sm group-hover:bg-blue-50 group-hover:text-blue-600"
          key={key}
        >
          {item}
        </td>
      ))}
    </tr>
  );
}
