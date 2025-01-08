import NewOrder from "../NewOrder/NewOrder";
import OrdersTable from "../Orders/OrdersTable";
import react, { useState, useEffect } from "react";
import * as Constants from "../../constants/consts";
import "./Orders.css";
import Axios from "axios";

const Orders = (props) => {
  const [orders, setOrders] = useState([]);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);

  useEffect(() => {
    fetchOrders();
  }, []);

  const fetchOrders = async () => {
    try {
      const res = await Axios.get(Constants.API_URL_GET_ORDERS);
      if (res.status === 200) {
        const fetchedOrders = res.data;
        if (fetchedOrders != undefined) {
          fetchedOrders.map((order) => ({
            ...order,
            customerDateOfBirth: new Date(order.customerDateOfBirth),
          }));
          setOrders(fetchedOrders);
          setError(null);
        }
        console.log(fetchedOrders);
      }
      else if(res.status !== 204)
      {
        setError("Failed to show orders. Please try later...");
      }
    } catch (error) {
      setError("Failed to show orders. Please try later...");
      setTimeout(() => setError(""), 3000);
    }
  };

  const postOrder = async (order) => {
    try {
      console.log("post order: and after response:");
      console.log(order);
      Axios.post(Constants.API_URL_POST_ORDER, order)
        .then((response) => {
          console.log(response);
          setOrders((prevOrders) => {
            return [response.data, ...prevOrders];
          });
          setError(null);
          setSuccess("Your Order Created Successfully!");
          setTimeout(() => setSuccess(""), 3000);
        })
        .catch((error) => {
          setError(
            "Failed to create the order. Please check the details and try again."
          );
          setTimeout(() => setError(""), 3000);
        });
    } catch (error) {
      setError(
        "Failed to create the order. Please check the details and try again."
      );
      setTimeout(() => setError(""), 3000);
    }
  };

  const deleteOrder = async (orderId) => {
    try {
      const { data } = await Axios.delete(
        `${Constants.API_URL_DELETE_ORDER}/${orderId}`
      );
      setOrders((prevOrders) => {
        console.log("Previous Orders:", prevOrders);
        const updatedOrders = prevOrders.filter(
          (order) => order.orderId !== orderId
        );
        return updatedOrders;
      });
      setError(null);
    } catch (error) {
      console.error("Error deleting order:", error.message);
      setError("Failed to delete order. Please try again later.");
      setTimeout(() => setError(""), 3000);
    }
  };

  const AddOrderHandler = (order) => {
    console.log(order);
    postOrder(order);
    console.log(orders);
  };

  const DeleteOrderHandler = (orderId) => {
    console.log(orderId);
    deleteOrder(orderId);
    console.log(orders);
  };

  return (
    <>
      {error && (
        <div className="error-message-alert">
          <p>{error}</p>
        </div>
      )}
      {success && (
        <div className="success-message-alert">
          <p>{success}</p>
        </div>
      )}
      <NewOrder onAddOrder={AddOrderHandler} />
      <OrdersTable orders={orders} onDelete={DeleteOrderHandler} />
    </>
  );
};

export default Orders;
