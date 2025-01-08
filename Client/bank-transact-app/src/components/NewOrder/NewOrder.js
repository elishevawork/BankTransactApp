import React, { useState } from "react";
import OrderForm from "./OrderForm";
import { TransactionType } from "../../constants/enums";
import "./NewOrder.css";

const NewOrder = (props) => {
  const [isEditing, setIsEditing] = useState(false);

  const startEditingHandler = () => {
    setIsEditing(true);
  };

  const stopEditingHandler = () => {
    setIsEditing(false);
  };

  const saveFormDataHandler = (enFormData) => {
    const formData = {
      ...enFormData,
      id: Math.random().toString(),
    };
    props.onAddOrder(formData);
  };

  return (
    <>
    <div className="new-order">
      {!isEditing && <button onClick={startEditingHandler}>הוסף הוראה</button>}
      {isEditing && (
        <OrderForm
          onClose={stopEditingHandler}
          onSaveFormData={saveFormDataHandler}/>)}</div>
    </>
  );
};

export default NewOrder;
