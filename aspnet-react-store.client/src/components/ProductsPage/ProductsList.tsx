import { Refresh as RefreshIcon } from '@mui/icons-material';
import {
  Button,
  CircularProgress,
  Container,
  Grid,
  Typography,
} from '@mui/material';
import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { IProduct } from '../../types/types';
import { getProducts } from '../../utils/productsApiUtils';
import Product from './Product';
import SearchBadges from './SearchBadges';

interface ProductsListProps {
  searchText?: string;
}

export default function ProductsList({ searchText }: ProductsListProps) {
  const location = useLocation();

  const [products, setProducts] = useState<IProduct[]>([]);
  const [productIndex, setProductIndex] = useState<number>(1);
  const [productsNotFound, setProductsNotFound] = useState<boolean>(false);
  const [isLoadButtonVisible, setIsLoadButtonVisible] =
    useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const updateProducts = (newProducts: IProduct[]) => {
    setProducts((prevProducts) => [...prevProducts, ...newProducts]);
    setProductIndex(newProducts[newProducts.length - 1].id + 1);
  };

  useEffect(() => {
    setProducts([]);
    setIsLoading(true);

    getProducts(1, searchText).then((result) => {
      setIsLoading(false);

      if (result && result.length > 0) {
        setIsLoadButtonVisible(true);
        updateProducts(result);
      } else {
        setProductsNotFound(true);
      }
    });
  }, [searchText]);

  const handleRemoveSearchText = () => {
    const searchParams = new URLSearchParams(location.search);
    searchParams.delete('searchText');

    window.location.href = `${location.pathname}?${searchParams.toString()}`;
  };

  const loadButtonClick = async (): Promise<void> => {
    setIsLoading(true);

    await getProducts(productIndex, searchText).then((result) => {
      setIsLoading(false);

      if (!result || result.length === 0) setIsLoadButtonVisible(false);
      else updateProducts(result);
    });
  };

  const productsInRows = [];
  for (let i = 0; i < products.length; i += 4) {
    const rowProducts = products.slice(i, i + 4);
    productsInRows.push(rowProducts);
  }

  return (
    <>
      <Container style={{ marginTop: '6rem' }}>
        {searchText && searchText.trim() !== '' && (
          <SearchBadges
            searchText={searchText}
            onRemoveSearchText={handleRemoveSearchText}
          />
        )}

        {!productsNotFound &&
          productsInRows.map((rowProducts, rowIndex) => (
            <Grid container spacing={2} key={rowIndex}>
              {rowProducts.map((product, index) => (
                <Grid item xs={12} sm={6} md={3} key={product.id}>
                  <Product
                    product={product}
                    index={index}
                    rowIndex={rowIndex}
                  />
                </Grid>
              ))}
            </Grid>
          ))}

        {productsInRows.length === 0 && !productsNotFound && !isLoading && (
          <Typography variant='h6'>Загрузка товаров...</Typography>
        )}

        {productsNotFound && (
          <div style={{ textAlign: 'center' }}>
            <Typography variant='h6'>Товары не найдены</Typography>
          </div>
        )}

        {isLoading && (
          <div style={{ textAlign: 'center', marginTop: '2rem' }}>
            <CircularProgress />
          </div>
        )}
      </Container>

      {isLoadButtonVisible && !isLoading && (
        <div
          style={{
            textAlign: 'center',
            marginTop: '5rem',
          }}
        >
          <Button
            variant='outlined'
            style={{ width: 250, border: 'none' }}
            onClick={loadButtonClick}
          >
            Загрузить еще&nbsp;&nbsp;
            <RefreshIcon />
          </Button>
        </div>
      )}
    </>
  );
}
