export interface ICategory {
  id: number;
  name: string;
}

export interface IProduct {
  id: number;
  name: string;
  price: number;
  quantity: number;
  discount: number;
  description?: string | null;
  categories: ICategory[];
  images: IImage[];
}

export interface IUser {
  id: number;
  userName: string;
  shortName?: string | null;
  userRole: string;
}

export interface IUserProfile extends IUser {
  email?: string | null;
  firstName?: string | null;
  middleName?: string | null;
  lastName?: string | null;
  joinDate: Date;
}

export interface IImage {
  id: number;
  filePath: string;
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
