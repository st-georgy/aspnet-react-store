import { useEffect, useState } from 'react';
import { Button, Container, Row } from 'react-bootstrap';
import { CiRepeat } from 'react-icons/ci';
import { IProduct } from '../types/types';
import { loadProducts } from '../utils/apiUtils';
import Product from './Product';

export default function ProductsList() {
  const [products, setProducts] = useState<IProduct[]>([]);
  const [productIndex, setProductIndex] = useState<number>(1);
  const [isLoadButtonVisible, setIsLoadButtonVisible] = useState<boolean>(true);
  const [fetchCount, setFetchCount] = useState<number>(1);

  useEffect(() => {
    setProducts([]);
    loadProducts(1, setProductIndex, setProducts);
  }, []);

  const loadButtonClick = async (): Promise<void> => {
    await loadProducts(productIndex, setProductIndex, setProducts).then(
      (code) => {
        setFetchCount(fetchCount + 1);
        if (code === -1) setIsLoadButtonVisible(false);
      }
    );
  };

  const productsInRows = [];
  for (let i = 0; i < products.length; i += 4) {
    const rowProducts = products.slice(i, i + 4);
    productsInRows.push(rowProducts);
  }

  return (
    <>
      <Container style={{ marginTop: '6rem' }}>
        {productsInRows.map((rowProducts, rowIndex) => (
          <Row key={rowIndex}>
            {rowProducts.map((product, index) => (
              <Product
                product={product}
                // FIXME: анимация продуктов в первоначальной загрузке верна,
                // но в последующих все продукты отображатся одновременно (без trail) и дольше
                index={Math.round((rowIndex * 4 + index) / fetchCount)}
                key={product.id}
              />
            ))}
          </Row>
        ))}
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
