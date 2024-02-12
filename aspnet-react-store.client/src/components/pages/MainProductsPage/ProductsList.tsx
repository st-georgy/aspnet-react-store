import { Refresh as RefreshIcon } from '@mui/icons-material';
import { Button, Container, Grid, Skeleton, Typography } from '@mui/material';
import { useEffect, useMemo, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { IProduct } from '../../../types/types';
import { getProducts } from '../../../utils/productsApiUtils';
import ShowAlert from '../../shared/ShowAlert';
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
  const [error, setError] = useState<string | null>(null);

  const searchParams = useMemo(() => {
    return new URLSearchParams(location.search);
  }, [location.search]);

  useEffect(() => {
    const fetchProducts = async () => {
      setIsLoading(true);
      setProducts([]);

      try {
        const result = await getProducts(1, searchText);

        if (result && result.length > 0) {
          setIsLoadButtonVisible(result.length >= 12);
          updateProducts(result);
          setIsLoading(false);
        } else {
          setProductsNotFound(true);
        }
      } catch (error) {
        setError('Ошибка при загрузке товаров');
      }
    };

    fetchProducts();
  }, [searchText]);

  const updateProducts = (newProducts: IProduct[]) => {
    setProducts((prevProducts) => [...prevProducts, ...newProducts]);
    setProductIndex(newProducts[newProducts.length - 1].id + 1);
  };

  const handleRemoveSearchText = () => {
    searchParams.delete('searchText');
    window.location.href = `${location.pathname}?${searchParams.toString()}`;
  };

  const loadMoreProducts = async () => {
    try {
      const result = await getProducts(productIndex, searchText);

      if (result && result.length > 0) {
        setIsLoadButtonVisible(result.length >= 12);
        updateProducts(result);
        setIsLoading(false);
      }
    } catch (error) {
      setError('Ошибка при загрузке товаров');
    }
  };

  const skeletonArray = new Array(4).fill(null);

  return (
    <>
      <Container style={{ marginTop: '6rem' }}>
        {searchText && searchText.trim() !== '' && (
          <SearchBadges
            searchText={searchText}
            onRemoveSearchText={handleRemoveSearchText}
          />
        )}

        {!productsNotFound && (
          <Grid container spacing={4}>
            {products.map((product, index) => (
              <Grid item xs={12} sm={6} md={4} lg={3} key={product.id}>
                <Product product={product} index={index} />
              </Grid>
            ))}

            {isLoading &&
              skeletonArray.map((_, index) => (
                <Grid item xs={12} sm={6} md={4} lg={3} key={index}>
                  <Skeleton
                    variant='rectangular'
                    height={250}
                    animation='wave'
                  />
                </Grid>
              ))}
          </Grid>
        )}

        {productsNotFound && (
          <div style={{ textAlign: 'center' }}>
            <Typography variant='h6'>Товары не найдены</Typography>
          </div>
        )}
      </Container>

      {isLoadButtonVisible && (
        <div style={{ textAlign: 'center', marginTop: '5rem' }}>
          <Button
            variant='outlined'
            style={{ width: 230, border: 'none' }}
            onClick={loadMoreProducts}
          >
            Загрузить еще&nbsp;&nbsp;
            <RefreshIcon />
          </Button>
        </div>
      )}

      {error && (
        <ShowAlert
          open={!!error}
          message={error || ''}
          severity={'error'}
          anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
        />
      )}
    </>
  );
}
