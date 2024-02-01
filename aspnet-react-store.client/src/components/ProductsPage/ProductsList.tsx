import { useEffect, useState } from 'react';
import { Button, Container, Row } from 'react-bootstrap';
import { CiRepeat } from 'react-icons/ci';
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

  const updateProducts = (newProducts: IProduct[]) => {
    setProducts((prevProducts) => [...prevProducts, ...newProducts]);
    setProductIndex(newProducts[newProducts.length - 1].id + 1);
  };

  useEffect(() => {
    setProducts([]);
    getProducts(1, searchText).then((result) => {
      if (result && result.length > 0) {
        setIsLoadButtonVisible(true);
        updateProducts(result);
      } else {
        setProductsNotFound(true);
      }
    });
  }, []);

  const handleRemoveSearchText = () => {
    const searchParams = new URLSearchParams(location.search);
    searchParams.delete('searchText');

    window.location.href = `${location.pathname}?${searchParams.toString()}`;
  };

  const loadButtonClick = async (): Promise<void> => {
    await getProducts(productIndex, searchText).then((result) => {
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
            <Row key={rowIndex}>
              {rowProducts.map((product, index) => (
                <Product
                  product={product}
                  index={index}
                  rowIndex={rowIndex}
                  key={product.id}
                />
              ))}
            </Row>
          ))}

        {productsInRows.length === 0 && !productsNotFound && (
          <h4>Загрузка товаров...</h4>
        )}

        {productsNotFound && (
          <div className='d-flex justify-content-center'>
            <h4>Товары не найдены</h4>
          </div>
        )}
      </Container>

      {isLoadButtonVisible && (
        <div className='text-center'>
          <Button
            variant='outline-dark'
            style={{ width: 250, border: 'none' }}
            onClick={loadButtonClick}
          >
            Загрузить еще&nbsp;&nbsp;
            <CiRepeat size={25} />
          </Button>
        </div>
      )}
    </>
  );
}
