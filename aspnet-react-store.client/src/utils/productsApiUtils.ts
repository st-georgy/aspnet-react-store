import axios from 'axios';
import { IProduct } from '../types/types';

export const getProducts = async (
  productIndex: number,
  searchText?: string
): Promise<IProduct[] | null> => {
  const productsToLoad = 12;
  const url =
    `api/products?startId=${productIndex}&count=${productsToLoad}` +
    (searchText ? `&searchText=${searchText}` : '');

  try {
    const response = await axios.get<IProduct[]>(url);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status === 400)
      return null;
    throw error;
  }
};
