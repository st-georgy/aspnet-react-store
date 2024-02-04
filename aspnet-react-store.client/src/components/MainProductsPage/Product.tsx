import { Card, CardContent, Typography } from '@mui/material';
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
    <animated.div style={trail[index]} className='product-block'>
      <Card elevation={0}>
        <div className='image-container'>
          <ProductImages images={product.images} />
        </div>
        <CardContent className='product-content'>
          <Typography variant='h6' className='product-name'>
            {product?.name}
          </Typography>
          <Typography variant='body2'>
            <em>{'₽' + product?.price + ' руб.'}</em>
          </Typography>
        </CardContent>
      </Card>
    </animated.div>
  );
}
