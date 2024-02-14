import axios, { AxiosResponse } from 'axios';
import { IUser } from '../types/types';

interface LoginData {
  email: string;
  password: string;
}

interface RegisterData {
  email: string;
  username: string;
  password: string;
}

const handleResponse = <T>(response: AxiosResponse<T>): T => {
  if (response.status === 200) return response.data;
  throw new Error('Request failed');
};

const handleError = (error: any): boolean => {
  if (axios.isAxiosError(error) && error.response?.status === 401) return false;
  throw error;
};

export const login = async (loginData: LoginData): Promise<boolean> => {
  try {
    await axios.post('/api/auth/login', loginData);
    return true;
  } catch (error) {
    return handleError(error);
  }
};

export const register = async (
  registerData: RegisterData
): Promise<boolean> => {
  try {
    await axios.post('/api/auth/register', registerData);
    return true;
  } catch (error) {
    return handleError(error);
  }
};

export const validateToken = async (): Promise<boolean> => {
  try {
    await axios.post('/api/auth/validate');
    return true;
  } catch (error) {
    return false;
  }
};

export const logout = async (): Promise<boolean> => {
  try {
    await axios.post('/api/auth/logout');
    return true;
  } catch (error) {
    return false;
  }
};

export const getUser = async (): Promise<IUser | null> => {
  try {
    const response = await axios.get('/api/profile/me');
    return handleResponse<IUser>(response);
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status === 401)
      return null;
    throw error;
  }
};
