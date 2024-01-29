import axios from 'axios';
import { IProduct } from '../models/models';

export const loadProducts = async ( 
    productIndex : number,
    setProductIndex : React.Dispatch<React.SetStateAction<number>>,
    setProducts : React.Dispatch<React.SetStateAction<IProduct[]>>
) : Promise<void> => {
    const productsToLoad = 12;

    axios(`api/products?startId=${productIndex}&count=${productsToLoad}`)
        .then(response => {
            setProducts(prevProducts => [...prevProducts, ...response.data]);
            setProductIndex(prevIndex => prevIndex + productsToLoad);
        })
        .catch(error => console.error('Error fetching products, ', error));
};