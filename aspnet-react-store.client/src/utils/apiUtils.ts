import axios, { AxiosResponse } from 'axios';
import { IProduct } from '../types/types';

export const loadProducts = async (
  productIndex: number,
  setProductIndex: React.Dispatch<React.SetStateAction<number>>,
  setProducts: React.Dispatch<React.SetStateAction<IProduct[]>>,
  searchText?: string
): Promise<number> => {
  const productsToLoad = 12;

  return axios<IProduct[]>(
    `api/products?startId=${productIndex}&count=${productsToLoad}` +
      (searchText ? `&searchText=${searchText}` : '')
  )
    .then((response: AxiosResponse<IProduct[]>) => {
      const newProducts = response.data as IProduct[];

      if (newProducts.length > 0) {
        setProducts((prevProducts) => [...prevProducts, ...response.data]);
        setProductIndex(newProducts[newProducts.length - 1].id + 1);
      }

      return newProducts.length;
    })
    .catch((error) => {
      if (axios.isAxiosError(error) && error.response?.status === 400)
        return -1;

      throw error;
    });
};
