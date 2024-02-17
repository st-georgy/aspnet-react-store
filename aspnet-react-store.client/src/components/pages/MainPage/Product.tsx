import { Card, CardContent, Typography } from '@mui/material';
import { Link } from 'react-router-dom';
import { animated, useTrail } from 'react-spring';
import { IProduct } from '../../../types/types';
import '../../style/product.css';
import ProductImages from './ProductImages';

export default function Product({
  product,
  index,
}: {
  product: IProduct;
  index: number;
}) {
  const trail = useTrail(index + 1, {
    opacity: 1,
    y: 0,
    from: { opacity: 0, y: 20 },
  });

  return (
    <animated.div style={trail[index]} className='product-block'>
      <Card elevation={0}>
        <Link to={`/products/${product.id}`}>
          <div className='image-container'>
            <ProductImages images={product.images} />
          </div>
        </Link>
        <CardContent className='product-content'>
          <Link to={`/products/${product.id}`}>
            <Typography variant='h6' className='product-name'>
              {product?.name}
            </Typography>
          </Link>
          <Typography variant='body2'>
            <em>{'₽' + product?.price + ' руб.'}</em>
          </Typography>
        </CardContent>
      </Card>
    </animated.div>
  );
}
