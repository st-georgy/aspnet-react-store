export interface IProduct {
  id: number;
  name: string;
  price: number;
  description?: string | null;
  images: IImage[];
}

export interface ICart {
  id: number;
  user: IUser;
  products: IProduct[];
}

export interface IImage {
  id: number;
  filePath: string;
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
  userRole: UserRole;
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

export enum UserRole {
  User,
  Support,
  Admin,
}

export enum FormType {
  Login,
  Register,
}
