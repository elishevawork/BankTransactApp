import React, { useState } from "react";
import { TransactOrderType } from "../../constants/enums";
import "./OrderForm.css";

const OrderForm = (props) => {
  const [data, setData] = useState({
    fullNameHeb: "",
    fullName: "",
    birthDate: "",
    idNumber: "",
    amount: 0,
    accountNumber: "",
    orderType: 0,
  });

  const ChangeHandler = (e) => {
    const { name, value } = e.target;
    setData({
      ...data,
      [name]: value,
    });
  };

  const [errors, setErrors] = useState({});

  const validateForm = (data) => {
    const newErrors = {};

    if (!data.fullName.trim()) {
      newErrors.fullName = "שדה חובה";
    }
    if (!data.fullNameHeb.trim()) {
      newErrors.fullNameHeb = "שדה חובה";
    }
    if (!data.amount) {
      newErrors.amount = "שדה חובה";
    }
    if (!data.accountNumber.trim()) {
      newErrors.accountNumber = "שדה חובה";
    }

    return newErrors;
  };

  const submitHandler = (event) => {
    event.preventDefault();
    //TODO init the fields
    const newErrors = validateForm(data);
    setErrors(newErrors);

    if (Object.keys(newErrors).length === 0) {
      const formData = {
        customerFullNameHebrew: data.fullNameHeb,
        customerFullName: data.fullName,
        customerDateOfBirth: new Date(data.birthDate),
        customerIdNumber: data.idNumber,
        amount: data.amount,
        accountNumber: data.accountNumber,
        orderType: parseInt(data.orderType, 10),
      };

      props.onSaveFormData(formData);
      console.log(formData);
      props.onClose();
    }
  };

  return (
    <div className="form-container">
      <h2 className="form-title">הכנס הוראה חדשה</h2>
      <form onSubmit={submitHandler}>
        <div className="form__control">
          <label>שם מלא בעברית</label>
          <input
            type="text"
            maxLength={20}
            name="fullNameHeb"
            value={data.fullNameHeb}
            onChange={ChangeHandler}
            onInput={(e) => {
              const regex = /^[\u0590-\u05FF\s \-']*$/;
              if (!regex.test(e.target.value)) {
                e.target.value = e.target.value.slice(0, -1);
                errors.fullNameHeb = "שם בעברית בלבד";
              } else {
                errors.fullNameHeb = "";
              }
            }}
          ></input>
          {errors.fullNameHeb && (
            <span className="error-message">{errors.fullNameHeb}</span>
          )}
        </div>
        <div className="form__control">
          <label>שם מלא באנגלית</label>
          <input
            type="text"
            maxLength={15}
            name="fullName"
            value={data.fullName}
            onChange={ChangeHandler}
            onInput={(e) => {
              const regex = /^[a-zA-Z\s\-']*$/;
              if (!regex.test(e.target.value)) {
                e.target.value = e.target.value.slice(0, -1);
                errors.fullName = "שם באנגלית בלבד";
              } else {
                errors.fullName = "";
              }
            }}
          ></input>
          {errors.fullName && (
            <span className="error-message">{errors.fullName}</span>
          )}
        </div>
        <div className="form__control">
          <label>תאריך לידה</label>
          <input
            type="date"
            name="birthDate"
            value={data.birthDate}
            max={new Date().toISOString().split("T")[0]}
            onChange={ChangeHandler}
          ></input>
        </div>
        <div className="form__control">
          <label>מספר זהות</label>
          <input
            type="text"
            maxLength={9}
            minLength={9}
            name="idNumber"
            value={data.idNumber}
            onChange={ChangeHandler}
            onInput={(e) => {
              const regex = /^\d{0,9}$/;
              if (!regex.test(e.target.value)) {
                e.target.value = e.target.value.slice(0, -1);
              }
            }}
          ></input>
          {errors.idNumber && (
            <span className="error-message">{errors.idNumber}</span>
          )}
        </div>
        <div className="form__control">
          <label>פעולה לביצוע</label>
          <select
            name="orderType"
            value={data.orderType}
            onChange={ChangeHandler}
          >
            {Object.entries(TransactOrderType).map(([key, label]) => (
              <option key={key} value={key}>
                {label}
              </option>
            ))}
          </select>
        </div>
        <div className="form__control">
          <label>סכום</label>
          <input
            type="number"
            name="amount"
            value={data.amount}
            onChange={ChangeHandler}
            onInput={(e) => {
              if (e.target.value.length > 10) {
                e.target.value = e.target.value.slice(0, 10);
              }
            }}
          ></input>
          {errors.amount && (
            <span className="error-message">{errors.amount}</span>
          )}
        </div>
        <div className="form__control">
          <label>מספר חשבון</label>
          <input
            type="text"
            maxLength={10}
            name="accountNumber"
            value={data.accountNumber}
            onChange={ChangeHandler}
          ></input>
          {errors.accountNumber && (
            <span className="error-message">{errors.accountNumber}</span>
          )}
        </div>
        <div className="action__container">
          <button className="actions" onClick={props.onClose}>
            ביטול
          </button>
          <button className="actions" type="submit">
            הוסף הוראה
          </button>
        </div>
      </form>
    </div>
  );
};

export default OrderForm;
