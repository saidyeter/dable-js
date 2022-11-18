import { Dable } from "./lib/table";
import Users from "./users.json";
import "./App.css";
import { IColumnProps } from "./lib/table/types";

function App() {
  const users: UserModel[] = Users;
  const columns: IColumnProps[] = [
    {
      name: "Id",
      identifier: "id",
      width: 50,
    },
    {
      name: "Name",
      identifier: "name",
      // width: 250,
      headerClassName: "nameColHeader",
    },
    {
      name: "User Name",
      identifier: "username",
      sortable: true,
    },
    {
      name: "E-Mail",
      identifier: "email",
    },
    {
      name: "Age",
      identifier: "age",
      sortable: true,
    },
  ];
  return (
    <div className="App">
      <Dable
        columns={columns}
        data={users}
        style={{
          classNames: {
            wrapper: "wrapper",
            table: "table",
            thead: {
              trTh: "theadtrth",
              trThWrapper: "theadtrthwrapper",
            },
            tbody: {
              tr: "tbodytr",
              trTd: "tbodyTrTd",
            },
          },
          sortButtons: {
            asc: "asc",
            desc: <span style={{ color: "red" }}>desc</span>,
            unSorted: (
              <svg
                stroke="currentColor"
                fill="currentColor"
                stroke-width="0"
                viewBox="0 0 320 512"
                height="14"
                width="14"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path d="M41 288h238c21.4 0 32.1 25.9 17 41L177 448c-9.4 9.4-24.6 9.4-33.9 0L24 329c-15.1-15.1-4.4-41 17-41zm255-105L177 64c-9.4-9.4-24.6-9.4-33.9 0L24 183c-15.1 15.1-4.4 41 17 41h238c21.4 0 32.1-25.9 17-41z"></path>
              </svg>
            ),
          },
        }}
      />
    </div>
  );
}
export interface UserModel {
  id: number;
  age: number;
  name: string;
  username: string;
  email: string;
  address: string;
  phone: string;
  website: string;
  companyname: string;
}

export default App;
