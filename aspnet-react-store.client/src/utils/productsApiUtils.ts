import axios, { AxiosResponse } from 'axios';
import { IProduct } from '../types/types';

export const getProducts = async (
  productIndex: number,
  searchText?: string
): Promise<IProduct[] | null> => {
  const productsToLoad = 12;

  return axios<IProduct[]>(
    `api/products?startId=${productIndex}&count=${productsToLoad}` +
      (searchText ? `&searchText=${searchText}` : '')
  )
    .then((response: AxiosResponse<IProduct[]>) => {
      const newProducts = response.data as IProduct[];

      return newProducts;
    })
    .catch((error) => {
      if (axios.isAxiosError(error) && error.response?.status === 400)
        return null;

      throw error;
    });
};
