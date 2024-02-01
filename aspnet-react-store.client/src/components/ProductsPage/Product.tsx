import { Card, Col } from 'react-bootstrap';
import { animated, useTrail } from 'react-spring';
import { IProduct } from '../../types/types';
import '../style/product.css';
import ProductImages from './ProductImages';

export default function Product({
  product,
  index,
  rowIndex,
}: {
  product: IProduct;
  index: number;
  rowIndex: number;
}) {
  const trail = useTrail(rowIndex * 4 + index + 1, {
    opacity: 1,
    y: 0,
    from: { opacity: 0, y: 20 },
  });

  return (
    <Col key={index} className='mb-4'>
      <animated.div
        style={trail[index]}
        className='product-block d-flex justify-content-around'
      >
        <Card className='image-container'>
          <ProductImages />
          <Card.Body>
            <Card.Title className='product-name'>{product?.name}</Card.Title>
            <Card.Text>
              <em>{'₽' + product?.price + ' руб.'}</em>
            </Card.Text>
          </Card.Body>
        </Card>
      </animated.div>
    </Col>
  );
}
