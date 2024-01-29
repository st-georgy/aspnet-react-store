export interface IProduct {
  id: number;
  name: string;
  price: number;
  description?: string | null;
}

export interface ICart {
  id: number;
  user: IUser;
  products: IProduct[];
}

export interface IOrder {
  id: number;
  user: IUser;
  status: OrderStatus;
  products: IProduct[];
}

export interface IUser {
  id: number;
  username: string;
  cart: ICart;
  info: IUserInfo;
  accountType: AccountType;
  orders: IOrder[];
}

export interface IUserInfo {
  id: number;
  user: IUser;
  email?: string | null;
  firstName?: string | null;
  middleName?: string | null;
  lastName?: string | null;
  joinDate: Date;
}

export enum OrderStatus {
  Pending,
  Cancelled,
  Declined,
  Confirmed,
  Shipped,
  Completed,
}

export enum AccountType {
  User,
  Support,
  Admin,
}
