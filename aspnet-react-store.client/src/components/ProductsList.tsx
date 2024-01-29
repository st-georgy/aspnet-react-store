import { useEffect, useState } from 'react';
import { Button, Container, Row } from 'react-bootstrap';
import { CiRepeat } from 'react-icons/ci';
import { IProduct } from '../models/models';
import { loadProducts } from '../utils/apiUtils';
import Product from './Product';

export default function ProductsList() {
  const [products, setProducts] = useState<IProduct[]>([]);
  const [productIndex, setProductIndex] = useState<number>(1);

  useEffect(() => {
    setProducts([]);
    loadProducts(1, setProductIndex, setProducts);
  }, []);

  const loadButtonClick = async (): Promise<void> => {
    await loadProducts(productIndex, setProductIndex, setProducts);
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
                index={rowIndex * index}
                key={product.id}
              />
            ))}
          </Row>
        ))}
      </Container>

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
    </>
  );
}
