import axios from 'axios';
import { ICart } from '../types/types';

export const getCart = async (): Promise<ICart | null> => {
  try {
    const response = await axios.get<ICart>('api/carts');
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) return null;
    throw error;
  }
};
