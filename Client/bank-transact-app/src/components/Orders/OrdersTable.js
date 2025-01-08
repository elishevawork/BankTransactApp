import "./OrdersTable.css";
import { TransactOrderType, TransactOrderStatus } from "../../constants/enums";

const OrdersTable = (props) => {
  console.log(props.orders);
  if (props.orders.length === 0) {
    return <h2 className="">Found no orders</h2>;
  }

  const deleteOrder = (key) => {
    props.onDelete(key);
  };

  return (
    <div className="orders-table">
      <table>
        <tr>
          <th>מספר זהות</th>
          <th>שם מלא</th>
          <th>שם מלא באנגלית</th>
          <th>תאריך לידה</th>
          <th>סוג הפעולה</th>
          <th>סכום</th>
          <th>מספר חשבון</th>
          <th>סטטוס הפעולה</th>
          <th>הסר</th>
        </tr>
        {props.orders.map((val, key) => {
          return (
            <tr key={key}>
              <td>{val.customerIdNumber}</td>
              <td>{val.customerFullNameHebrew}</td>
              <td>{val.customerFullName}</td>
              <td>{new Date(val.customerDateOfBirth).toLocaleDateString()}</td>
              <td>{TransactOrderType[val.orderType] || "Unknown"}</td>
              <td>{val.amount}</td>
              <td>{val.accountNumber}</td>
              <td>{TransactOrderStatus[val.status] || "Unknown"}</td>
              <td>
                <button onClick={() => deleteOrder(val.orderId)}>❌</button>
              </td>
            </tr>
          );
        })}
      </table>
    </div>
  );
};

export default OrdersTable;
